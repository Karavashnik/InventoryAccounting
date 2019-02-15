using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Hosting;

namespace InventoryAccounting.Models
{
    public static class CreateWordDocuments
    {
        public static byte[]  CreateDocumentFromTmcLayout(IHostingEnvironment environment, Tmc tmc)
        {
            var template = new FileInfo(Path.Combine(environment.WebRootPath, "docs/tmc.dotx"));
            List<Tuple<string, string>> strings = new List<Tuple<string, string>>();
            strings.Add(new Tuple<string, string>("InventoryNumber", tmc.InventoryNumber.ToString()));
            strings.Add(new Tuple<string, string>("Name", tmc.Name));
            strings.Add(new Tuple<string, string>("Description", tmc.Description));
            strings.Add(new Tuple<string, string>("FactoryNumber", tmc.FactoryNumber.ToString()));
            strings.Add(new Tuple<string, string>("PurchaseDate", tmc.PurchaseDate.ToShortDateString()));
            strings.Add(new Tuple<string, string>("WriteOffDate", tmc.WriteOffDate.HasValue ? tmc.WriteOffDate.Value.ToShortDateString() : ""));
            strings.Add(new Tuple<string, string>("WarrantyDate", tmc.WarrantyDate.HasValue ? tmc.WarrantyDate.Value.ToShortDateString() : ""));
            strings.Add(new Tuple<string, string>("LastName", tmc.ResponsiblePerson.LastName));
            strings.Add(new Tuple<string, string>("FirstName", tmc.ResponsiblePerson.FirstName));
            strings.Add(new Tuple<string, string>("PersonnelNumber", tmc.ResponsiblePerson.PersonnelNumber.ToString()));
            strings.Add(new Tuple<string, string>("ActNumber", tmc.Act.ActNumber.ToString()));
            strings.Add(new Tuple<string, string>("Type", tmc.Type.Name));
            strings.Add(new Tuple<string, string>("RoomName", tmc.Room.Name));
            strings.Add(new Tuple<string, string>("ContractNumber", tmc.Act.Contract.ContractNumber.ToString()));
            return CreateWordDocument(template, strings);
        }
        private static byte[] CreateWordDocument(FileInfo file, List<Tuple<string, string>> strings)
        {
            FileInfo template = file;
            //var template = @"~/docs/tmc.dotx";
            byte[] templateBytes = File.ReadAllBytes(template.FullName);

            using (var templateStream = new MemoryStream())
            {
                templateStream.Write(templateBytes, 0, templateBytes.Length);
                using (WordprocessingDocument document = WordprocessingDocument.Open(templateStream, true))
                {
                    // Change the document type to Document
                    document.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);

                    // Get the MainPart of the document
                    MainDocumentPart mainPart = document.MainDocumentPart;

                    // Get the Document Settings Part
                    DocumentSettingsPart documentSettingPart1 = mainPart.DocumentSettingsPart;

                    foreach (var str in strings)
                    {
                        try
                        {
                            ReplaceBookmarkParagraphs(document, str.Item1, str.Item2);
                        }
                        catch (InvalidOperationException)
                        {
                            // ignore
                        }
                    }
                    // Create a new attachedTemplate and specify a relationship ID
                    //AttachedTemplate attachedTemplate1 = new AttachedTemplate() { Id = "relationId1" };

                    // Append the attached template to the DocumentSettingsPart
                    //documentSettingPart1.Settings.Append(attachedTemplate1);

                    // Add an ExternalRelationShip of type AttachedTemplate.
                    // Specify the path of template and the relationship ID
                    //documentSettingPart1.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(template.FullName, UriKind.Absolute), "relationId1");

                    // Save the document
                    mainPart.Document.Save();
                }

                templateStream.Position = 0;
                var result = templateStream.ToArray();
                templateStream.Flush();

                return result;
            }
        }
        private static void ReplaceBookmarkParagraphs(WordprocessingDocument doc, string bookmark, string text)
        {
            //Find all Paragraph with 'BookmarkStart' 
            var t = (from el in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>()
                where (el.Name == bookmark) &&
                      (el.NextSibling<Run>() != null)
                select el).First();
            //Take ID value
            var val = t.Id.Value;
            //Find the next sibling 'text'
            OpenXmlElement next = t.NextSibling<Run>();
            //Set text value
            next.GetFirstChild<Text>().Text = text;

            //Delete all bookmarkEnd node, until the same ID
            deleteElement(next.GetFirstChild<Text>().Parent, next.GetFirstChild<Text>().NextSibling(), val, true);
        }
        private static bool deleteElement(OpenXmlElement parentElement, OpenXmlElement elem, string id, bool seekParent)
        {
            bool found = false;

            //Loop until I find BookmarkEnd or null element
            while (!found && elem != null && (!(elem is BookmarkEnd) || (((BookmarkEnd)elem).Id.Value != id)))
            {
                if (elem.ChildElements != null && elem.ChildElements.Count > 0)
                {
                    found = deleteElement(elem, elem.FirstChild, id, false);
                }

                if (!found)
                {
                    OpenXmlElement nextElem = elem.NextSibling();
                    elem.Remove();
                    elem = nextElem;
                }
            }

            if (!found)
            {
                if (elem == null)
                {
                    if (!(parentElement is Body) && seekParent)
                    {
                        //Try to find bookmarkEnd in Sibling nodes
                        found = deleteElement(parentElement.Parent, parentElement.NextSibling(), id, true);
                    }
                }
                else
                {
                    if (elem is BookmarkEnd && ((BookmarkEnd)elem).Id.Value == id)
                    {
                        found = true;
                    }
                }
            }

            return found;
        }
    }
}