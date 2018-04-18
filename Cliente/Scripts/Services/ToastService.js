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