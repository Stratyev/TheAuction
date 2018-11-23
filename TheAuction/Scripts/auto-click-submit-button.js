
    //hide the button (it works, but with some delay)
//document.getElementById("addcheckmodal").style.display='none';

//auto click
$(document).ready(function () {
    $("#addcheckmodal").trigger('click');
});



// Just in case
//function submitParentForm(sender) {
//    var $formToSubmit = $(sender).closest('form');

//    $formToSubmit.submit();
//}