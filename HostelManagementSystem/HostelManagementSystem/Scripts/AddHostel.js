



$('#hostelAdd').on('submit', function (e) {
    if (AddHostelValidation() == false) {
        AddHostelValidation();
        e.preventDefault();
    }
   

});
 

function AddHostelValidation()
{
    var validate = true;
    var inputHostelName = $('#hostelName').val();
    var inputHostelOwnerName = $('#hostelOwnerName').val();
    var inputHostelAddress = $('#hostelAddress').val();
    var inputHostelPhone = $('#hostelPhoneNo').val();
    var InputHostelPhoneExp = RegExp(/^[0-9]{11}$/);
    $('.Error').remove();
    if (inputHostelName.length < 1) {
        $('#hostelName').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    if (inputHostelOwnerName.length < 1) {
        $('#hostelOwnerName').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    if (inputHostelAddress.length < 1) {
        $('#hostelAddress').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    if (inputHostelPhone.length<1) {
        $('#hostelPhoneNo').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    } else
    if (!InputHostelPhoneExp.test(inputHostelPhone)) {
        $('#hostelPhoneNo').after('<span class="Error" style="color:red">Please enter valid Number.</span>');

        validate = false;
    }
    return validate


}

 