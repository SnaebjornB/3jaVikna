﻿function decreaseQuantity(bookID) {
    console.log(bookID);
    var spanID = '#' + bookID;
    var temp = $(spanID).text();
    parseInt(temp);

    if(temp <= 1){

    }else{
        temp--;
        console.log(temp);
        $(spanID).text(temp);
        
        var data = {
            bookID: bookID
        };
        $.ajax({
            url: '../../Order/deleteItemFromBasket',
            data : data,
            async: false,
            type: 'POST',
            dataType: 'json',
            success: function () {
                 console.log('success');
            }
        });
    }
    
}

function increseQuantity(bookID){
    console.log(bookID);
    console.log(bookID);
    var spanID = '#' + bookID;
    var temp = $(spanID).text();
    parseInt(temp);

    temp++;
    console.log(temp);
    $(spanID).text(temp);

    addToBasket(bookID);
    
}

function clearBasket(){
    $.ajax({
        url: '../../Order/clearBasket',
        type: 'POST',
        success: function () {
            $(".basketList").css( "display", "none" );
            console.log('success');
        }
    });
}

function removeFromBasket(bookID, ID){
    var data = {
        bookID: bookID
    };
    console.log(ID);
    var rowID = "#" + ID;
    console.log(rowID);
    $.ajax({
        url: '../../Order/clearBookCopies',
        data : data,
        type: 'POST',
        dataType: 'json',
        success: function () {
            $(rowID).css( "display", "none" );
            console.log('success');
        }
    });
}

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

$(function() {    		
    $(document).ready(function() {
        var rating = $('.stars').val();
        $('.span').html('<span class="stars">'+parseFloat($('stars').val())+'</span>');
        console.log('stars');
        $('span.stars').stars();
    });    		
    $(document).ready();
});

$.fn.stars = function() {
    return $(this).each(function() {
        $(this).html($('<span />').width(Math.max(0, (Math.min(5, parseFloat($(this).html())))) * 16));
    });
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
