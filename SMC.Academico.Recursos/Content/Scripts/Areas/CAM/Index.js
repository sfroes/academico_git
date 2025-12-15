(function (smc, $) {

    (function (sga) {
        $(document).on('ready', function () {
            //$("#formPesquisaCampanhaOferta").on("submit", function () {
            //    smc.conditionalBehavior.destroy($("#btnConfigVaga"));
            //});          
            $("[id^=formPesquisa]").on("submit", function () {
                //Reseta o conditional dos botões com prefixo "btn" ao efetuar um novo filtro
                smc.conditionalBehavior.destroy($("[id^=btn]"));
            });
        });
    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);