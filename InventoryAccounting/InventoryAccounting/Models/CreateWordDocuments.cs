using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace InventoryAccounting.Models
{
    public class CreateWordDocuments
    {
        public byte[]  CreateDocumentFromTmcLayout(IHostingEnvironment environment, Tmc tmc)
        {
            var template = new FileInfo(Path.Combine(environment.WebRootPath, "docs/tmc.dotx"));
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

                    // Create a new attachedTemplate and specify a relationship ID
                    AttachedTemplate attachedTemplate1 = new AttachedTemplate() { Id = "relationId1" };

                    // Append the attached template to the DocumentSettingsPart
                    documentSettingPart1.Settings.Append(attachedTemplate1);

                    // Add an ExternalRelationShip of type AttachedTemplate.
                    // Specify the path of template and the relationship ID
                    documentSettingPart1.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(template.FullName, UriKind.Absolute), "relationId1");

                    // Save the document
                    mainPart.Document.Save();
                }
                templateStream.Position = 0;
                var result = templateStream.ToArray();
                templateStream.Flush();

                return result;
            }
            /*
            var filePath = @"~/docs/tmc.dotx";
            var file = WordprocessingDocument.CreateFromTemplate(filePath);
            
            IDictionary<String, BookmarkStart> bookmarkMap = 
                new Dictionary<String, BookmarkStart>();

            foreach (BookmarkStart bookmarkStart in file.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarkMap[bookmarkStart.Name] = bookmarkStart;
            }

            foreach (BookmarkStart bookmarkStart in bookmarkMap.Values)
            {
                Run bookmarkText = bookmarkStart.NextSibling<Run>();
                if (bookmarkText != null)
                {
                    bookmarkText.GetFirstChild<Text>().Text = "blah";
                }
            }
            */
        }
        public static void ReplaceBookmarkParagraphs(WordprocessingDocument doc, string bookmark, string text)
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
        public static bool deleteElement(OpenXmlElement parentElement, OpenXmlElement elem, string id, bool seekParent)
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