(function (smc, $) {

    (function (sga) {

        sga.instituicaoTipoEntidade = {

            fieldValue: function (field, success) {

                var obj;
                if (success) {
                    obj = smc.core.uicontrol(field);
                    if (obj.getValue() == "" || obj.getValue() == null)
                        obj.setValue("False");
                }

            }

        };

    })(smc.sga = smc.sga || {});

})(window.smc = window.smc || {}, jQuery);