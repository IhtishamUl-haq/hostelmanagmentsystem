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
    var phoneRegExp = new RegExp(/^[0-9]$/);
    $('.Error').remove();
    if (inputHostelName.length < 1)
    {
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
    
    if (!phoneRegExp.test(inputHostelPhone)){
        $('#hostelPhoneNo').after('<span class="Error" style="color:red">please enter valid phone number.</span>');

        validate = false;
    }
    return validate
}

