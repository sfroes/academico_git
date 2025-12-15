(function (smc, $) {
    (function (sga) {
        
        //#region [ Extensões do conditional ]

        sga.solicitacaoDispensaItensDispensados = {
            conditionalTabular: function (field, success) {
                var name = field.attr('data-name'),
                    head = $('[data-id="' + name + '"]');
                if (success) {
                    head.parent().hide();
                }
                else {
                    head.parent().show();
                }
            }
        };

        //#endregion

        //#region [ Scripts customizados da tela ]

        $(document).ready(function () {
            var calcular;

            var cursadoHora = (Math.round(parseFloat($('#CursadosTotalCargaHorariaHoras').find("p").html().replace(/[,]/g, '.')) * 100) / 100).toString().replace(/[.]/g, ',');
            var cursadoHoraAula = (Math.round(parseFloat($('#CursadosTotalCargaHorariaHorasAula').find("p").html().replace(/[,]/g, '.')) * 100) / 100).toString().replace(/[.]/g, ',');
            var cursadoCredito = (Math.round(parseFloat($('#CursadosTotalCreditos').find("p").html().replace(/[,]/g, '.')) * 100) / 100).toString().replace(/[.]/g, ',');

            $('#CursadosTotalCargaHorariaHoras').find("p").html(cursadoHora);
            $('#CursadosTotalCargaHorariaHorasAula').find("p").html(cursadoHoraAula);
            $('#CursadosTotalCreditos').find("p").html(cursadoCredito);

            $('#ComponentesCurriculares').on("afterdetailremoved afterdetailadded", function () {
                calcular();
            });

            $(document).on('change', '[data-name="SeqGrupoCurricularComponente"]', function () {
                calcular();
            });

            $(document).on('change', '[data-name="SeqComponenteCurricular"]', function () {
                calcular();
            });            

            $('#GruposCurriculares').on("afterdetailremoved afterdetailadded", function () {
                calcular();
            });

            $(document).on('change', '[data-name="SeqGrupoCurricular"]', function () {
                calcular();
            });

            $(document).on('change', '[data-name="QuantidadeDispensaGrupo"]', function () {
                calcular();
            });
            (calcular = function () {
                var totalHoras = 0;
                var totalHorasAula = 0;
                var totalCreditos = 0;

                var linesComponente = smc.core.uicontrol($('[data-name="ComponentesCurriculares"][data-control="masterdetail"]')).getActiveRows();
                $(linesComponente).each(function () {
                    for (var item = 0; item < $(this).find('[data-lookup-component="CargaHoraria"]').length; item++) {
                        var valorCargaHoraria = $(this).find('[data-lookup-component="CargaHoraria"]')[item].value;
                        var valorCredito = $(this).find('[data-lookup-component="Credito"]')[item].value;

                        if (valorCargaHoraria != "" && valorCargaHoraria != undefined && valorCargaHoraria != null) {
                            var formatoCargaHoraria = $(this).find('[data-lookup-component="Formato"]')[item].value;
                            switch (formatoCargaHoraria) {
                                case "Hora":
                                case "2":
                                    totalHorasAula += (parseFloat(valorCargaHoraria) * 60) / 50;
                                    totalHoras += parseFloat(valorCargaHoraria);
                                    break;

                                case "HoraAula":
                                case "1":
                                    totalHorasAula += parseFloat(valorCargaHoraria);
                                    totalHoras += (parseFloat(valorCargaHoraria) * 50) / 60;
                                    break;
                            }
                            //totalHoras += parseFloat(valorCargaHoraria);
                        }

                        if (valorCredito != "" && valorCredito != undefined && valorCredito != null) {
                            totalCreditos += parseFloat(valorCredito);
                        }
                    }                      
                });

                var linesGrupo = smc.core.uicontrol($('[data-name="GruposCurriculares"][data-control="masterdetail"]')).getActiveRows();
                $(linesGrupo).each(function () {
                    var formatoConfiguracao = $(this).find('[data-lookup-component="FormatoConfiguracaoGrupo"]').val();
                    var valorCampoQuantidade = parseFloat($(this).find('[data-name="QuantidadeDispensaGrupo"]').val());

                    if (!isNaN(valorCampoQuantidade)) {
                        if (formatoConfiguracao == "Credito") {
                            totalCreditos += valorCampoQuantidade;
                        }
                        else if (formatoConfiguracao == "CargaHoraria") {
                            totalHoras += valorCampoQuantidade;
                            totalHorasAula += (parseFloat(valorCampoQuantidade) * 60) / 50;
                        }
                    }
                });

                var textoHora = (Math.round(parseFloat(totalHoras) * 100) / 100).toString().replace(/[.]/g, ',');
                var textoHoraAula = (Math.round(parseFloat(totalHorasAula) * 100) / 100).toString().replace(/[.]/g, ',');
                var textoCredito = (Math.round(parseFloat(totalCreditos) * 100) / 100).toString().replace(/[.]/g, ',');

                $('#DispensaTotalCargaHorariaHoras').find("p").html(textoHora);
                $('#DispensaTotalCargaHorariaHorasAula').find("p").html(textoHoraAula);
                $('#DispensaTotalCreditos').find("p").html(textoCredito);

            })();
        });

        //#endregion

    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);