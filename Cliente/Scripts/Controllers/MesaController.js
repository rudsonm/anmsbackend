(function () {
    angular
        .module("Module")
        .controller("MesaController", MesaController);
    
    function MesaController(RestService, ToastService) {
        var mesaVm = this;

        mesaVm.editando = false;
        mesaVm.irParaFormulario = irParaFormulario;
        mesaVm.remover = remover;
        mesaVm.salvar = salvar;

        _buscarMesas();

        // Lista

        function _buscarMesas() {
            let promise = RestService.buscar("mesas");
            promise.then(function (mesas) {
                mesaVm.mesas = mesas;
                mesas.forEach(function (mesa) {
                    mesa.Comandas = _buscarComandas(mesa);
                });
            });
        }

        function _buscarComandas(mesa) {
            let promise = RestService.buscar("comandas", { mesa: mesa.Id });
            promise.then(function (comandas) {
                mesa.Comandas = comandas;
            });
        }

        function irParaFormulario(mesa) {
            mesaVm.mesa = mesa || {};
            mesaVm.editando = true;
        }

        function remover(id) {
            let promise = RestService.remover("mesas", id);
            promise.then(function () {
                ToastService.Send("Mesa removida com sucesso");
                _buscarMesas();
            });
        }

        // Formulário

        function salvar(mesa) {
            let promise = RestService.salvar("mesas", mesa);
            promise.then(function (response) {
                ToastService.Send("Mesa salva com sucesso");
                if (!mesa.Id)
                    mesaVm.mesas.push(response);
                mesaVm.editando = false;
            });
        }
    };
})();