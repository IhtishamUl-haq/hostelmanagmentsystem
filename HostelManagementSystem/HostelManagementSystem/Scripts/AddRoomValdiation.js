$('#roomAdd').on('submit', function (e) {
    if (AddRoomValidation() == false) {
        AddRoomValidation();
        e.preventDefault();
    }

});

function AddRoomValidation()
{
    var validate = true;
    var inputRoomNumber = $('#roomNumber').val();
    var inputRoomSize = $('#roomSize').val();
    var inputRoomRent = $('#roomRent').val();
    var inputRoomAvailable = $('#isRoomAvailable').val();
    /*var inputValidation = new RegExp(/^[0-9]$/);*/
    $('.Error').remove();

    if (inputRoomNumber.length < 1)
    {
        $('#roomNumber').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    //else if (NumberRegExp(inputRoomNumber) == false) {
    //    $('#roomNumber').after('<span class="Error" style="color:red">please enter valid number.</span>');

    //    validate = false;
    //}

    if (inputRoomSize.length < 1)
    {
        $('#roomSize').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    //else if (NumberRegExp(inputRoomSize) == false)
    //{
    //    $('#roomSize').after('<span class="Error" style="color:red">please enter valid  number.</span>');

    //    validate = false;
    //}

    if (inputRoomRent.length < 1)
    {
        $('#roomRent').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }
    //else if (NumberRegExp(inputRoomRent) == false)
    //{
    //    $('#roomRent').after('<span class="Error" style="color:red">please enter valid  number.</span>');

    //    validate = false;
    //}

    if (inputRoomAvailable.length < 1)
    {
        $('#isRoomAvailable').after('<span class="Error" style="color:red">This field is required.</span>');

        validate = false;
    }

    return validate
}