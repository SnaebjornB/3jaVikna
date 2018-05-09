function decreaseQuantity(bookID) {
    var spanID = '#' + bookID;
    var temp = $(spanID).text();
    parseInt(temp);

    if(temp <= 1){

    }else{
        temp--;
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
            }
        });
    }
    
}

function increseQuantity(bookID){
    var spanID = '#' + bookID;
    var temp = $(spanID).text();
    parseInt(temp);

    temp++;
    $(spanID).text(temp);

    addToBasket(bookID);
    
}

function clearBasket(){
    $.ajax({
        url: '../../Order/clearBasket',
        type: 'POST',
        success: function () {
            $(".basketList").css( "display", "none" );
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
    var data = {
        bookID: BookID 
    };
    
    $.ajax({
            url: '../../Order/addToBasket',
            data : data,
            type: 'POST',
            dataType: 'json',
            success: function () {
            }
    });
}

$(function() {    		
    $(document).ready(function() {
        var rating = $('.stars').val();
        $('.span').html('<span class="stars">'+parseFloat($('stars').val())+'</span>');
        $('span.stars').stars();
    });    

    $.fn.stars = function() {
        return $(this).each(function() {
            $(this).html($('<span />').width(Math.max(0, (Math.min(5, parseFloat($(this).html())))) * 16));
        });
    }
});



$( document ).ready(function(){
    
    $("#forgottenPassBtn").click(function(){
        var temp = $('#emailInput').val(); //KEMUR ALLTAF TIL BAKA SEM UNDEFINED...
        var email = {
            email: temp
        };
        $.ajax({
            url: 'DoesEmailExist',
            method: 'GET',
            data: email,
            dataType: 'json',
            success: function (data) {
                handleEmailData(data);
            },
            error: function(err) {
                alert("error");
            }
        })
    })
    function handleEmailData(data){
        var emailStatusMessage = $('#emailStatusMessage');
        if(data == null) {
            emailStatusMessage.text('There is no account associated with that email');
            emailStatusMessage.css('color','red');
        }
        else {
            emailStatusMessage.text('A new password has been sent to your email!');
            emailStatusMessage.css('color','green');
        }
    }
})