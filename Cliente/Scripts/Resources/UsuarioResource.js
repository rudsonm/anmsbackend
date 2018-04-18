﻿(function () {
    angular
        .module("Module")
        .factory("UsuarioResource", UsuarioResource);

    function UsuarioResource(RestService, $httpParamSerializerJQLike) {
        return {
            buscar: buscar,
            buscarUm: buscarUm,
            salvar: salvar,
            remover: remover,
            autenticar: autenticar
        }

        function buscar(parametros) {
            return RestService.buscar("usuarios", parametros);
        }

        function buscarUm(id) {
            return RestService.buscarUm("usuarios", id);
        }

        function salvar(usuario) {
            return RestService.salvar("usuarios", usuario);
        }

        function remover(id) {
            return RestService.remover("usuarios", id);
        }

        function autenticar(usuario) {
            return RestService.Restangular.all("usuarios/autenticar").post(
                $httpParamSerializerJQLike({
                    grant_type: 'password',
                    username: usuario.CpfCnpj,
                    password: usuario.Senha
                }),
                {},
                { 'Content-Type': 'application/x-www-form-urlencoded' }
            );
        }               
    }
})();