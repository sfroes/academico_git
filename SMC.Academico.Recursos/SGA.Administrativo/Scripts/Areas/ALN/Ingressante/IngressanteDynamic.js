(function (smc, $) {
    (function (sga) {
        (function (ingressante) {
            var depTermos,
                depOfertas,
                termos,
                ofertas;

            $(document).on('smcload', function () {
                var loadDep = false;
                
                if ($('#OfertasMatrizDependencyTermos').not('[data-custom-dependency]').length > 0) {
                    depTermos = $('#OfertasMatrizDependencyTermos');
                    depTermos.attr('data-custom-dependency', 'true');

                    termos = $('#TermosIntercambio');
                    termos.on('afterdetailadded', reloadDependecies);
                    termos.on('afterdetailremoved', reloadDependecies);

                    loadDep = true;
                }

                if ($('#OfertasMatrizDependencyOfertas').not('[data-custom-dependency]').length > 0) {
                    depOfertas = $('#OfertasMatrizDependencyOfertas');
                    ofertas = $('#Ofertas');
                    ofertas.on('afterdetailadded', function (e, item) {
                        reloadDependecies();
                        $(item).on('change', reloadDependecies);
                    });
                    ofertas.on('afterdetailremoved', reloadDependecies);

                    smc.masterDetailFactory(ofertas).getActiveRows().each(function () {
                        $(this).on('change', reloadDependecies);
                    });

                    loadDep = true;
                }

                if (loadDep === true)
                    reloadDependecies();
            });

            function reloadDependecies() {
                var itensTermos = [],
                    itensOfertas = [];

                if (depTermos && depTermos.length > 0) {
                    $('[name^="TermosIntercambio["][name$="].SeqTermoIntercambio.Seq"]', smc.masterDetailFactory(termos).getActiveRows()).each(function () {
                        var val = $(this).val();
                        if (val.length > 0)
                            itensTermos.push(val);
                    });
                    depTermos.find('input').val(itensTermos.join(', ')).trigger('change');
                }

                if (depOfertas && depOfertas.length > 0) {
                    $('[data-model-property="key"]', smc.masterDetailFactory(ofertas).getActiveRows()).each(function () {
                        var val = $(this).val();
                        if (val.length > 0)
                            itensOfertas.push(val);
                    });
                    depOfertas.find('input').val(itensOfertas.join(', ')).trigger('change');
                }
            }
        })(sga.ingressante = smc.ingressante || {});
    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);