using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMC.Academico.Domain.Areas.SRC.Resources;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class GrupoEscalonamentoItemDomainService : AcademicoContextDomain<GrupoEscalonamentoItem>
    {
        #region DomainService

        private EscalonamentoDomainService EscalonamentoDomainService
        {
            get { return this.Create<EscalonamentoDomainService>(); }
        }

        private ProcessoDomainService ProcessoDomainService { get => this.Create<ProcessoDomainService>(); }

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService { get => this.Create<GrupoEscalonamentoDomainService>(); }

        private ServicoMotivoBloqueioParcelaDomainService ServicoMotivoBloqueioParcelaDomainService { get => this.Create<ServicoMotivoBloqueioParcelaDomainService>(); }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => this.Create<ConfiguracaoEventoLetivoDomainService>(); }

        private MotivoBloqueioDomainService MotivoBloqueioDomainService { get => this.Create<MotivoBloqueioDomainService>(); }

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService { get => this.Create<CursoOfertaLocalidadeDomainService>(); }
        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService { get => this.Create<CursoOfertaLocalidadeTurnoDomainService>(); }

        private GrupoEscalonamentoItemParcelaDomainService GrupoEscalonamentoItemParcelaDomainService { get => this.Create<GrupoEscalonamentoItemParcelaDomainService>(); }
        private TipoServicoDomainService TipoServicoDomainService { get => this.Create<TipoServicoDomainService>(); }
        private ServicoDomainService ServicoDomainService { get => this.Create<ServicoDomainService>(); }

        #endregion DomainService

        public long SalvarGrupoEscalonamentoItem(GrupoEscalonamentoItemVO modelo)
        {
            ValidarModeloConfigurarParcelas(modelo);

            var grupoEscalonamento = modelo.Transform<GrupoEscalonamentoItem>();

            this.SaveEntity(grupoEscalonamento);

            if (modelo.HouveAlteracaoParcela)
            {
                var specGrupoEscalonamento = new GrupoEscalonamentoFilterSpecification { Seq = grupoEscalonamento.SeqGrupoEscalonamento };
                var grupoEscalonamentoBanco = GrupoEscalonamentoDomainService.SearchByKey(specGrupoEscalonamento);
                if (grupoEscalonamentoBanco != null)
                    grupoEscalonamentoBanco.Ativo = false;

                GrupoEscalonamentoDomainService.SaveEntity(grupoEscalonamentoBanco);
            }

            return grupoEscalonamento.Seq;
        }

        public void ValidarModeloConfigurarParcelas(GrupoEscalonamentoItemVO modelo)
        {
            // Recupera o escalonamento em questão
            var escalonamento = this.EscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.SeqEscalonamento));

            if (modelo.Parcelas.Count == 0)
            {
                var parcelasGrupoEscalonamento = GrupoEscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(modelo.SeqGrupoEscalonamento), IncludesGrupoEscalonamento.Itens
                                                                                                                         | IncludesGrupoEscalonamento.Itens_Escalonamento
                                                                                                                         | IncludesGrupoEscalonamento.Itens_Parcelas);


                var servicoProcesso = ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(modelo.SeqServico));
                var possuiIntegracaoFinanceira = servicoProcesso.IntegracaoFinanceira == IntegracaoFinanceira.NaoSeAplica ? false : true;

                if (possuiIntegracaoFinanceira && parcelasGrupoEscalonamento.Itens.Where(c => c.Seq != modelo.Seq).SelectMany(c => c.Parcelas).Count() == 0)
                {
                    throw new GrupoEscalonamentoEtapasSemParcelasException();
                }
            }

            var dadosProcesso = this.GrupoEscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(modelo.SeqGrupoEscalonamento), p => new
            {
                p.SeqProcesso,
                p.Processo.Servico.Token,
                p.Processo.DataFim,
                p.Processo.SeqCicloLetivo,
                p.Processo.Servico.ObrigatorioIdentificarParcela,
                p.Processo.UnidadesResponsaveis.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).SeqEntidadeResponsavel
            });

            /*1. Esta regra não deve ser considerada para processos do serviço que possuem um dos tokens: 
            SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU, SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA 
            e MATRICULA_REABERTURA.*/
            if (dadosProcesso.Token != TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU &&
                dadosProcesso.Token != TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA &&
                dadosProcesso.Token != TOKEN_SERVICO.MATRICULA_REABERTURA)
            {
                /*Para os demais processos:
                A "Data de Vencimento" de todas as parcelas deverá ser MAIOR ou IGUAL do que a "Data/Hora Fim"
                do escalonamento em questão e MENOR do que a "Data/Hora Fim" do processo em questão. Caso NÃO ocorra, 
                abortar a operação e exibir mensagem de impeditiva:
                "Configuração não permitida. A data de vencimento de todas as parcelas deverá ser maior ou igual do que a 
                data/hora fim do escalonamento em questão e menor do que a data/hora fim do processo em questão".*/
                foreach (var parcela in modelo.Parcelas)
                {
                    if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
                        parcela.DataVencimentoParcela.Date >= dadosProcesso.DataFim.GetValueOrDefault().Date)
                    {
                        throw new GrupoEscalonamentoItemParcelaDataVencimentoMenorEscalonamentoException();
                    }
                }

                /*Para cada parcela cadastrada, verificar se a "Data de Vencimento" da parcela é MENOR que a "Data de Vencimento" 
                da parcela seguinte, levando em consideração o número da parcela. Caso NÃO ocorra, abortar a operação e exibir 
                a seguinte mensagem de impeditiva:
                "Configuração não permitida. A data de vencimento das parcelas deverá levar em consideração o número da parcela, 
                isto é, parcela com o número menor do que a outra parcela deverá conter a data de vencimento menor também."*/
                if (modelo.Parcelas.Count > 1)
                {
                    //Ordena as parcelas pelo numero da parcela
                    modelo.Parcelas = modelo.Parcelas.OrderBy(p => p.NumeroParcela).ToList();

                    for (int i = 0; i < modelo.Parcelas.Count - 1; i++)
                    {
                        if (modelo.Parcelas[i].DataVencimentoParcela >= modelo.Parcelas[i + 1].DataVencimentoParcela)
                        {
                            throw new GrupoEscalonamentoItemParcelaDataVencimentoMaiorProximaParcelaException();
                        }
                    }
                }
            }

            /*1.1 Para os processos do serviço que possui o token SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA, 
            considerar a seguinte regra:"*/
            //Comentado por conta da tarefa - 58834 
            //if (dadosProcesso.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
            //{
            //    var cursoOfertaLocalidades = CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(dadosProcesso.SeqEntidadeResponsavel);

            //    if (cursoOfertaLocalidades.Any() && dadosProcesso.SeqCicloLetivo.HasValue)
            //    {
            //        var cursoOfertaLocalidade = cursoOfertaLocalidades.First();
            //        var cursoOfertaLocalidadeTurnos = CursoOfertaLocalidadeTurnoDomainService.BuscarTurnosPorLocalidadeCusroOfertaSelect(cursoOfertaLocalidade.RecuperarSeqLocalidade(), cursoOfertaLocalidade.SeqCursoOferta);

            //        if (cursoOfertaLocalidadeTurnos.Any())
            //        {
            //            var seqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnos.First().Seq;

            //            var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosProcesso.SeqCicloLetivo.Value, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            //            #region Recuperando os meses do ciclo letivo

            //            //Lista que terá os meses do ciclo letivo
            //            List<int> mesesCicloLetivo = new List<int>() { datasEventoLetivo.DataInicio.Month };

            //            //Variáveis para auxiliar a verificar qual o último mês do ciclo
            //            var achouMesFimCiclo = false;
            //            var contadorMeses = 1;

            //            //Recupera os meses do ciclo, se a data for menor ou igual que a data fim do ciclo, ela será validada
            //            while (!achouMesFimCiclo)
            //            {
            //                var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMeses);

            //                /*Validação para adicionar na lista somente as datas que são menores que a data fim
            //                Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
            //                último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
            //                if (dataVerificar <= datasEventoLetivo.DataFim)
            //                    mesesCicloLetivo.Add(dataVerificar.Month);
            //                else
            //                    achouMesFimCiclo = true;

            //                contadorMeses++;
            //            }

            //            var ultimoMesCiclo = mesesCicloLetivo.Last();

            //            #endregion

            //            var numeroDivisaoParcelas = this.GrupoEscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(modelo.SeqGrupoEscalonamento), x => x.NumeroDivisaoParcelas);

            //            #region Validação do fator de divisão temporiariamente comentada conforme Task 40404

            //            //if (numeroDivisaoParcelas.HasValue)
            //            //{
            //            //    foreach (var parcela in modelo.Parcelas)
            //            //    {
            //            //        switch (numeroDivisaoParcelas.Value)
            //            //        {
            //            //            case 4:

            //            //                //Fator de divisão = 4: a data de vencimento deve estar dentro do primeiro mês do ciclo letivo
            //            //                if (parcela.DataVencimentoParcela.Month != datasEventoLetivo.DataInicio.Month)
            //            //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

            //            //                break;

            //            //            case 3:

            //            //                //Fator de divisão = 3: a data de vencimento deve estar dentro do segundo mês do ciclo letivo
            //            //                var segundoMesCiclo = datasEventoLetivo.DataInicio.AddMonths(1);

            //            //                if (parcela.DataVencimentoParcela.Month != segundoMesCiclo.Month)
            //            //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

            //            //                break;

            //            //            case 2:

            //            //                //Fator de divisão = 2: a data de vencimento deve estar dentro do terceiro mês do ciclo letivo
            //            //                var terceiroMesCiclo = datasEventoLetivo.DataInicio.AddMonths(2);

            //            //                if (parcela.DataVencimentoParcela.Month != terceiroMesCiclo.Month)
            //            //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

            //            //                break;

            //            //            case 1:

            //            //                //Fator de divisão = 1: a data de vencimento deve estar dentro do quarto mês do ciclo letivo ou demais meses
            //            //                var quartoMesCiclo = datasEventoLetivo.DataInicio.AddMonths(3);

            //            //                //Lista que terá os meses a serem comparados com a data de vencimento
            //            //                List<int> mesesCicloLetivoAPartirQuartoMes = new List<int>() { quartoMesCiclo.Month };

            //            //                //Variáveis para auxiliar a verificar qual o último mês do ciclo, considerando que pegará do quarto mês em diante
            //            //                var achouMesFimCicloDepoisQuartoMes = false;
            //            //                var contadorMesesDepoisQuartoMes = 4;

            //            //                //Recupera os meses do ciclo do quarto mês em diante, se a data for menor ou igual que a data fim do ciclo, ela será validada
            //            //                while (!achouMesFimCicloDepoisQuartoMes)
            //            //                {
            //            //                    var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMesesDepoisQuartoMes);

            //            //                    /*Validação para adicionar na lista somente as datas que são menores que a data fim
            //            //                    Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
            //            //                    último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
            //            //                    if (dataVerificar <= datasEventoLetivo.DataFim)
            //            //                        mesesCicloLetivoAPartirQuartoMes.Add(dataVerificar.Month);
            //            //                    else
            //            //                        achouMesFimCicloDepoisQuartoMes = true;

            //            //                    contadorMesesDepoisQuartoMes++;
            //            //                }

            //            //                //Verifica se a data de vencimento da parcela não está contida do quarto mês em diante da data de início do ciclo letivo
            //            //                if (!mesesCicloLetivoAPartirQuartoMes.Contains(parcela.DataVencimentoParcela.Month) && parcela.DataVencimentoParcela <= datasEventoLetivo.DataFim)
            //            //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

            //            //                break;
            //            //        }
            //            //    }
            //            //}

            //            #endregion

            //            ///Se a data fim do escalonamento da etapa for menor que a data início do [ciclo letivo]* do processo, a 
            //            ///data de vencimento da parcela deverá ser maior ou igual a data fim do escalonamento e menor ou igual a 
            //            ///data início do ciclo letivo. Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem de 
            //            ///erro: "Configuração não permitida. A data de vencimento da parcela deverá ser maior ou igual à 
            //            ///data/hora fim do escalonamento e menor ou igual a data início do ciclo letivo do processo."
            //            if (escalonamento.DataFim.Date < datasEventoLetivo.DataInicio.Date)
            //            {
            //                foreach (var parcela in modelo.Parcelas)
            //                {
            //                    if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
            //                        parcela.DataVencimentoParcela.Date > datasEventoLetivo.DataInicio.Date)
            //                        throw new GrupoEscalonamentoItemDataVencimentoMenorDataFimEscalonamentoException();
            //                }
            //            }
            //            ///Se a data fim do escalonamento estiver no último mês do [ciclo letivo]* do processo, a data de 
            //            ///vencimento da parcela deverá ser maior ou igual a data fim do escalonamento e menor que a data fim do 
            //            ///ciclo letivo. Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem de erro:
            //            ///"Configuração não permitida. A data de vencimento da parcela deverá ser maior ou igual à data/hora fim 
            //            ///do escalonamento e menor que a data fim do ciclo letivo do processo."
            //            else if (escalonamento.DataFim.Month == ultimoMesCiclo)
            //            {
            //                foreach (var parcela in modelo.Parcelas)
            //                {
            //                    if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
            //                        parcela.DataVencimentoParcela.Date >= datasEventoLetivo.DataFim.Date)
            //                        throw new GrupoEscalonamentoItemVencimentoDeveSerMaiorDataFimEscalonamentoException();
            //                }
            //            }
            //            ///Se a data fim do escalonamento não estiver em nenhum destes casos, a data de vencimento da parcela 
            //            ///deverá ser maior ou igual a data fim do escalonamento e menor ou igual ao dia primeiro do mês 
            //            ///subsequente à data fim do escalonamento. Caso isto não ocorra, abortar a operação e exibir a seguinte
            //            ///mensagem de erro: "Configuração não permitida. A data de vencimento da parcela deverá ser maior ou 
            //            ///igual à data/hora fim do escalonamento e menor ou igual ao dia primeiro do mês subsequente à data 
            //            ///fim do escalonamento."
            //            else
            //            {
            //                var dataMesSubsequenteDataFimEscalonamento = escalonamento.DataFim.AddMonths(1);
            //                var dataPrimeiroDiaMesSubsequenteDataFimEscalonamento = new DateTime(dataMesSubsequenteDataFimEscalonamento.Year, dataMesSubsequenteDataFimEscalonamento.Month, 1);

            //                foreach (var parcela in modelo.Parcelas)
            //                {
            //                    if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
            //                        parcela.DataVencimentoParcela.Date > dataPrimeiroDiaMesSubsequenteDataFimEscalonamento.Date)
            //                        throw new GrupoEscalonamentoItemDatavencimentoMenorIgualPrimeiroDiaException();
            //                }
            //            }
            //        }
            //    }
            //}

            #region Antiga validação valor percentual

            //Verificar se a soma do "Valor Percentual" de TODAS as parcelas é exatamente 100. Caso NÃO
            //ocorra, abortar operação e exibir de erro

            //if (modelo.Parcelas.Sum(p => p.ValorPercentualParcela) != 100)
            //    throw new GrupoEscalonamentoItemSomatorioPercentualInvalidoException();

            #endregion

            /*2. A validação do somatório do Valor Percentual será de acordo com o campo Serviço Adicional, sendo que:*/
            var parcelasServicoAgrupadas = modelo.Parcelas.GroupBy(g => g.ServicoAdicional);

            foreach (var item in parcelasServicoAgrupadas)
            {
                /*Se Serviço Adicional igual a Sim, o somatório dos percentuais deverá ser menor ou igual a 100%. Em caso negativo, exibir a seguinte mensagem impeditiva:
                “Configuração não permitida. O somatório do percentual das parcelas que são de serviço adicional deve ser menor ou igual a 100%.”*/
                if (item.Key == true)
                {
                    if (item.Sum(s => s.ValorPercentualParcela) > 100)
                    {
                        throw new GrupoEscalonamentoItemSomatorioPercentualMaiorPermitidoException();
                    }
                }
                else
                {
                    /*Senão, se Serviço Adicional igual a Não, o somatório dos percentuais deverá ser igual a 100% .Em caso negativo, exibir a seguinte mensagem impeditiva:
                    “Configuração não permitida.O somatório do percentual das parcelas que não é de serviço adicional deve ser igual a 100 %.”.*/
                    if (item.Sum(p => p.ValorPercentualParcela) != 100)
                    {
                        throw new GrupoEscalonamentoItemSomatorioPercentualInvalidoException();
                    }
                }
            }

            /*3. Verificar se todos os motivos de bloqueio que foram parametrizados como obrigatórios para o serviço em questão estão associados à pelo menos uma parcela. 
            Caso não esteja, abortar a operação e exibir a seguinte mensagem de impeditiva: "Não é possível prosseguir. É obrigatória a seleção dos motivo(s) bloqueio:
            - <Descrição do motivo bloqueio obrigatório 1>
            - <Descrição do motivo bloqueio obrigatório 2>"*/
            var spec = new ServicoMotivoBloqueioParcelaFilterSpecification() { SeqServico = modelo.SeqServico, Obrigatorio = true };

            var motivosBloqueioObrigatoriosServico = this.ServicoMotivoBloqueioParcelaDomainService.SearchProjectionBySpecification(spec, x => x.MotivoBloqueio).ToList();
            StringBuilder motivosBloqueiosObrigatoriosNaoSelecionados = new StringBuilder();

            if (modelo.Parcelas.Any())
            {
                foreach (var motivoBloqueio in motivosBloqueioObrigatoriosServico)
                {
                    if (!modelo.Parcelas.Any(a => a.SeqMotivoBloqueio == motivoBloqueio.Seq))
                        motivosBloqueiosObrigatoriosNaoSelecionados.Append($"<br /> - {motivoBloqueio.Descricao}");
                }
            }

            if (!string.IsNullOrEmpty(motivosBloqueiosObrigatoriosNaoSelecionados.ToString()))
                throw new GrupoEscalonamentoItemParcelaMotivoBloqueioObrigatorioException(motivosBloqueiosObrigatoriosNaoSelecionados.ToString());

            /*4. Caso o serviço possua o token SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ou MATRICULA_REABERTURA, verificar se a
            data de vencimento das parcelas é a data início do ciclo letivo do processo, de acordo com a regra RN_CAM_030 -Retorna período do evento letivo, passando como parâmetro:
            -Ciclo letivo: ciclo letivo do processo
            - Tipo de evento: Período do ciclo letivo
            - Curso oferta localidade turno: curso oferta localidade turno da entidade responsável pelo processo
            Caso não seja, abortar a operação e exibir a seguinte mensagem de impeditiva: "Não é possível prosseguir. Este serviço exige que a data de vencimento das parcelas seja o primeiro dia do ciclo letivo."*/
            if (dadosProcesso.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ||
                dadosProcesso.Token == TOKEN_SERVICO.MATRICULA_REABERTURA)
            {
                var cursoOfertaLocalidadeTurnos = CursoOfertaLocalidadeTurnoDomainService.BuscarCursoOfertasLocalidadeTurnoAtivoPorEntidadeResponsavel(dadosProcesso.SeqEntidadeResponsavel);

                if (cursoOfertaLocalidadeTurnos.Any() && dadosProcesso.SeqCicloLetivo.HasValue)
                {
                    if (cursoOfertaLocalidadeTurnos.Any())
                    {
                        var seqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnos.First().Seq;

                        var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosProcesso.SeqCicloLetivo.Value, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                        if (modelo.Parcelas.Any(a => a.DataVencimentoParcela.Date != datasEventoLetivo.DataInicio.Date))
                            throw new GrupoEscalonamentoItemParcelaDataVencimentoCicloLetivoException();
                    }
                }
            }

            /*5. Se o tipo de serviço do processo, obriga a identificação da parcela:*/
            if (dadosProcesso.ObrigatorioIdentificarParcela && modelo.Parcelas.Any())
            {
                if (modelo.Parcelas.Count() > 1)
                {
                    /*Quando existir mais de uma parcela configurada, verificar se a menor data de vencimento de parcela está 
                    associado à parcela de número 1 e ao motivo de bloqueio que possui o token PARCELA_PRE_MATRICULA_PENDENTE. Caso não estiver, abortar a operação e exibir a seguinte mensagem impeditiva: "Não é possível prosseguir. A parcela 1 deve possuir a menor data de vencimento e estar associada ao motivo bloqueio "Parcela de pré-matrícula pendente".*/
                    var parcelaNumero1ComMenorDataVencimento = modelo.Parcelas.Where(a => a.DataVencimentoParcela == modelo.Parcelas.Min(m => m.DataVencimentoParcela)).FirstOrDefault(a => a.NumeroParcela == 1);

                    if (parcelaNumero1ComMenorDataVencimento != null)
                    {
                        var motivoBloqueioParcela = this.MotivoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<MotivoBloqueio>(parcelaNumero1ComMenorDataVencimento.SeqMotivoBloqueio));

                        if (motivoBloqueioParcela.Token != TOKEN_MOTIVO_BLOQUEIO.PARCELA_PRE_MATRICULA_PENDENTE)
                            throw new GrupoEscalonamentoItemParcelaMenorVencimentoSemMotivoBloqueioException();
                    }
                    else
                        throw new GrupoEscalonamentoItemParcelaMenorVencimentoSemMotivoBloqueioException();
                }

                if (modelo.Parcelas.Count() == 1)
                {
                    /*Quando existir apenas uma parcela, verificar se ela está associada ao bloqueio que possui o token 
                    PARCELA_MATRICULA_PENDENTE e o número da parcela é igual a 1.
                    Caso não estiver, abortar a operação e exibir a seguinte mensagem impeditiva: 
                    "Não é possível prosseguir. A parcela única deve ser de número 1 e estar associada ao motivo bloqueio "Parcela de matrícula pendente."
                    */
                    var motivoBloqueioParcela = this.MotivoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<MotivoBloqueio>(modelo.Parcelas.First().SeqMotivoBloqueio));
                    
                    var parcelaNumero = modelo.Parcelas.FirstOrDefault().NumeroParcela;
                            
                    if (motivoBloqueioParcela.Token != TOKEN_MOTIVO_BLOQUEIO.PARCELA_MATRICULA_PENDENTE || parcelaNumero != 1)
                        throw new GrupoEscalonamentoItemParcelaUnicaSemMotivoBloqueioException();
                }
            }

            modelo.HouveAlteracaoParcela = ValidaAssertSalvar(modelo);

        }


        /// <summary>
        /// Buscar grupo escalonamento item
        /// </summary>
        /// <param name="seq">Sequencial do grupo escalonamento item</param>
        /// <returns>Retorna o grupo de escalonamento item</returns>
        public GrupoEscalonamentoItemVO BuscarGrupoEscalonamentoItem(long seq)
        {
            var includes = IncludesGrupoEscalonamentoItem.Escalonamento | IncludesGrupoEscalonamentoItem.GrupoEscalonamento | IncludesGrupoEscalonamentoItem.Parcelas;
            var retorno = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamentoItem>(seq), includes).Transform<GrupoEscalonamentoItemVO>();

            ///Ordem crescente pelo Nº Parcela e, em seguida pela Data de Vencimento.
            retorno.Parcelas = retorno.Parcelas.OrderBy(o => o.NumeroParcela).ThenBy(t => t.DataVencimentoParcela).ToList();

            var dadosGrupoEscalonamentoProcesso = this.GrupoEscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(retorno.SeqGrupoEscalonamento), p =>
            new
            {
                p.SeqProcesso,
                ProcessoEncerrado = p.Processo.DataEncerramento.HasValue && p.Processo.DataEncerramento.Value < DateTime.Now,
                ProcessoExpirado = (p.Processo.DataFim.HasValue && DateTime.Now > p.Processo.DataFim.Value),
                p.Ativo
            });

            var servico = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(dadosGrupoEscalonamentoProcesso.SeqProcesso), p => p.Servico);
            var processo = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(dadosGrupoEscalonamentoProcesso.SeqProcesso), p => p);

            retorno.ObrigatorioIdentificarParcela = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(dadosGrupoEscalonamentoProcesso.SeqProcesso), p => p.Servico.ObrigatorioIdentificarParcela);
            retorno.SeqServico = servico.Seq;
            retorno.TokenServico = servico.Token;
            retorno.SeqProcesso = dadosGrupoEscalonamentoProcesso.SeqProcesso;
            retorno.ValorPercentualBanco = processo.ValorPercentualServicoAdicional;

            var dadosEscalonamento = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(retorno.SeqEscalonamento), p =>
            new
            {
                EscalonamentoEncerrado = p.DataEncerramento.HasValue,
                EscalonamentoVigente = DateTime.Now >= p.DataInicio && (p.DataFim == null || DateTime.Now <= p.DataFim),
                p.ProcessoEtapa.SituacaoEtapa
            });

            var listaQuantidadeSolicitacoes = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = dadosGrupoEscalonamentoProcesso.SeqProcesso }).Select(s =>
            new
            {
                s.SeqGrupoEscalonamento,
                s.QuantidadeSolicitacoes

            }).ToList();

            int quantidadeSolicitacoes = 0;

            if (listaQuantidadeSolicitacoes.Any(a => a.SeqGrupoEscalonamento == retorno.SeqGrupoEscalonamento))
            {
                quantidadeSolicitacoes = listaQuantidadeSolicitacoes.FirstOrDefault(f => f.SeqGrupoEscalonamento == retorno.SeqGrupoEscalonamento).QuantidadeSolicitacoes;
            }

            retorno.CamposReadOnly = false;
            retorno.MensagemInformativa = string.Empty;

            if (dadosGrupoEscalonamentoProcesso.ProcessoEncerrado)
            {
                retorno.CamposReadOnly = true;
                retorno.MensagemInformativa = MessagesResource.MSG_CamposConfigurarParcela_ProcessoEncerrado;
            }
            else if (dadosEscalonamento.EscalonamentoEncerrado)
            {
                retorno.CamposReadOnly = true;
                retorno.MensagemInformativa = MessagesResource.MSG_CamposConfigurarParcela_EscalonamentoEncerrado;
            }
            else if (dadosEscalonamento.EscalonamentoVigente && quantidadeSolicitacoes > 0 && dadosEscalonamento.SituacaoEtapa == SituacaoEtapa.Liberada)
            {
                retorno.CamposReadOnly = true;
                retorno.MensagemInformativa = MessagesResource.MSG_CamposConfigurarParcela_SolicitacaoAssociada;
            }

            foreach (var parcela in retorno.Parcelas)
            {
                var motivoBloqueioParcela = this.MotivoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<MotivoBloqueio>(parcela.SeqMotivoBloqueio));

                /// Se o token do motivo do bloqueio selecionado for igual à PARCELA_SERVICO_ADICIONAL_PENDENTE E exista o valor percentual do serviço adicional 
                /// paramerizado no processo, este campo deve ser preenchido automaticamente com o valor informado no processo e o campo deverá ficar desabilitado.                
                if (motivoBloqueioParcela.Token == TOKEN_MOTIVO_BLOQUEIO.PARCELA_SERVICO_ADICIONAL_PENDENTE && (retorno.ValorPercentualBanco.HasValue && retorno.ValorPercentualBanco.Value > 0))
                {
                    parcela.DesativarPercentualParcela = true;
                    parcela.ValorPercentualParcela = retorno.ValorPercentualBanco.Value;
                }
            }

            return retorno;
        }

        /// <summary>
        ///  Valida se o escalonmento esta vigente baseado no grupo de escalomento e na esta do SGF
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <param name="seqEtapaSGF">Sequenial da etapa no SGF</param>
        /// <param name="somenteDataFinmEtapa">Leva em consideração a data fim da etapa</param>
        /// <returns>Se o escalonamento esta vigente ou não</returns>
        public bool ValidarEscalonmentoDaEtapaVigentePorGrupoEscalonamento(long seqGrupoEscalonamento, long seqEtapaSGF, bool somenteDataFinmEtapa = false)
        {
            bool retorno = true;

            var spec = new GrupoEscalonamentoItemFilterSpecification() { SeqGrupoEscalonamento = seqGrupoEscalonamento };

            var result = SearchBySpecification(spec, i => i.GrupoEscalonamento, i => i.Escalonamento, i => i.Escalonamento.ProcessoEtapa).ToList();

            if (somenteDataFinmEtapa)
            {
                retorno = result.Any(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == seqEtapaSGF && (DateTime.Now <= i.Escalonamento.DataFim));
            }
            else
            {
                retorno = result.Any(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == seqEtapaSGF && (DateTime.Now >= i.Escalonamento.DataInicio && DateTime.Now <= i.Escalonamento.DataFim));
            }

            return retorno;
        }

        public bool ValidaAssertSalvar(GrupoEscalonamentoItemVO model)
        {
            if (model.Parcelas.Any())
            {
                foreach (var parcela in model.Parcelas)
                {
                    var specParcela = new GrupoEscalonamentoItemParcelaFilterSpecification { Seq = parcela.Seq };
                    var parcelaBanco = GrupoEscalonamentoItemParcelaDomainService.SearchByKey(specParcela);

                    if(parcelaBanco != null)
                    {
                        if (parcela.NumeroParcela != parcelaBanco.NumeroParcela)
                            model.HouveAlteracaoParcela = true;

                        if (!parcela.Descricao.Equals(parcelaBanco.Descricao))
                            model.HouveAlteracaoParcela = true;

                        if (parcela.DataVencimentoParcela != parcelaBanco.DataVencimentoParcela)
                            model.HouveAlteracaoParcela = true;

                        if (parcela.ValorPercentualParcela != parcelaBanco.ValorPercentualParcela)
                            model.HouveAlteracaoParcela = true;

                        if (parcela.SeqMotivoBloqueio != parcelaBanco.SeqMotivoBloqueio)
                            model.HouveAlteracaoParcela = true;

                        if (parcela.ServicoAdicional != parcelaBanco.ServicoAdicional)
                            model.HouveAlteracaoParcela = true;
                    }
                }
            }

            return model.HouveAlteracaoParcela;
        }

        public bool ConsistenciasValidadasSalvarGrupoEscalonamentoParcela(GrupoEscalonamentoItemVO modelo)
        {
            ValidarModeloConfigurarParcelas(modelo);

            return modelo.HouveAlteracaoParcela;
        }

      
    }
}