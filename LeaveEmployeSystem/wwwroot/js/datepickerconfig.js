$(function () {
    $('.datetimepicker').datetimepicker({
        format: 'YYYY-MM-DD HH:mm:ss'
    });

    $('.datepicker').datetimepicker({
        format: 'YYYY-MM-DD'
    });

    $('#timepicker').datetimepicker({
        format: 'HH:MM:SS'
    });
});
