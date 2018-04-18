(function () {
    angular
        .module("Module")
        .controller("UsuarioController", UsuarioController);
    
    function UsuarioController(UsuarioResource, ToastService) {
        usuarioVm = this;

        usuarioVm.salvar = salvar;
        usuarioVm.autenticar = autenticar;

        function salvar(usuario) {
            let promise = UsuarioResource.salvar(usuario)
            promise.then(function (response) {
                ToastService.Send(200, "Usuário salvo com sucesso");
            });
        }

        function autenticar(usuario) {
            let promise = UsuarioResource.autenticar(usuario);
            promise.then(function (response) {
                var token = response.token_type + " " + response.access_token;
                sessionStorage.webmenu_generated_token = token;
                ToastService.Send(200, "Autenticação concluída com sucesso");
                usuarioVm.usuario = {};
            });
        }
    };
})();