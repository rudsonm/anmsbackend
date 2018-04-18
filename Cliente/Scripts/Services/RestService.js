(function () {
    angular
        .module("Module")
        .service("RestService", RestService);

    function RestService(Restangular, ToastService, RequestsListenerService) {

        this.Restangular = Restangular;
        this.buscar = buscar;
        this.buscarUm = buscarUm;
        this.salvar = salvar;
        this.remover = remover;

        Restangular.addRequestInterceptor(function (element, operation, what, url) {
            RequestsListenerService.adicionarRequisicao();
            return element;
        });

        Restangular.addResponseInterceptor(function (data, operation, what, url, response, deferred) {
            RequestsListenerService.removerRequisicao();
            return data;
        });

        Restangular.addErrorInterceptor(function (response, deferred, responseHandler) {
            console.log(response);
            console.log(deferred);
            console.log(responseHandler);
            RequestsListenerService.removerRequisicao();
            if (response.status !== 200) {
                ToastService.Send((response.status > 0) ? response.status : 500, "");
                return false;
            }
            return true;
        });

        function buscar(rota, parametros) {
            parametros = parametros || {};
            return Restangular.all(rota).getList(parametros);
        }

        function buscarUm(rota, id) {
            return Restangular.one(rota, id).get();
        }

        function salvar(rota, entidade) {
            if (!entidade.Id)
                return _adicionar(rota, entidade);
            return _editar(entidade);
        }

        function _adicionar(rota, entidade) {
            return Restangular.all(rota).post(entidade);
        }

        function _editar(entidade) {
            entidade.id = entidade.Id || entidade.id;
            return entidade.put();
        }

        function remover(rota, id) {
            return Restangular.one(rota, id).remove();
        }
    }
})();