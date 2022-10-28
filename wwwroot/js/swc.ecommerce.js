var SWC = SWC || {};
var SWC = (function () {

    function clearform(form) {
        $(form).find('input:text').val('')
        $(form).find('input:password').val('');
        $(form).find('input:checkbox').prop('checked', false);
    }

    var fnFormFailure = function (response) {
        toastr.error('Error occured.', 'Error')
    };

    var fnFormOnSuccess = function (response) {
        toastr.success(response, 'Success');
        clearform($(this));
    };

    return {
        fnFormFailure: fnFormFailure,
        fnFormOnSuccess: fnFormOnSuccess
    }
})();