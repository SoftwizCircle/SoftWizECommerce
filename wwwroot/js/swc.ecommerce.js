var SWC = SWC || {};
var SWC = (function () {

    function clearform(form) {
        $(form).find('input:text').val('')
        $(form).find('input:password').val('');
        $(form).find('input:checkbox').prop('checked', false);
    }

    var fnFormFailure = function (response) {
        alert("Error occured.");
    };

    var fnFormOnSuccess = function (response) {
        alert(response);
        clearform($(this));
    };

    return {
        fnFormFailure: fnFormFailure,
        fnFormOnSuccess: fnFormOnSuccess
    }
})();