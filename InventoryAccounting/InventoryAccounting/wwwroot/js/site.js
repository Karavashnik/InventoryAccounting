﻿$(function () {
    $(":input").inputmask();
    function SetDataTable(table) {
        if ($.fn.dataTable.isDataTable('#main-table')) {
            table = $('#main-table').DataTable();
        } else {        
        table = $('#main-table').DataTable(
            {
                select: false,
                "scrollX": true,
                "autoWidth": false,
                "language": {
                    "processing": "Подождите...",
                    "search": "Поиск:",
                    "lengthMenu": "Показать _MENU_ записей",
                    "info": "Записи с _START_ до _END_ из _TOTAL_ записей",
                    "infoEmpty": "Записи с 0 до 0 из 0 записей",
                    "infoFiltered": "(отфильтровано из _MAX_ записей)",
                    "infoPostFix": "",
                    "loadingRecords": "Загрузка записей...",
                    "zeroRecords": "Записи отсутствуют.",
                    "emptyTable": "В таблице отсутствуют данные",
                    "paginate": {
                        "first": "Первая",
                        "previous": "Предыдущая",
                        "next": "Следующая",
                        "last": "Последняя"
                    },
                    "aria": {
                        "sortAscending": ": активировать для сортировки столбца по возрастанию",
                        "sortDescending": ": активировать для сортировки столбца по убыванию"
                    }
                }
            }
        );
        }
        table.columns.adjust().draw();
        $('label.toggle-vis').on( 'click', function (e) {
            e.preventDefault();
            // Get the column API object
            var column = table.column( $(this).attr('data-column') );
            if(!$(this).hasClass('active') ) {
                column.visible(true);
            }else {
                column.visible(false);
            }
        } );
        return table
    }
    var table = SetDataTable(table);
    
    var placeholderElement = $('#modal-placeholder').modal('handleUpdate');
    $(document).on('click', 'a[data-toggle="modal"]',function (event) {
        var url = $(this).attr('data-url'); 
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var button = this;
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (data) {
            
            var newBody = $('.modal-body', data);
            if (! button.hasAttribute('data-delete')) {
                var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            } else {
                var isValid = newBody.find('[name="IdErrorMessage"]').text() == '';
            }

            placeholderElement.find('.modal-body').replaceWith(newBody);
                    
            // find IsValid input field and check it's value
            // if it's valid then hide modal window        
            if (isValid) {                
                var tableElement = $('#main-table');
                var tableUrl = tableElement.data('url');
                AjaxCall(tableUrl).done(function (data) {   
                    var options = table.options;
                    $('#partialTable').html(data);
                    SetDataTable(table);
                    placeholderElement.find('.modal').modal('hide');
                });
            }            
        });
    });
    $(placeholderElement).on('hidden.bs.modal', function () {
        // remove the bs.modal data attribute from it
        $(this).removeData('bs.modal');
        // and empty the modal-content element
        $(placeholderElement).empty();
        $(".modal-backdrop").remove();        
    });
    
    $('#TmcTypesSelect').change(function () {
        var select = $('#TmcTypesSelect');
        var del = $('#delete-tmc-type');
        var edit = $('#edit-tmc-type');
        edit.attr("data-url", "/TmcTypes/Edit/" + select.val());
        del.attr("data-url", "/TmcTypes/Delete/" + select.val());
    });    
    
});
function UpdateTmcTypes() {
    AjaxCall('/TmcTypes/GetTmcTypes', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#TmcTypesSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].name + '</option>';
            }
            select.append(options);
            var del = $('#delete-tmc-type');
            var edit = $('#edit-tmc-type');
            edit.attr("data-url", "/TmcTypes/Edit/" + select.val());
            del.attr("data-url", "/TmcTypes/Delete/" + select.val());
        }
    });    
}
function UpdateRooms() {
    AjaxCall('/Rooms/GetRooms', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#RoomsSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].name + '</option>';
            }
            select.append(options);
        }
    })
}
function UpdateActs() {
    AjaxCall('/Acts/GetActs', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#ActsSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].actNumber + '</option>';
            }
            select.append(options);
        }
    })
}
function UpdatePersons() {
    AjaxCall('/Persons/GetPersons', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#PersonsSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].lastName + ' ' + response[i].firstName + '</option>';
            }
            select.append(options);
        }
    })
}
function UpdateContracts() {
    AjaxCall('/Contracts/GetContracts', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#ContractsSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].contractNumber + '</option>';
            }
            select.append(options);
        }
    })
}
function UpdateCompanies() {
    AjaxCall('/Companies/GetCompanies', null).done(function (response) {
        if (response.length > 0) {
            var select = $('#CompaniesSelect');
            select.html('');
            var options = '';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + response[i].id + '">' + response[i].name + '</option>';
            }
            select.append(options);
        }
    })
}
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}