var SWC = SWC || {};
var SWC = (function () {

    function clearform(form) {
        $(form).find('input:text').val('')
        $(form).find('input:password').val('');
        $(form).find('input:checkbox').prop('checked', false);
    }

    var fnFormOnFailure = function (response) {
        toastr.error('Error occured.', 'Error')
    };

    var fnFormOnSuccess = function (response) {
        switch (response.flag) {

            case true:
                switch (response.type) {
                    case 'redirect':
                        window.location = response.message;
                        break;
                    default:
                        toastr.success(response.message, 'Success', { timeOut: 5000 });
                        clearform($(this));
                }

                break;
            case false:
                toastr.warning(response.message, 'Error');
                break;
        }
    };

    return {
        fnFormOnFailure: fnFormOnFailure,
        fnFormOnSuccess: fnFormOnSuccess
    }
})();