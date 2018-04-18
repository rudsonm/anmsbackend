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