(function (smc, $) {

    (function (sga) {

        //#region [ Extensões do conditional ]

        sga.instituicaoNivelTipoVinculoAluno = {

            fieldValue: function (field, success) {

                var obj = smc.core.uicontrol(field);
                if (success) {
                    obj.setValue("False");
                }
                else {
                    obj.setValue("True");
                }
            }

        };

        //#endregion [ Extensões do conditional ]

    })(smc.sga = smc.sga || {});

})(window.smc = window.smc || {}, jQuery);