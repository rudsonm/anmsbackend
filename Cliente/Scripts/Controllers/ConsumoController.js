(function () {
    angular
        .module("Module")
        .controller("ConsumoController", ConsumoController);

    function ConsumoController(RestService, Comanda, ToastService, $mdDialog, $mdSidenav) {
        var consumoVm = this;

        consumoVm.fechar = fechar;
        consumoVm.irParaFormulario = irParaFormulario;
        consumoVm.fecharFormulario = fecharFormulario;
        consumoVm.obterTotalAdicionais = obterTotalAdicionais;
        consumoVm.obterPrecoTotal = obterPrecoTotal;
        consumoVm.buscarProdutos = buscarProdutos;        
        consumoVm.salvar = salvar;

        _buscarComanda(Comanda);

        var _consumo = {
            Comanda: Comanda,
            Produto: {
                Id: null                
            },
            Categoria: {
                Id: null
            }
        };        

        function fechar() {
            $mdDialog.cancel();
        }

        function irParaFormulario() {
            _buscarCategorias();
            consumoVm.consumo = angular.copy(_consumo);
            $mdSidenav('consumo-sidenav').toggle();
        }

        function fecharFormulario() {
            $mdSidenav('consumo-sidenav').toggle();
        }

        function obterTotalAdicionais(consumos) {
            return consumos.sum(function (c) {
                return c.Adicionais.length;
            });
        }

        function obterPrecoTotal(consumos) {
            return consumos.sum(function (c) {
                return c.Produto.Preco;
            });
        }

        function _agruparConsumos(consumos) {
            return consumos.groupBy(function (c) {
                return c.Produto.Id;
            });
        }

        function _buscarCategorias() {
            let promise = RestService.buscar("categorias", { tipo: "PRODUTO" });
            promise.then(function (categorias) {
                consumoVm.categorias = categorias;
            });            
        }

        function buscarProdutos(categoria) {
            let promise = RestService.buscar("produtos", { categoria: categoria.Id });
            promise.then(function (produtos) {
                consumoVm.produtos = produtos;
            });
        }

        function _buscarComanda(comanda) {
            comanda = comanda || consumoVm.comanda;
            let promise = RestService.buscarUm("comandas", comanda.Id);
            promise.then(function (response) {
                consumoVm.comanda = response;
                if (response.Consumos.length) {
                    consumoVm.grupoConsumos = _agruparConsumos(response.Consumos);
                } else {
                    irParaFormulario();
                }                
            });
        }        

        function salvar(consumo) {
            let promise = RestService.salvar("consumos", consumo);
            promise.then(function (response) {
                ToastService.Send(200, "Consumo salvo com sucesso");
                consumoVm.comanda.Consumos.push(consumo);
                consumoVm.grupoConsumos = _agruparConsumos(consumoVm.comanda.Consumos);
                fecharFormulario();
            });
        }
    };
})();