angular
    .module('Module', ['restangular', 'ngMaterial', 'md.data.table', 'ngMessages', 'ngResource', 'ngRoute'])
    .config(function (RestangularProvider, $mdThemingProvider) {

        RestangularProvider.setBaseUrl('http://localhost:49664/api/');
        var token = sessionStorage.webmenu_generated_token;
        if (token)
            RestangularProvider.setDefaultHeaders({ 'Authorization': token });

        $mdThemingProvider.theme('default')
        //     .primaryPalette('grey', {
        //         'default': '900'
        //     })
        //     .accentPalette('grey', {
        //         'default': '700'
        //     })
        //    .dark();
            .primaryPalette('blue')
            .accentPalette('brown', {
                'default': '600'
            });
    });
(function () {
    angular
        .module("Module")
        .service("ToastService", ToastService);
    
    function ToastService($mdToast) {

        this.Send = show;

        function show(status, message) {
            let toast = _getCustomToast(status, message);
            $mdToast.show(toast);
        }

        function _getCustomToast(status, message) {
            switch (status) {
                case 200:
                    return _success(message);
                case 400:
                    return _badRequest(message);
                case 404:
                    return _notFound(message);
                case 403:
                    return _notAuthorized(message);
                case 500:
                    return _serverError(message);
                default:
                    return _info(status);
            }
        }

        function _info(message) {
            return _getToast(message);
        }

        function _success(message) {
            message = message || "Alterações salvas com sucesso";
            return _getToast(message, "done", "green");            
        }

        function _badRequest(message) {
            message = message || "Requisição inválida";
            return _getToast(message, "warning", "orange");
        }

        function _notFound(message) {
            message = message || "Recurso não encontrado";
            return _getToast(message, "warning", "orange");
        }

        function _notAuthorized(message) {
            message = message || "Você não possui acesso";
            return _getToast(message, "clear", "red");
        }

        function _serverError(message) {
            message = message || "Erro interno, tente novamente mais tarde";
            return _getToast(message, "cloud_off", "blue");
        }

        function _getToast(message, icon, color) {
            return {
                hideDelay: 3000,
                position: "top right",
                templateUrl: "Templates/Toast.html",
                locals: {
                    content: message,
                    icon: icon || null,
                    color: color || null
                },
                controllerAs: 'toastVm',
                controller: function ($mdToast, content, icon, color) {
                    toastVm = this;
                    toastVm.content = content;
                    toastVm.icon = icon;
                    toastVm.color = color;
                    toastVm.close = $mdToast.hide;
                }
            };
        }
    };
})();
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
(function () {
    angular
        .module("Module")
        .service("RequestsListenerService", RequestsListenerService);

    function RequestsListenerService() {

        this.requisicoesPendentes = requisicoesPendentes;
        this.adicionarRequisicoes = adicionarRequisicoes;
        this.adicionarRequisicao = adicionarRequisicao;
        this.removerRequisicoes = removerRequisicoes;
        this.removerRequisicao = removerRequisicao;
        this.zerarRequisicoes = zerarRequisicoes;

        var requisicoesAtivas = 0;

        function requisicoesPendentes() {
            return requisicoesAtivas;
        }

        function adicionarRequisicao() {
            adicionarRequisicoes(1);
        }

        function adicionarRequisicoes(n) {
            requisicoesAtivas += n || 1;
        }

        function removerRequisicao() {
            removerRequisicoes(1);
        }

        function removerRequisicoes(n) {
            requisicoesAtivas -= n || 1;
            if (requisicoesAtivas < 0)
                zerarRequisicoes();
        }

        function zerarRequisicoes() {
            requisicoesAtivas = 0;
        }
    }
})();
(function () {
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
(function () {
    angular
        .module("Module")
        .factory("ProdutoResource", ["$resource", function ($resource) {
            return $resource("http://localhost:49664/api/produtos/:id",
                {
                    id: "@id"
                },
                {
                    buscar: {
                        method: "GET",
                        isArray: true
                    },

                    buscarUm: {
                        method: "GET",
                        params: { id: "@id" },
                        isArray: false
                    },

                    salvar: {
                        method: "POST",
                        isArray: false
                    },

                    editar: {
                        method: "PUT",
                        params: { id: "@id" },
                        isArray: false
                    },

                    remover: {
                        method: "DELETE",
                        params: { id: "@id" },
                        isArray: false
                    }

                });
        }]);
})();
(function () {
    angular
        .module("Module")
        .factory("MesaResource", ["$resource", function ($resource) {
            return $resource("http://localhost:49664/api/mesas/:id",
                {
                    id: "@id"
                },
                {
                    buscar: {
                        method: "GET",
                        isArray: true
                    },

                    buscarUm: {
                        method: "GET",
                        params: { id: "@id" },
                        isArray: false
                    },

                    salvar: {
                        method: "POST",
                        isArray: false
                    },

                    editar: {
                        method: "PUT",
                        params: { id: "@id" },
                        isArray: false
                    },

                    remover: {
                        method: "DELETE",
                        params: { id: "@id" },
                        isArray: false
                    }

                });
        }]);
})();
(function () {
    angular
        .module("Module")
        .factory("ComandaResource", ["$resource", function ($resource) {
            return $resource("http://localhost:49664/api/comandas/:id",
                {
                    id: "@id"
                },
                {
                    buscar: {
                        method: "GET",
                        isArray: true
                    },

                    buscarUm: {
                        method: "GET",
                        params: { id: "@id" },
                        isArray: false
                    },

                    salvar: {
                        method: "POST",
                        isArray: false
                    },

                    editar: {
                        method: "PUT",
                        params: { id: "@id" },
                        isArray: false
                    },

                    remover: {
                        method: "DELETE",
                        params: { id: "@id" },
                        isArray: false
                    }

                });
        }]);
})();
(function () {
    angular
        .module("Module")
        .factory("CategoriaResource", ["$resource", function ($resource) {
            return $resource("http://localhost:49664/api/categorias/:id",
                {
                    id: "@id"
                },
                {
                    buscar: {
                        method: "GET",
                        isArray: true
                    },

                    buscarUm: {
                        method: "GET",
                        params: { id: "@id" },
                        isArray: false
                    },

                    salvar: {
                        method: "POST",
                        isArray: false
                    },

                    editar: {
                        method: "PUT",
                        params: { id: "@id" },
                        isArray: false
                    },

                    remover: {
                        method: "DELETE",
                        params: { id: "@id" },
                        isArray: false
                    }

                });
        }]);
})();
(function () {
    angular
        .module("Module")
        .filter("telefone", function () {
            return function (telefone) {
                var over = telefone.length - 10;
                var ddd = telefone.slice(0, 2);
                var a = telefone.slice(2, 6 + over);
                var b = telefone.slice(6 + over, 10 + over);

                return "(" + ddd + ") " + a + "-" + b;
            };
        });
})();
(function () {
    angular
        .module("Module")
        .directive("ngTooltip", [function () {
            return {                
                restrict: "A",
                compile: function (elemento, atributos) {
                    let content = atributos.ngTooltip;
                    let direction = atributos.ngDirection || 'left';
                    let tooltip = "<md-tooltip data-md-delay='50' data-md-direction='" + direction + "'>" + content + "</md-tooltip>";
                    elemento.append(tooltip);
                }
            }
        }]);
})();
(function () {
    angular
        .module("Module")
        .directive("ngMask", [function () {
            return {
                restrict: "A",
                link: function (scope, element, attribute) {
                    switch (attribute.ngMask) {
                        case "date":
                            $(element).mask("00/00/0000");
                            break;
                        case "time":
                            $(element).mask("00:00");
                            break;
                        case "phone":
                            $(element).mask("(00) 00000-0000");
                            break;
                        case "money":
                            $(element).mask("0,000,000.00", { reverse: true });
                            break;
                        case "cpf":
                            $(element).mask("000.000.000-00");
                            break;
                    }
                }
            }
        }]);
})();
(function () {
    angular
        .module("Module")
        .directive('ngEnter', function () {
            return function (scope, element, attrs) {
                element.bind("keydown keypress", function (event) {
                    if (event.which === 13) {
                        scope.$apply(function () {
                            scope.$eval(attrs.ngEnter, { 'event': event });
                        });

                        event.preventDefault();
                    }
                });
            };
        });
})();
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
(function () {
    angular
        .module("Module")
        .controller("MainController", MainController);
    
    function MainController($mdSidenav, $http, RequestsListenerService) {
        mainVm = this;

        mainVm.toggleSidenav = toggleSidenav;
        mainVm.requisicoesPendentes = RequestsListenerService.requisicoesPendentes;

        _setDefaultAuthenticationHeader();

        function toggleSidenav() {
            $mdSidenav("side-nav").toggle();
        }        

        function _setDefaultAuthenticationHeader() {
            var token = sessionStorage.webmenu_generated_token;
            if (token)
                $http.defaults.headers.common["Authorization"] = token;
        }
    };
})();
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