function EmailReg(Email) {
    var emailRegExp = new RegExp(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)
    if (emailRegExp.test(Email)) {
        return true
    }
    else { return false }
}
function PasswordRegExp(password) {
    var passwordlRegExp = new RegExp(/^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@@#$%&]).*$/);
    if (passwordlRegExp.test(password)) {
        return true;
    }
    else {
        return false;
    }
}
function CnicRegExp(cnic) {
    var cnicRegularExpression = new RegExp(/^[0-9]{13}$/);
    if (cnicRegularExpression.test(cnic)) {
        return true;
    }
    else {
        return false;
    }
}

function PhoneRegExp(phone) {
    var phoneRegularExpression = new RegExp(/^[0-9]{11}$/);
    if (phoneRegularExpression.test(phone)) {
        return true;
    }
    else {
        return false;
    }
}
function NumberRegExp(number) {
    var numberRegularExpression = new RegExp(/^[0-9]$/);
    if (numberRegularExpression.test(number)) {
        return true;
    }
    else {
        return false;
    }
}





 