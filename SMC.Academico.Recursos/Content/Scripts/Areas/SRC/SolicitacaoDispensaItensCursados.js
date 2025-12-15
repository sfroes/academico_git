(function (smc, $) {
    (function (sga) {
                
        //#region [ Scripts customizados da tela ]

        $(document).ready(function () {
            var calcular;

            $('#ItensCursadosOutrasInstituicoes').on("change afterdetailremoved afterdetailadded", function () {
                calcular();
            });

            $(document).on('change afterdetailremoved afterdetailadded', '#ItensCursadosNestaInstituicao', function () {
                calcular();
            });

            (calcular = function () {
                var totalHoras = 0;
                var totalHorasAula = 0;
                var totalCreditos = 0;

                var linesComponente = smc.core.uicontrol($('#ItensCursadosOutrasInstituicoes')).getActiveRows();
                $(linesComponente).each(function () {
                    var valorCargaHoraria = $(this).find('[data-name="CargaHoraria"]').val();
                    var valorCredito = $(this).find('[data-name="Credito"]').val();

                    if (valorCargaHoraria != "" && valorCargaHoraria != undefined && valorCargaHoraria != null) {
                        var formatoCargaHoraria = $(this).find('[data-name="FormatoCargaHoraria"]').val();
                        switch (formatoCargaHoraria) {
                            case "HoraAula":
                            case "1":
                                totalHorasAula += parseFloat(valorCargaHoraria);
                                totalHoras += (parseFloat(valorCargaHoraria) * 50) / 60;
                                break;

                            case "Hora":
                            case "2":
                                totalHorasAula += (parseFloat(valorCargaHoraria) * 60) / 50;
                                totalHoras += parseFloat(valorCargaHoraria);
                                break;

                        }

                        //totalHoras += parseFloat(valorCargaHoraria);
                    }

                    if (valorCredito != "" && valorCredito != undefined && valorCredito != null) {
                        totalCreditos += parseFloat(valorCredito);
                    }
                });


                var itensCursadosNestaInstituicaoNaoExcluidos = $('#ItensCursadosNestaInstituicao').find("tbody > tr").not(".smc-linha-removida");
                var totalNestaInstituicao = $(itensCursadosNestaInstituicaoNaoExcluidos).find('[data-name="Seq"]').length;

                if (totalNestaInstituicao > 0) {
                    for (componente = 0; componente < totalNestaInstituicao; componente++) {
                        var elementoCarga = $(itensCursadosNestaInstituicaoNaoExcluidos).find('[data-name="CargaHoraria"]')[componente];
                        var elementoCredito = $(itensCursadosNestaInstituicaoNaoExcluidos).find('[data-name="Credito"]')[componente];

                        var valorCargaHoraria = elementoCarga != null ? elementoCarga.value : "";
                        var valorCredito = elementoCredito != null ? elementoCredito.value : "";

                        if (valorCargaHoraria != "" && valorCargaHoraria != undefined && valorCargaHoraria != null) {
                            var formatoCargaHoraria = $(itensCursadosNestaInstituicaoNaoExcluidos).find('[data-name="FormatoCargaHoraria"]')[componente].value;
                            switch (formatoCargaHoraria) {
                                case "HoraAula":
                                case "1":
                                    totalHorasAula += parseFloat(valorCargaHoraria);
                                    totalHoras += (parseFloat(valorCargaHoraria) * 50) / 60;
                                    break;

                                case "Hora":
                                case "2":
                                    totalHorasAula += (parseFloat(valorCargaHoraria) * 60) / 50;
                                    totalHoras += parseFloat(valorCargaHoraria);
                                    break;
                            }
                            //totalHoras += parseFloat(valorCargaHoraria);
                        }

                        if (valorCredito != "" && valorCredito != undefined && valorCredito != null) {
                            totalCreditos += parseFloat(valorCredito);
                        }
                    }
                }

                var textoHora = (Math.round(parseFloat(totalHoras) * 100) / 100).toString().replace(/[.]/g, ',');
                var textoHoraAula = (Math.round(parseFloat(totalHorasAula) * 100) / 100).toString().replace(/[.]/g, ',');
                var textoCredito = (Math.round(parseFloat(totalCreditos) * 100) / 100).toString().replace(/[.]/g, ',');

                $('#CursadosTotalCargaHorariaHoras').find("p").html(textoHora);
                $('#CursadosTotalCargaHorariaHorasAula').find("p").html(textoHoraAula);
                $('#CursadosTotalCreditos').find("p").html(textoCredito);

            })();
        });

        //#endregion

    })(smc.sga = smc.sga || {});
})(window.smc = window.smc || {}, jQuery);