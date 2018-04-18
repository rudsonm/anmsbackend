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