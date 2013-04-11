$(function () {
    $('#logout').click(function () {
        var form = $(this).parent();
        form.submit();
    });
});