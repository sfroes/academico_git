(function (smc, $) {

    (function (sga) {

        (function (lancamentoHistoricoEscolar) {
            // Essa implementação da regra RN_APR_007 existe também no Dominio, na classe HistoricoEscolarDomainService, no método CalcularSituacaoFinal

            var escalaApuracao,
                situacaoHistoricoEscolar,
                indicadorPermiteAlunoSemNota,
                indicadorApuracaoNota,
                indicadorApuracaoFrequencia,
				seqEscalaApuracao,
				tipoArredondamento,
                pf, nm, pn, ch;

            $(document).ready(function () {

                var escalaApuracaoValue = new smc.hidden($('#EscalaApuracao')).getValue();
                // Se houver algum valor na escadaApuração, converte para JSON.
                if (escalaApuracaoValue.length > 0) {
                    escalaApuracao = JSON.parse(escalaApuracaoValue);
                }

                situacaoHistoricoEscolar = JSON.parse(new smc.hidden($('#SituacoesFinais')).getValue());

                indicadorPermiteAlunoSemNota = $('[data-name="IndicadorPermiteAlunoSemNota"]:first').val() === 'True';
                indicadorApuracaoNota = $('[data-name="IndicadorApuracaoNota"]:first').val() === 'True';
                indicadorApuracaoFrequencia = $('[data-name="IndicadorApuracaoFrequencia"]:first').val() === 'True';
                seqEscalaApuracao = $('[data-name="SeqEscalaApuracao"]:first').val();                
                pf = $('[data-name="PercentualMinimoFrequencia"]:first').val();
                nm = $('[data-name="NotaMaxima"]:first').val();
                pn = $('[data-name="PercentualMinimoNota"]:first').val();
				ch = $('[data-name="CargaHorariaRealizada"]:first').val() || $('[data-name="CargaHoraria"]:first').val();
				tipoArredondamento = $('[data-name="TipoArredondamento"]:first').val();

                $('[data-control-child="smc-list"]').each(function () {

                    var el = $(this),
                        fieldSemNota = $('[data-name="SemNota"]:visible', el),
                        fieldNota = $('[data-name="Nota"]', el),
                        fieldCargaHorariaRealizada = $('[data-name="CargaHorariaRealizada"]', el),
                        fieldFaltas = $('[data-name="SomaFaltasApuracao"]', el),
                        fieldObservacao = $('[data-name="Observacao"]', el),
                        btnObservacao = $('[name="btnhidePanelObservacao"]', el),

                        fieldApuracao = $('[data-name="DescricaoEscalaApuracaoItem"]', el),
                        fieldSituacao = $('[data-name="DescricaoSituacaoHistoricoEscolar"]', el),
                        fieldEscalaApuracao = $('[data-name="SeqEscalaApuracaoItem"]', el),
                        calcularApuracaoNota = function () {
                            var escala = processaEscala($(this).val());
                            fieldApuracao.val(escala);
                            calcularApuracao();
                        },
                        calcularApuracao = function () {
                            calcularSituacaoFinalAluno();
                        },
                        calcularSituacaoFinalAluno = function () {
                            var nota, faltas, aprovadoNota, aprovadoFrequencia, aprovado = false,
                                aprovadoEscala = $('[data-name="SeqEscalaApuracaoItem"] option:selected', el).data('aprovado') === 'True';

                            if (indicadorPermiteAlunoSemNota && fieldSemNota.prop('checked') && (fieldNota.length === 0 || fieldNota.val().length === 0)) {
                                fieldSituacao.val(situacaoHistoricoEscolar.AlunoSemNota).trigger('change');
                            } else {

                                nota = fieldNota.length > 0 ? fieldNota.val() : 0;
                                faltas = fieldFaltas.val();
                                ch = $('[data-name="CargaHorariaRealizada"]', el).val() || $('[data-name="CargaHoraria"]:first').val();

                                var frequencia = 0;
                                if (ch <= 0) {
                                    // Caso tenha falta, significa que faltou todas as aulas, uma vez que nao tem CH
                                    if (faltas > 0)
                                        frequencia = 0;
                                    // Caso não tenha, participou de todas as aulas
                                    else
                                        frequencia = 100;
                                }
                                else {
                                    if (tipoArredondamento == "ArredondarParaTeto")
                                        frequencia = Math.ceil(100 - faltas / ch * 100);
                                    else
                                        frequencia = 100 - faltas / ch * 100;
                                }

                                aprovadoNota = nota >= nm * pn / 100;
                                aprovadoFrequencia = frequencia >= pf;

                                if (indicadorApuracaoNota && fieldNota.val().length === 0) {
                                    fieldSituacao.val('').trigger('change');
                                    return;
                                }
                                if (indicadorApuracaoNota && indicadorApuracaoFrequencia) {
                                    // RN_APR_007.1
                                    aprovado = aprovadoFrequencia && aprovadoNota;
                                }
                                else if (indicadorApuracaoNota) {
                                    // RN_APR_007.2                                    
                                    aprovado = aprovadoNota;
                                }
                                else if (indicadorApuracaoFrequencia && seqEscalaApuracao.length > 0) {
                                    if (fieldEscalaApuracao.val().length == 0) {
                                        fieldSituacao.val('').trigger('change');
                                        return;
                                    }
                                    // RN_APR_007.3
                                    aprovado = aprovadoFrequencia && aprovadoEscala;
                                }
                                else if (indicadorApuracaoFrequencia) {
                                    // RN_APR_007.4
                                    aprovado = aprovadoFrequencia;
                                }
                                else {
                                    // RN_APR_007.5
                                    aprovado = aprovadoEscala;
                                }

                                fieldSituacao.val(aprovado ? situacaoHistoricoEscolar.Aprovado : situacaoHistoricoEscolar.Reprovado).trigger('change');
                            }
                        },
                        atualizarObservacaoPreenchida = function () {
                            if (fieldObservacao.val().length === 0) {
                                btnObservacao.removeClass('smc-hidepanel-button-filled');
                                btnObservacao.removeClass('smc-hidepanel-button-expanded-filled');
                            }
                            else {
                                if (btnObservacao.hasClass('smc-hidepanel-button-expanded')) {
                                    btnObservacao.addClass('smc-hidepanel-button-expanded-filled');
                                    btnObservacao.removeClass('smc-hidepanel-button-filled');
                                }
                                else {
                                    btnObservacao.addClass('smc-hidepanel-button-filled');
                                    btnObservacao.removeClass('smc-hidepanel-button-expanded-filled');
                                }
                            }
                        };

                    atualizarObservacaoPreenchida();
					fieldNota.on('change', calcularApuracaoNota);
					fieldCargaHorariaRealizada.on('change', calcularApuracao);
                    fieldFaltas.on('change', calcularSituacaoFinalAluno);
                    fieldEscalaApuracao.on('change', calcularSituacaoFinalAluno);
                    fieldSemNota.on('change', function () { fieldApuracao.val(''); fieldNota.val(''); calcularSituacaoFinalAluno(); });
                    fieldObservacao.on('change', atualizarObservacaoPreenchida);
                    btnObservacao.on('click', atualizarObservacaoPreenchida);
                });

            });

            var processaEscala = function (val) {
                if (escalaApuracao === undefined)
                    return;

                val = smc.core.parseFloat(val);
                for (var i = 0; i < escalaApuracao.length; i++) {
                    if (smc.core.parseFloat(escalaApuracao[i].min) <= val && val <= smc.core.parseFloat(escalaApuracao[i].max)) {
                        return escalaApuracao[i].desc;
                    }
                }
                return '';
            }

        })(sga.lancamentoHistoricoEscolar = sga.lancamentoHistoricoEscolar || {});

    })(smc.sga = smc.sga || {});

})(window.smc = window.smc || {}, jQuery);