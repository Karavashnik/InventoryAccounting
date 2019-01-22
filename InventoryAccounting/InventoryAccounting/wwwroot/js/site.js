// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// $('#modal-container').modal('handleUpdate');
// $(function () {
//     // when the modal is closed
//     $('#modal-container').on('hidden.bs.modal', function () {
//         // remove the bs.modal data attribute from it
//         $(this).removeData('bs.modal');
//         // and empty the modal-content element
//         $('#modal-container .modal-content').empty();
//     });
// });
// // boostrap 4 load modal example from docs
// $('#modal-container').on('show.bs.modal', function (event) {
//     var button = $(event.relatedTarget); // Button that triggered the modal
//     var url = button.attr("href");
//     var modal = $(this);
//
//     // note that this will replace the content of modal-content ever time the modal is opened
//     modal.find('.modal-content').load(url);
// });
$(function () {
    var placeholderElement = $('#modal-placeholder').modal('handleUpdate');
    $(document).on('click', 'a[data-toggle="modal"]',function (event) {
        var url = $(this).data('url');     
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            
            // find IsValid input field and check it's value
            // if it's valid then hide modal window
            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                var tableElement = $('#main-table');
                var tableUrl = tableElement.data('url');
                $.get(tableUrl).done(function (table) {                    
                    tableElement.replaceWith(table);
                    placeholderElement.find('.modal').modal('hide');
                });
            }            
        });
    });
    placeholderElement.on('click', '[data-delete="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {            
            var tableElement = $('#main-table');
            var tableUrl = tableElement.data('url');
            $.get(tableUrl).done(function (table) {
                tableElement.replaceWith(table);
            });
            placeholderElement.find('.modal').modal('hide');            
        });
    });
    $(placeholderElement).on('hidden.bs.modal', function () {
        // remove the bs.modal data attribute from it
        $(this).removeData('bs.modal');
        // and empty the modal-content element
        $(placeholderElement).empty();
        $(".modal-backdrop").remove();
    });

    // $('#CompanySelect').change(function () {
    //     var companyId = $(this).val();
    //     $.ajax({
    //         url: '@Url.Action("GetCompanies ","Contracts")',
    //         data: { CompanyId: companyId },
    //         datatype: "json",
    //         success: function (data) {
    //             $.each(data, function (i, company) {
    //                 companyId.append(
    //                     $('<option></option>').val(company.Id).html(company.Name)
    //                 );
    //             });
    //
    //         }
    //     })
    // });
    
    $("#CompanySelect").change(function () {
        var companyId = $(this).val();
        $.ajax({
            url: 'GetCompanies',
            data: { CompanyId: companyId },
            //datatype: "json",
            success: function(companies) {
                var items = '<option>Выберите компанию</option>';
                $.each(companies, function (i, company) {
                    items += "<option value='" + company.Id + "'>" + company.Name + "</option>";
                });
                console.log("Executed");
            }
        });    
        
     });
    // $('#CompanySelect').change(function () {
    //     var URL = '@Url.Action("GetCompanies","Contracts")';
    //     $.getJSON(URL + '/' + $('#CompanyId').val(), function (data) {
    //         var items = '<option>Выберите компанию</option>';
    //         $.each(data, function (i, company) {
    //             items += "<option value='" + company.Id + "'>" + state.Name + "</option>";
    //             // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
    //         });
    //         $('#StatesID').html(items);
    //         $('#StatesDivID').show();
    //
    //     });
    // });
    
});