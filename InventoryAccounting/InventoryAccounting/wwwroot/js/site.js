// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#modal-container').modal('handleUpdate');
$(function () {
    // when the modal is closed
    $('#modal-container').on('hidden.bs.modal', function () {
        // remove the bs.modal data attribute from it
        $(this).removeData('bs.modal');
        // and empty the modal-content element
        $('#modal-container .modal-content').empty();
    });
});
// boostrap 4 load modal example from docs
$('#modal-container').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var url = button.attr("href");
    var modal = $(this);

    // note that this will replace the content of modal-content ever time the modal is opened
    modal.find('.modal-content').load(url);
});