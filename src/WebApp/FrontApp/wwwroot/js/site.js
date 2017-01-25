function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function sendAuthorizationInputs(authForm) {
    var _login = authForm.login.value;
    var _password = authForm.password.value;
    var _client_id = getUrlParameter("client_id");
    var _redirect_uri = getUrlParameter("redirect_uri");

    $.post("Authorization/Code",
        {
            login: _login,
            password: _password,
            client_id: _client_id,
            redirect_uri: _redirect_uri
        })
        .done(function (resp) {
            if (resp.error === undefined)
                window.location.href = resp.uri;
            else
                alert("Error: " + resp.error + "\nError description: " + resp.error_description);
        })
        .fail(function () {
            alert("Request to server failed");
        })
        .error(function () {
            alert("Request to server failed");
        });
}
