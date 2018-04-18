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