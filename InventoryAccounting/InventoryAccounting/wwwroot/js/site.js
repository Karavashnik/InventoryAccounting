﻿$(function () {
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
    $("#CompanySelect").on("show", UpdateCompanies());
});
function UpdateContracts() {
    AjaxCall('/Acts/GetContracts', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#ContractSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].contractNumber + '</option>';
            }
            select.append(options);
        }
    }).fail(function (error) {
        alert(error.StatusText);
    });
}
function UpdateCompanies() {
    AjaxCall('/Contracts/GetCompanies', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#CompanySelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].name + '</option>';
            }
            select.append(options);
        }
    }).fail(function (error) {
        alert(error.StatusText);
    });
}
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}