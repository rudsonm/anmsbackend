angular
    .module('Module')
    .controller('PedidoController', function (RestService) {
        var pedidoVm = this;

        setTimeout(function () {
            _buscarConsumos();
        }, 500);        

        function _buscarConsumos() {            
            let promise = RestService.buscar("consumos");
            promise.then(function (consumos) {
                pedidoVm.consumos = _buildPedidoGrid(consumos);
                setTimeout(function () {
                    console.log(_buildPedidoGrid(consumos));
                });                
            });
        }

        function _buildPedidoGrid(consumos) {
            consumos = consumos.filter(function(consumo, indice) {
                consumo.span = {
                    row: 1,
                    col: 1
                };
                
                if (indice <= 3) {
                    consumo.span.row = indice;
                    consumo.span.col = indice - 1;                    
                }

                return consumo;
            });
            return consumos.orderBy(function (consumo) {
                consumo.span.col;
            });
        }
    });