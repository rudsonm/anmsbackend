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