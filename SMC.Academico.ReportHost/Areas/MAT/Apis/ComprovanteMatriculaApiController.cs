using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;

namespace SMC.Academico.ReportHost.Areas.MAT.Apis
{
    public class ComprovanteMatriculaApiController : SMCApiControllerBase
    {
        #region [ Services ]

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IEntidadeImagemService EntidadeImagemService => Create<IEntidadeImagemService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private IAlunoHistoricoService AlunoHistoricoService => Create<IAlunoHistoricoService>();

        private IPessoaService PessoaService => Create<IPessoaService>();

        #endregion [ Services ]

        [HttpPost]
        //[SMCAuthorize(UC_MAT_003_19_02.RELATORIO_COMPROVANTE_MATRICULA)]
        [SMCAllowAnonymous]
        public byte[] ComprovanteMatriculaRelatorio(ComprovanteMatriculaFiltroVO filtro)
        {
            EfetivacaoComprovanteData lista = new EfetivacaoComprovanteData();
            int totalCreditos = 0;

            #region [ Turmas ]

            //Recuperar as turmas da solicitação
            lista.Turmas = new List<EfetivacaoTurmaData>();
            var turmas = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(filtro.SeqSolicitacaoMatricula, filtro.SeqIngressante, null, filtro.SeqProcessoEtapa, true);
            if (turmas.SMCCount() > 0)
            {
                turmas.SMCForEach(t =>
                {
                    totalCreditos += t.Credito.GetValueOrDefault();
                    t.TurmaMatriculaDivisoes.SMCForEach(d =>
                    {
                        lista.Turmas.Add(new EfetivacaoTurmaData()
                        {
                            Seq = t.Seq,
                            Descricao = t.TurmaFormatado,
                            SeqDivisao = d.SeqDivisaoTurma.Value,
                            DescricaoDivisao = d.DivisaoTurmaRelatorioDescricao
                        });
                    });
                });
            }

            #endregion [ Turmas ]

            #region [ Atividades ]

            //Recuperar as atividades da solicitação
            lista.Atividades = new List<EfetivacaoAtividadeData>();
            var atividades = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(filtro.SeqSolicitacaoMatricula, filtro.SeqIngressante, filtro.SeqProcessoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso);
            if (atividades.SMCCount() > 0)
            {
                totalCreditos += atividades.Sum(s => s.Credito).GetValueOrDefault();
                lista.Atividades.AddRange(atividades.Select(s => new EfetivacaoAtividadeData()
                {
                    Seq = s.Seq,
                    Descricao = s.DescricaoFormatada
                }));
            }

            #endregion [ Atividades ]

            #region [ Parcelas ]

            lista.Parcelas = new List<EfetivacaoParcelaData>();
            //var parcelas = SolicitacaoMatriculaService.BuscarCondicoesPagamentoAcademico(seqSolicitacaoMatricula, true);

            //if (parcelas.SMCCount() > 0)
            //{
            //    parcelas.SMCForEach(t =>
            //    {
            //        lista.Parcelas.Add(new EfetivacaoParcelaData()
            //        {
            //            Numero = t.NumeroParcela.ToString(),
            //            Vencimento = t.DataVencimentoParcelas.ToShortDateString(),
            //            Limite = t.DataVencimentoParcelas.AddDays(5).ToShortDateString(),
            //        });
            //    });
            //}

            #endregion [ Parcelas ]

            #region [ DadosPessoais ]

            //Recuperar os dados basicos do relatório
            lista.DadosPessoais = new List<EfetivacaoDadosData>();

            var dadosPessoais = SolicitacaoMatriculaService.BuscarCabecalhoMenu(filtro.SeqSolicitacaoMatricula);

            // No comprovante de matrícula o registro academico a ser apresentado deve ser o código de aluno de migração
            // na falta dele, retornar o próprio SeqPessoaAtuacao
            var registroAcademico = AlunoHistoricoService.BuscarCodigoAlunoMigracao(filtro.SeqIngressante, filtro.SeqSolicitacaoMatricula);

            var codigoPessoaCAD = PessoaService.BuscarCodigoDePessoaNosDadosMestres(dadosPessoais.SeqPessoa, TipoPessoa.Fisica, filtro.SeqIngressante);

            lista.DadosPessoais.Add(new EfetivacaoDadosData()
            {
                Aluno = dadosPessoais.Nome,
                EntidadeResponsavel = dadosPessoais.DescricaoUnidadeResponsavel,
                LocalidadeUnidadeResponsavel = $"{dadosPessoais.LocalidadeUnidadeResponsavel}, {DateTime.Now.Day} de {DateTime.Now.ToString("MMMM")} de {DateTime.Now.Year}",
                Vinculo = dadosPessoais.DescricaoVinculo,
                VinculoInstitucional = dadosPessoais.DescricaoVinculoInstitucional,
                CicloLetivo = dadosPessoais.DescricaoCicloLetivo,
                TotalCreditos = $"Total de Créditos: {totalCreditos}",
                NumeroRegistroAcademico = registroAcademico.ToString(),
                CodigoPessoaCAD = codigoPessoaCAD.ToString(),
            });

            #endregion [ DadosPessoais ]

            #region [ Relatório ]

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSComprovanteMatriculaTurmas", lista.Turmas));

                e.DataSources.Add(new ReportDataSource("DSComprovanteMatriculaAtividades", lista.Atividades));

                e.DataSources.Add(new ReportDataSource("DSComprovanteMatriculaParcelas", lista.Parcelas));
            };

            var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();
            var parameters = new List<ReportParameter>();
            SMCUploadFile imagemCabecalho = EntidadeImagemService.BuscarImagemEntidade(dadosPessoais.SeqInstituicaoEnsino, TipoImagem.LogotipoTimbrado);

            parameters.Add(new ReportParameter("ImagemCabecalho", imagemCabecalho == null ? string.Empty : Convert.ToBase64String(imagemCabecalho.FileData)));

            #endregion [ Relatório ]

            return SMCGenerateReport("Areas/MAT/Reports/Efetivacao/ComprovanteMatriculaRelatorio.rdlc", lista.DadosPessoais, "DSComprovanteMatriculaDados", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);
        }
    }
}