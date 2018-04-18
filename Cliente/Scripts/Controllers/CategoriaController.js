(function () {
    angular
        .module("Module")
        .controller("CategoriaController", CategoriaController);
    
    function CategoriaController(RestService, ToastService) {
        var categoriaVm = this;

        categoriaVm.editando = false;
        categoriaVm.irParaFormulario = irParaFormulario;
        categoriaVm.remover = remover;
        categoriaVm.salvar = salvar;

        _buscarCategorias();

        // Lista

        function _buscarCategorias() {
            let promise = RestService.buscar("categorias");
            promise.then(function (categorias) {
                categoriaVm.categorias = categorias;
            });            
        }

        function irParaFormulario(categoria) {
            categoriaVm.categoria = categoria || {};
            categoriaVm.editando = true;
        }

        function remover(id) {
            let promise = RestService.remover("categorias", id);
            promise.then(function () {
                ToastService.Send(200, "Categoria removida com sucesso");
                _buscarCategorias();
            });
        }

        // Formulário

        function salvar(categoria) {
            let promise = RestService.salvar("categorias", categoria);
            promise.then(function (response) {
                ToastService.Send(200, "Categoria salva com sucesso");
                if (!categoria.Id)
                    categoriaVm.categorias.push(response);
                categoriaVm.editando = false;
            });
        }        
    };
})();