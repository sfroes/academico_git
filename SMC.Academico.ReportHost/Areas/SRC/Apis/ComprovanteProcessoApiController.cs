using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;

namespace SMC.Academico.ReportHost.Areas.SRC.Apis
{
    public class ComprovanteProcessoApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService => Create<IAlunoService>();
        private IEntidadeImagemService EntidadeImagemService => Create<IEntidadeImagemService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IPessoaService PessoaService => Create<IPessoaService>();

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        private ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        #endregion [ Services ]

        [HttpPost]
        //[SMCAuthorize(UC_MAT_003_19_02.RELATORIO_COMPROVANTE_MATRICULA)]
        [SMCAllowAnonymous]
        public byte[] ComprovanteProcessosRelatorio(ComprovanteProcessoFiltroVO filtro)
        {
            ComprovanteProcessosData lista = new ComprovanteProcessosData();
            int totalCreditos = 0;

            #region [ Turmas ]

            //Recuperar as turmas da solicitação
            lista.Turmas = new List<ComprovanteProcessosTurmaData>();
            var turmas = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(filtro.SeqSolicitacaoMatricula, 0, null, filtro.SeqProcessoEtapa, true);
            if (turmas.SMCCount() > 0)
            {
                turmas.SMCForEach(t =>
                {
                    totalCreditos += t.Credito.GetValueOrDefault();
                    t.TurmaMatriculaDivisoes.SMCForEach(d =>
                    {
                        lista.Turmas.Add(new ComprovanteProcessosTurmaData()
                        {
                            Seq = t.Seq,
                            Descricao = t.TurmaFormatado,
                            SeqDivisao = d.SeqDivisaoTurma.Value,
                            DescricaoDivisao = d.DivisaoTurmaRelatorioDescricao,
                            Situacao = d.Situacao,
                            Motivo = d.Motivo,
                        });
                    });
                });
            }

            #endregion [ Turmas ]

            #region [ Atividades ]

            //Recuperar as atividades da solicitação
            lista.Atividades = new List<ComprovanteProcessosAtividadeData>();
            var atividades = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(filtro.SeqSolicitacaoMatricula, filtro.SeqPessoaAtuacao, filtro.SeqProcessoEtapa, null);
            if (atividades.SMCCount() > 0)
            {
                totalCreditos += atividades.Sum(s => s.Credito).GetValueOrDefault();
                lista.Atividades.AddRange(atividades.Select(s => new ComprovanteProcessosAtividadeData()
                {
                    Seq = s.Seq,
                    Descricao = s.DescricaoFormatada,
                    Situacao = s.SituacaoEtapa,
                    Motivo = s.MotivoEtapa
                }));
            }

            #endregion [ Atividades ]

            #region [ DadosPessoais ]

            //Recuperar os dados basicos do relatório
            lista.DadosPessoais = new List<ComprovanteProcessosDadosData>();
            var dadosPessoais = SolicitacaoMatriculaService.BuscarCabecalhoMenu(filtro.SeqSolicitacaoMatricula);
            var tituloProcesso = SolicitacaoServicoService.BuscarSolicitacaoServico(filtro.SeqSolicitacaoMatricula);
            var registroAcademico = AlunoService.BuscarRegistroAcademicoAluno(filtro.SeqPessoaAtuacao);
            var codigoPessoaCAD = PessoaService.BuscarCodigoDePessoaNosDadosMestres(dadosPessoais.SeqPessoa, TipoPessoa.Fisica, filtro.SeqPessoaAtuacao);
            lista.DadosPessoais.Add(new ComprovanteProcessosDadosData()
            {
                Aluno = dadosPessoais.Nome,
                EntidadeResponsavel = dadosPessoais.DescricaoUnidadeResponsavel,
                LocalidadeUnidadeResponsavel = $"{dadosPessoais.LocalidadeUnidadeResponsavel}, {DateTime.Now.Day} de {DateTime.Now.ToString("MMMM")} de {DateTime.Now.Year}",
                Vinculo = dadosPessoais.DescricaoVinculo,
                VinculoInstitucional = dadosPessoais.DescricaoVinculoInstitucional,
                Titulo = tituloProcesso.DescricaoServico,
                TotalCreditos = $"Total de Créditos: {totalCreditos}",
                NumeroRegistroAcademico = registroAcademico.ToString(),
                CodigoPessoaCAD = codigoPessoaCAD.ToString(),
                TotalTurmas = lista.Turmas.Count,
                TotalAtividades = lista.Atividades.Count,
            });

            #endregion [ DadosPessoais ]

            #region [ Relatório ]

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSComprovanteProcessosTurmas", lista.Turmas));

                e.DataSources.Add(new ReportDataSource("DSComprovanteProcessosAtividades", lista.Atividades));
            };

            SMCUploadFile imagemCabecalho = EntidadeImagemService.BuscarImagemEntidade(dadosPessoais.SeqInstituicaoEnsino, TipoImagem.LogotipoTimbrado);


            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", imagemCabecalho == null ? string.Empty : Convert.ToBase64String(imagemCabecalho.FileData)));

            #endregion [ Relatório ]

            return SMCGenerateReport("Areas/SRC/Reports/ComprovanteProcessos/ComprovanteProcessosRelatorio.rdlc", lista.DadosPessoais, "DSComprovanteProcessosDados", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);
        }
    }
}