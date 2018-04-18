(function () {
    angular
        .module("Module")
        .controller("ProdutoController", ProdutoController);
    
    function ProdutoController(RestService, ToastService) {
        var produtoVm = this;

        produtoVm.editando = false;
        produtoVm.buscarProdutos = buscarProdutos;
        produtoVm.buscarProduto = buscarProduto;
        produtoVm.irParaFormulario = irParaFormulario;
        produtoVm.remover = remover;
        produtoVm.salvar = salvar;        

        _buscarCategorias();
        buscarProdutos();

        // Lista

        function _buscarCategorias() {
            let promise = RestService.buscar("categorias");
            promise.then(function (categorias) {
                produtoVm.categorias = categorias;
            });
        }

        function buscarProdutos() {
            let promise = RestService.buscar("produtos");
            promise.then(function (produtos) {
                produtoVm.produtos = produtos;
            });
        }

        function buscarProduto(id) {
            let promise = RestService.buscarUm("produtos", id);
            promise.then(function (produto) {
                return produtoVm.irParaFormulario(produto);
            });            
        }

        function irParaFormulario(produto) {
            produtoVm.produto = produto || {};
            produtoVm.editando = true;
        }

        function remover(produto) {
            let promise = RestService.remover("produtos", produto.Id);
            promise.then(function () {
                produtoVm.buscarProdutos();
                ToastService.Send(200, "Produto salvo com sucesso");
            });
        }

        // Formulário

        function salvar(produto) {
            let promise = RestService.salvar("produtos", produto);
            promise.then(function (response) {
                buscarProdutos();
                ToastService.Send(200, "Produto salvo com sucesso");
                produtoVm.editando = false;
            });
        }
    };
})();