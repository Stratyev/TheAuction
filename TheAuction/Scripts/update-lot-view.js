$(function () {

    // Ссылка на автоматически-сгенерированный прокси хаба
    var chat = $.connection.indexLotHub;

    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.addMessage = function () {
        // Добавление сообщений на веб-страницу 
        $('#chatroom').append('<p><b> Превед </b></p>');
    };

    // Функция, вызываемая при подключении нового пользователя
    //chat.client.onConnected = function (id, allUsers) {

    //    // установка в скрытых полях имени и id текущего пользователя
    //    $('#hdId').val(id);
    //    // Добавление всех пользователей
    //    for (i = 0; i < allUsers.length; i++) {

    //        AddUser(allUsers[i].ConnectionId);
    //    }
    //}
    //$("#btnLogin").click(function () {

    //     chat.server.connect();
    //});

    //// Добавляем нового пользователя
    //chat.client.onNewUserConnected = function (id) {

    //    //AddUser(id);
    //}

    //// Удаляем пользователя
    //chat.client.onUserDisconnected = function (id) {

    //    $('#' + id).remove();
    //}

    // Открываем соединение
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            chat.server.send();
            //$('#counter').val('');
        });
    });
});
// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//function htmlNumEncode(value) {
//    var encodedValue = $('<div />').number(value).html();
//    return encodedValue;
//}

//Добавление нового пользователя
//function AddUser(id) {

//    var userId = $('#hdId').val();

//    if (userId != id) {

//        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
//    }
//}