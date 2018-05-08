// Write your JavaScript code.
function check(input) {
    if (input.value != document.getElementById('password').value) {
        input.setCustomValidity('Password Must be Matching.');
    } else {
        input.setCustomValidity('');
    }
} 

function addToBasket(BookID) {
    console.log(BookID);
    var data = {
        bookID: BookID 
    };
    
    $.ajax({
            url: '../../Order/addToBasket',
            data : data,
            type: 'POST',
            dataType: 'json',
            success: function () {
                 console.log('success');
            }
    });
}

function decreaseQuantity(bookID) {
    console.log(bookID);
}


/*$(document).ready(function (){    Fuck this shit I'm out!
    function doesEmailExist(event) {
        event.PreventDefault();
        var email = $('#email').val(); //KEMUR ALLTAF TIL BAKA SEM UNDEFINED...
        console.log("stuff"); 
        if(email != undefined) {
            $.ajax({
                url: 'DoesEmailExist',
                method: 'POST',
                async: false,
                data: {email : email},
                dataType: 'bool',
                succcess: function (data) {
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
                    alert("error");
                }
            })
        }
        else {
            console.log("shit");
        }
    }
})*/
