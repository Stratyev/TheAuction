

    $(document).ready(function () {

        $("#Description").blur(function () {

            var params = new Object();
            params.UserName = $("#Description").val().trim();

            $.ajax(
            {
                type: "POST",
                url: "/Home/CheckDescription",
                data: $.toJSON(params),
                contentType: "application/json",
                success: function (response) {

                    if (response == false) {
                        alert('s');
                        $("#message").html("Неверное описание");
                    }
                }
            });
        });

    });
