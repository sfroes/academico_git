(function (smc, $) {

    (function (sga) {        
        $(document).on('smcload', function () {
            $('[data-type="smc-modal"]').off('modalclosed.custom').on('modalclosed.custom',
                function () {
                    var elemento = $(this).find('[data-type="smc-modal-footer"]').find('button[type="submit"]');
                    if ($(elemento).is("[data-conditional]"))
                        smc.conditionalBehavior.destroy($(elemento));
                });
        });

        $(document).on("detailadded", function (evento, template) {
            var elementoPai = $(template).closest('[data-control="masterdetail"]');
            var nomeElementoPai = elementoPai.attr("data-name");

            // MD de divisões de componente
            if (nomeElementoPai.endsWith("DivisoesComponentes")) {
                // Copia o número da divisão e número da turma
                var numeroDivisao = smc.core.uicontrol(smc.core.findInputs("DivisaoDescricao", elementoPai).first()).getValue();
                var codigoTurma = smc.core.uicontrol(smc.core.findInputs("Turma", elementoPai).first()).getValue();

                var inputNumeroDivisao = smc.core.findInputs("DivisaoDescricao", template);
                smc.core.uicontrol(inputNumeroDivisao).setValue(numeroDivisao);

                var inputCodigoTurma = smc.core.findInputs("Turma", template);
                smc.core.uicontrol(inputCodigoTurma).setValue(codigoTurma);
            }
        });
    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);