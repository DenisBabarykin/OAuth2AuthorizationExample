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

    $.post("Authorization/Token",
        {
            login: _login,
            password: _password,
            client_id: _client_id,
            redirect_uri: _redirect_uri
        })
        .done(function (resp) { window.location.href = resp.uri; })
        .fail(function () { alert("Incorrect input data"); });
}
