function validateEmail() {
    var pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var email = document.getElementById("Email").value;
    if (!email.match(pattern)) {
        mailId.innerHTML = "<span style='color: red;'>Invalid EmailId</span>";
    }
    else {
        mailId.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}

function validateUserName() {
    var pattern = "^[a-zA-Z ]*$";
    var name = document.getElementById("Name").value;
    if (!name.match(pattern)) {
        userName.innerHTML = "<span style='color: red;'>Invalid UserName</span>";
    }
    else {
        userName.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}

function validateMobileNumber() {
    var number = document.getElementById("MobileNumber").value;
    if (!number.match("^[789]+[0-9]{9}$")) {
        mobileNumber.innerHTML = "<span style='color: red;'>Invalid Mobile Number</span>";
    }
    else {
        mobileNumber.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}
function validatePassword() {
    var pattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    var number = document.getElementById("Password").value;
    if (!number.match(pattern)) {
        password.innerHTML = "<span style='color: red;'>Invalid Password<br>" +
            "At least one lower case letter,<br>" +
            "At least one upper case letter,<br>" +
            "At least one number,<br>At least 6 characters length</span > ";
    }
    else {
        password.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}

function validateConfirmPassword() {
    var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("ConfirmPassword").value;
    if (password != confirmPassword) {
        confirmpassword.innerHTML = "<span style='color: red;'>Password doesn't Matched</span>";
    }
    else {
        confirmpassword.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}
function validateAnswer() {
    var pattern = /^[a-zA-Z0-9!@#\$%\^\&*\)\(+=._-]{1,}$/g;
    var answer = document.getElementById("Answer").value;
    if (!answer.match(pattern)) {
        checkAnswer.innerHTML = "<span style='color: red;'>Please enter your answer.</span>";
    }
    else {
        checkAnswer.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}
function validateEditAnswer() {
    var pattern = /^[a-zA-Z0-9!@#\$%\^\&*\)\(+=._-]{1,}$/g;
    var answer = document.getElementById("EditAnswer").value;
    if (!answer.match(pattern)) {
        checkEditAnswer.innerHTML = "<span style='color: red;'>Please enter your answer.</span>";
    }
    else {
        checkEditAnswer.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}
function displayForm() {
    var x = document.getElementById("editContent");
    if (x.style.display === "none") {
        x.style.display = "block";
    }
    else {
        x.style.display = "none";
    }
}

function validateQuestion() {
    var pattern = /^[a-zA-Z0-9!@#\$%\^\&*\)\(+=._-]{1,}$/g;
    var name = document.getElementById("QuestionName").value;
    if (!name.match(pattern)) {
        checkQuestion.innerHTML = "<span style='color: red;'>Invalid Question</span>";
    }
    else {
        checkQuestion.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}


function validateCategory() {
    var category = document.getElementById("drpCategory").value;
    if (category == null) {
        checkQuestion.innerHTML = "<span style='color: red;'>Category can't be empty</span>";
    }
    else {
        checkQuestion.innerHTML = "<span style='color: green;'>Looks good!</span>";
    }
}