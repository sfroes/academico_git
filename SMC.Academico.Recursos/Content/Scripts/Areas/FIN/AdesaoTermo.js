(function (smc, $) {

    (function (sga) {

        $("#TermoAceite").on("click", () => {
            var concordoTermo = $("input[name=TermoAceite]").is(":checked");
            if (concordoTermo === true) {
                $('#btnSalvarAdesao').removeAttr('disabled');
            } else {
                $('#btnSalvarAdesao').attr('disabled', 'disabled');
            }
        });

        $("button[data-behavior=smc-behavior-fechar]").on("click", () => {
            var concordoTermo = $("input[name=TermoAceite]").is(":checked");
            if (concordoTermo === true) {
                $('#btnSalvarAdesao').removeAttr('disabled');
            } else {
                $('#btnSalvarAdesao').attr('disabled', 'disabled');
            }
        });

    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);