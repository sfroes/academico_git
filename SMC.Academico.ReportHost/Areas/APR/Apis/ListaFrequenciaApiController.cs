using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.ReportHost.Areas.TUR.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.APR.Apis
{
    public class ListaFrequenciaApiController : SMCApiControllerBase
    {
        #region [ Services ]

        internal ITurmaService TurmaService => Create<ITurmaService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        internal IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();

        internal IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        //[SMCAuthorize(UC_APR_006_03_02.EXIBIR_LISTA_FREQUENCIA)]
        public byte[] GerarRelatorio(ListaFrequenciaFiltroVO filtro)
        {
            //Validando se deve emitir o relatório
            ValidarRelatorioListaFrequencia(filtro);

            ValidarDivisaoTurma(filtro.SeqDivisaoTurma);

            var parameters = MontarParametros(filtro);

            var reportAction = MontarReportAction(filtro.Colaboradores, filtro.DiaEmissao, filtro.SeqDivisaoTurma.Value);

            var dadosTurma = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(filtro.SeqDivisaoTurma.Value).FirstOrDefault(d => d.Seq == filtro.SeqDivisaoTurma);
            var listaAlunos = TurmaService.BuscarDiarioTurmaAluno(dadosTurma.SeqTurma, filtro.SeqDivisaoTurma, null, filtro.Colaboradores).Select(x => new DivisaoTurmaRelatorioAlunoData
            {
                SeqCursoOfertaLocalidadeTurno = x.SeqCursoOfertaLocalidadeTurno,
                DescricaoCursoOfertaLocalidadeTurno = x.DescricaoCursoOfertaLocalidadeTurno,
                NomeAluno = x.NomeAluno,
                NumeroRegistroAcademico = x.NumeroRegistroAcademico,
                SeqDivisaoTurma = filtro.SeqDivisaoTurma.Value,
                AlunoDI = x.AlunoDI,
                AlunoFormado = x.AlunoFormado
            }).ToList();

            return SMCGenerateReport("Areas/APR/Reports/ListaFrequencia/ListaFrequenciaRelatorio.rdlc", listaAlunos, "DSListaFrequencia", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);
        }

        private void ValidarRelatorioListaFrequencia(ListaFrequenciaFiltroVO filtro)
        {
            if (filtro.DiaEmissao.HasValue)
            {
                ValidarDivisaoTurma(filtro.SeqDivisaoTurma);

                //RN_APR_029 - Consistência duração aula
                ValidarConsistenciaDuracaoAula(filtro);

                ValidarDataIntervalo(filtro.DiaEmissao, filtro.SeqDivisaoTurma.Value);
            }
        }

        private void ValidarDivisaoTurma(long? seqDivisaoTurma)
        {
            if (seqDivisaoTurma.GetValueOrDefault() <= 0) { throw new SMCApplicationException("Divisão de turma inválida!"); }
        }

        private void ValidarDataIntervalo(DateTime? dataEmissao, long seqDivisaoTurma)
        {
            // Faz a comparação desconsiderando a hora da aula, pois o período letivo da turma não tem hora informada
            (DateTime dataInicio, DateTime dataFim) divisaoTurma = DivisaoTurmaService.BuscarDatasEventoLetivoTurma(seqDivisaoTurma);
            if (dataEmissao < divisaoTurma.dataInicio || dataEmissao > divisaoTurma.dataFim)
            {
                throw new SMCApplicationException("A data informada está fora do intervalo permitido.");
            }
        }

        /// <summary>
        /// RN_APR_029 - Consistência duração aula
        /// Antes de emitir a lista de frequência, consistir:
        /// A duração da aula(Hora início menos Hora fim) deverá ser maior ou igual a Duração Mínima Aula e
        /// menor ou igual a Duração Máxima Aula.Duração Mínima Aula e Duração Máxima Aula são valores parametrizados por instituição e nível de
        /// ensino e salvos no banco em minutos.Portanto, é necessário converter os valores de Hora início e Hora
        /// fim para minutos.Em caso de violação, abortar a operação e emitir a mensagem de erro:
        /// "A duração da aula deverá ser maior ou igual a {0} [Duração Mínima Aula] e menor ou igual a {1} [Duração Máxima Aula]."*/
        /// </summary>
        /// <param name="seqNivelEnsino"></param>
        private void ValidarConsistenciaDuracaoAula(ListaFrequenciaFiltroVO filtro)
        {
            if (filtro.SeqNivelEnsino.HasValue && filtro.SeqNivelEnsino > 0)
            {
                var instituicaoNivel = InstituicaoNivelService.BuscarInstituicaoNivelPorNivelEnsino(filtro.SeqNivelEnsino.GetValueOrDefault());

                if (instituicaoNivel == null) { return; }

                /// Hora início e hora fim, não são obrigatórios, portanto não serão validados se virem vazios.
                if (!filtro.HoraInicio.HasValue || !filtro.HoraFim.HasValue) { return; }

                /// faço a converção dos horários inseridos pelo usuário para minutos
                var totalMinutosHoraInicioInseridos = ConverterHoraParaMinutos(filtro.HoraInicio.Value);
                var totalMinutosHoraFimInseridos = ConverterHoraParaMinutos(filtro.HoraFim.Value);

                if (totalMinutosHoraInicioInseridos >= totalMinutosHoraFimInseridos)
                {
                    throw new Exception("A Hora Final deve ser maior que a Hora Inicial!");
                }

                /// A duração da aula(Hora início menos Hora fim)
                var duracaoAula = totalMinutosHoraFimInseridos - totalMinutosHoraInicioInseridos;

                /// Faço a comparação com a regra
                /// Deverá ser maior ou igual a Duração Mínima Aula e menor ou igual a Duração Máxima Aula
                if (duracaoAula < instituicaoNivel.QuantidadeMinutosMinimoAula || duracaoAula > instituicaoNivel.QuantidadeMinutosMaximoAula)
                {
                    ErrorDivisaoTurmaDuracaoAulaException(instituicaoNivel);
                }
            }
        }

        private List<ReportParameter> MontarParametros(ListaFrequenciaFiltroVO filtro)
        {
            var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();

            var parameters = new List<ReportParameter>();

            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Turma", filtro.TurmaDescricao));
            parameters.Add(new ReportParameter("Data", filtro.DiaEmissao?.ToShortDateString()));
            parameters.Add(new ReportParameter("HoraInicio", filtro.HoraInicio?.ToShortTimeString()));
            parameters.Add(new ReportParameter("HoraFim", filtro.HoraFim?.ToShortTimeString()));
            parameters.Add(new ReportParameter("QuantidadeAula", filtro.QuantidadeAula));

            return parameters;
        }

        private Action<object, SubreportProcessingEventArgs> MontarReportAction(List<long> colaboradores, DateTime? dataReferencia, long seqDivisaoTurma)
        {
            // Busca os Colaboradores da divisão de turma
            var listaColaboradores = DivisaoTurmaService.BuscarDivisaoTurmaRelatorioColaborador(seqDivisaoTurma, colaboradores, dataReferencia).ToList();

            // Se existe colaborador na divisão e nenhum foi selecionado, erro
            if (listaColaboradores.SMCCount() > 0 && colaboradores.SMCIsNullOrEmpty())
                throw new Exception("Nenhum Professor foi selecionado!");

            // Busca os dados do cabeçalho
            var listaCabecalho = DivisaoTurmaService.BuscarDivisaoTurmaRelatorioCabecalho(seqDivisaoTurma);

            // Agrupando por disciplina e, depois, por curso, para montar o cabeçalho.
            List<SMCDatasourceItem> listaDisciplinasCursos = new List<SMCDatasourceItem>();
            foreach (var itemGrupo in listaCabecalho.GroupBy(a => a.Disciplina).ToList())
            {
                string itemListaDisciplinaCurso = itemGrupo.Key + Environment.NewLine;
                List<string> listaAdicionada = new List<string>();
                foreach (var item in itemGrupo)
                {
                    if (!listaAdicionada.Contains(item.Curso))
                    {
                        listaAdicionada.Add(item.Curso);
                        itemListaDisciplinaCurso += "\t\t\t\t- " + item.Curso + Environment.NewLine;
                    }
                }
                listaDisciplinasCursos.Add(new SMCDatasourceItem(new Random().Next(), itemListaDisciplinaCurso));
            }

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSListaFrequenciaColaborador", listaColaboradores));
                e.DataSources.Add(new ReportDataSource("DSListaFrequenciaCabecalho", listaDisciplinasCursos));
            };

            return reportAction;
        }

        /// <summary>
        /// Converte a hora, para seu total em minutos
        /// </summary>
        /// <param name="hora"></param>
        /// <returns>horário convertido para minutos</returns>
        private double ConverterHoraParaMinutos(DateTime? hora)
        {
            var timeMinimoInserido = new TimeSpan(hora.Value.Hour, hora.Value.Minute, hora.Value.Second);

            return timeMinimoInserido.TotalMinutes;
        }

        /// <summary>
        /// Converte o valor em minutos para valor formatadao em horas (HH:MM)
        /// </summary>
        /// <param name="quantidadeMinutos"></param>
        /// <returns></returns>
        private string ConverterMinutosEmHoraFormatada(short quantidadeMinutos)
        {
            string formato = @"hh\:mm";

            var time = new TimeSpan(0, quantidadeMinutos, 0).ToString(formato);

            return time;
        }

        private void ErrorDivisaoTurmaDuracaoAulaException(InstituicaoNivelData instituicaoNivel)
        {
            var timeMinimoAula = ConverterMinutosEmHoraFormatada(instituicaoNivel.QuantidadeMinutosMinimoAula);
            var timeMaximoAula = ConverterMinutosEmHoraFormatada(instituicaoNivel.QuantidadeMinutosMaximoAula);

            throw new DivisaoTurmaDuracaoAulaException(timeMinimoAula, timeMaximoAula);
        }
    }
}