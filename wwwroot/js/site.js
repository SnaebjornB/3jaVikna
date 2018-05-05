// Write your JavaScript code.
function check(input) {
    if (input.value != document.getElementById('password').value) {
        input.setCustomValidity('Password Must be Matching.');
    } else {
        input.setCustomValidity('');
    }
} 
$(document).ready(function (){
    $('#DoesEmailExist').click(function() {
        var email = $('#checkEmail').val(); //KEMUR ALLTAF TIL BAKA SEM UNDEFINED...
        //console.log(email); 

        $.ajax({
            url: 'DoesEmailExist',
            method: 'POST',
            data: {email : email},
            dataType: 'bool',
            succcess: function (data) {
                console.log("stuff");

                var emailStatusMessage = $('emailStatusMessage');
                if(!data) {
                    emailStatusMessage.text('There is no account associated with that email');
                    emailStatusMessage.css('color','red');
                }
                else {
                    emailStatusMessage.text('A new password has been sent to ' + email);
                    emailStatusMessage.css('color','green');
                }
            },
            error : function(err) {
                alert(err);
            }
        })
    })
})
