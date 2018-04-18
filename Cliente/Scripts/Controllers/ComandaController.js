(function () {
    angular
        .module("Module")
        .controller("ComandaController", ComandaController);

    function ComandaController(RestService, ToastService, $mdDialog) {
        var comandaVm = this;

        comandaVm.editando = false;
        comandaVm.salvar = salvar;
        comandaVm.abrirModalConsumo = abrirModalConsumo;

        _buscarComandas();
        _buscarMesas();

        function salvar(comanda) {
            let promise = RestService.salvar("comandas", comanda);
            promise.then(function (response) {
                ToastService("Comanda salva com sucesso");
                comandaVm.comandas.push(response);
            });
        }

        function abrirModalConsumo(comanda, event) {
            $mdDialog.show({
                controller: 'ConsumoController',
                controllerAs: 'consumoVm',
                templateUrl: 'Templates/Consumo.html',
                parent: angular.element(document.body),
                targetEvent: event,
                clickOutsideToClose: true,
                fullscreen: false,
                locals: {
                    Comanda: comanda
                }
            });
        }

        function _buscarMesas() {
            let promise = RestService.buscar("mesas");
            promise.then(function (mesas) {
                comandaVm.mesas = mesas;
            });
        }

        function _buscarComandas() {
            let promise = RestService.buscar("comandas");
            promise.then(function (comandas) {
                comandaVm.comandas = comandas;
            });
        }
    };
})();