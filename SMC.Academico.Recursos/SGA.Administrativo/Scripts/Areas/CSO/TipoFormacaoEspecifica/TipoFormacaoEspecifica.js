(function (smc, $) {

    (function (sga) {
        $(document).on('smcload', function () {

            $('input[name=ExigeGrau]').click(function () {
                if ($(this).val()) {
                    $('input[name=PermiteTitulacao]').each(function () {
                        $(this).attr("checked", false);
                    });

                    $('input[name=ExibeGrauDescricaoFormacao]').each(function () {
                        $(this).attr("checked", false);
                    });
                }
            });
        })
    })(smc.sga = smc.sga || {});

})(window.smc = window.smc || {}, jQuery);