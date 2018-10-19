function drowMessage(count) {

    if (count !== 0) {
        var a = $('#messages').find('a');
        var div = a.find('.top-counter');
        if (!div.length) {
            div = $('<div></div>').addClass('top-counter');
            div.text(count);
            a.prepend(div);
        }
        div.text(count);

    }
    else {
        var li = $('.message-count-icon-pos').remove();
    }
}

function drowMessageInDialog(companionAvatar, companionName, count, lastMessage, newDate, dialogId) {

    var trId = '#message-row-' + dialogId;

    var tr = $(trId);
    tr.addClass('warning-row');
    var tdText = tr.find('#dialog-text');
    var tdDate = tr.find('#dialog-date');
    //Html.Action("Photo", "Profile", new { UserId'+ companionId +' = message.SenderId })

    tdText.text(lastMessage);
    tdDate.text(newDate);

    var div = tr.find('#div-message-counter');
    if (div.find(".dialog-message-counter").length > 0) {
        span = $('.dialog-message-counter');
        span.text(count);
    }
    else {
        span = $('<span></span>').addClass('dialog-message-counter');
        span.text(count);
        div.append(span);
    }
    
    







}

$(function () {
    var messageHub = $.connection.messageHub;

    messageHub.client.updateMessage = function (counter) {

        drowMessage(counter);

    };

    messageHub.client.updateMessageInDialog = function (userName, companionId, companionName, counter, lastMessage, date, dialogId) {
        
        drowMessageInDialog(userName, companionId, companionName, counter, lastMessage, date, dialogId);
        //$(".clickable").each(function () {
        //    $(this).on('click', function (e) {
        //        var id = $(this).data('id');
        //        location.href = "/dialog/details?dialogId=" + id;
        //    });
        //});
        //SetDeleteDialogClickable();
    };

    messageHub.client.addMessage = function (receiverName, senderName, messageBody, date, senderImage) {
        var messagesContainer = $('#messages-col');
        var messageBlock = $('#message-block-element').last().clone();

        messageBlock.find('#message-block-img').attr('src', '/Content/Images/Avatars/' + senderImage);
        messageBlock.find('#sender-name').text(senderName);
        messageBlock.find('#created-date').text(date);
        messageBlock.find('#message-body').text(messageBody);
        messageBlock.removeAttr('style');
        messagesContainer.append(messageBlock);

        $('#messageBody').val('');
        var objDiv = $('#messages-col');
        objDiv.scrollTop(objDiv[0].scrollHeight);


    };

    messageHub.client.addDialog = function (dialogId, companionAvatar, companionName) {

        var dialogsTable = $('#dialogs-table');
        var dialogBlock = $('#dialog-block').clone();
        dialogBlock.attr("data-href", '/User/Dialog/Details?id=' + dialogId);
        dialogBlock.on("click", function () {
            document.location = $(this).data('href');
        });
        var compImg = dialogBlock.find('#dialog-block-img');
        var compName = dialogBlock.find('#dialog-companion-name');
        compName.text(companionName);
        compImg.attr('src', '/Content/Images/Avatars/' + companionAvatar);
        dialogBlock.attr('id', 'message-row-' + dialogId);

        dialogBlock.removeAttr('style');

        dialogsTable.append(dialogBlock);

    };
    $.connection.hub.start();


});