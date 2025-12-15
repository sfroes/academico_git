using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.FilesCollection;
using SMC.Academico.ReportHost.Areas.ALN.App_LocalResources;
using SMC.Academico.ReportHost.Areas.ALN.Models;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.ALN.Apis
{
    public class ReportApiController : SMCApiControllerBase
    {
        #region [ Services ]

        internal IAlunoService AlunoService => Create<IAlunoService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        internal ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        internal IEntidadeImagemService EntidadeImagemService => Create<IEntidadeImagemService>();
        internal IEntidadeService EntidadeService => Create<IEntidadeService>();
        internal IAtoNormativoService AtoNormativoService => Create<IAtoNormativoService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(RelatorioListarVO model)
        {
            ValidarModelo(model);
            var parameters = new List<ReportParameter>();

            switch (model.TipoRelatorio)
            {
                case TipoRelatorio.IdentidadeEstudantil:
                    return IdentidadeEstudantil(model, parameters);

                case TipoRelatorio.DeclaracaoMatricula:
                    return RelatorioDeclaracaoMatricula(model, parameters);

                case TipoRelatorio.DeclaracaoDisciplinasCursadas:
                    return RelatorioDisciplinasCursadas(model, parameters);

                case TipoRelatorio.HistoricoEscolar:
                    return RelatorioHistoricoEscolar(model, parameters);

                case TipoRelatorio.HistoricoEscolarInterno:
                    return RelatorioHistoricoEscolarInterno(model, parameters);

                case TipoRelatorio.ListagemAssinatura:
                    return RelatorioListagemAssinatura(model, parameters);
            }

            throw new Exception("Relatório não encontrado");
        }

        private List<ReportParameter> AdicionarParametrosNomeInstituicaoImagemCabecalho(long seqInstituicaoEnsino)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoEnsino);
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            return parameters;
        }

        private byte[] IdentidadeEstudantil(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            var lista = AlunoService.BuscarAlunosIdentidadeEstudantil(model.SelectedValues);
            parameters.AddRange(AdicionarParametrosNomeInstituicaoImagemCabecalho(model.SeqInstituicaoEnsino));

            var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/IdentidadeEstudantilRelatorio.rdlc", lista, "DSIdentidadeEstudantil", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Custom, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Identidade estudantil", "application/pdf");

            return arquivo;
        }

        private byte[] RelatorioDeclaracaoMatricula(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            var items = AlunoService.BuscarItemsDeclaracaoMatricula(model.SelectedValues.ToArray(), model.SeqCicloLetivo.Seq.Value);
            var itemsLista = RemoverRegistrosNulos(items, model.SeqCicloLetivo.Seq.Value, model.SelectedValues.ToArray());

            var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSDeclaracaoMatriculaTurma", itemsLista.Where(w => w.Turma).ToList()));
                e.DataSources.Add(new ReportDataSource("DSDeclaracaoMatriculaAtividade", itemsLista.Where(w => !w.Turma).ToList()));
            };

            long seqInstituicaoEnsino = items.Any() ? items.FirstOrDefault().SeqInstituicaoEnsino : 0L;
            string descricaoAtoNormativo = BuscarDescricaoAtoNormativo(seqInstituicaoEnsino);
            SMCUploadFile imagemCabecalho = EntidadeImagemService.BuscarImagemEntidade(seqInstituicaoEnsino, TipoImagem.LogotipoTimbrado);
            EntidadeRodapeData rodapeEntidadeData = EntidadeService.BuscarInformacoesRodapeEntidade(SIGLA_ENTIDADE.CRA, TOKEN_TIPO_ENTIDADE.SETOR);
            string rodapeEntidade = rodapeEntidadeData == null ? string.Empty : rodapeEntidadeData.ToString();

            parameters.Add(new ReportParameter("ImagemCabecalho", imagemCabecalho == null ? string.Empty : Convert.ToBase64String(imagemCabecalho.FileData)));
            parameters.Add(new ReportParameter("DescricaoAtoNormativo", descricaoAtoNormativo));
            parameters.Add(new ReportParameter("DataLocalizacaoInstituicao", BuscarLocalDataString(instituicao)));
            parameters.Add(new ReportParameter("EntidadeResponsavel", items.FirstOrDefault().NomeEntidade));
            parameters.Add(new ReportParameter("NomeUsuarioLogado", model.NomeUsuarioLogado));
            parameters.Add(new ReportParameter("RodapeEntidade", rodapeEntidade));

            var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/DeclaracaoMatriculaRelatorio.rdlc", itemsLista, "DSDeclaracaoMatricula", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Declaração de matrícula", "application/pdf");
            return arquivo;
        }

        private byte[] RelatorioDisciplinasCursadas(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            var lista = AlunoService.RelatorioDisciplinasCursadas(model.Transform<RelatorioDisciplinasCursadasFiltroData>());

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var subDataSourceComponentesCurricularesHoraAula = lista.SelectMany(a => a.ComponentesCurriculares).Where(c => c.CargaHorariaAula.HasValue).ToList();
                e.DataSources.Add(new ReportDataSource("DSComponenteCurricularHoraAula", subDataSourceComponentesCurricularesHoraAula));

                var subDataSourceComponentesCurricularesHora = lista.SelectMany(a => a.ComponentesCurriculares).Where(c => c.CargaHorariaRelogio.HasValue || !c.CargaHorariaAula.HasValue).ToList();
                e.DataSources.Add(new ReportDataSource("DSComponenteCurricularHora", subDataSourceComponentesCurricularesHora));

                var subDataSourceEmentas = lista.SelectMany(a => a.Ementas).SMCDistinct(d => new { d.DescricaoComponenteCurricular, d.DescricaoComponenteCurricularAssunto, d.Ementa }).ToList();
                e.DataSources.Add(new ReportDataSource("DSEmenta", subDataSourceEmentas));
            };

            long seqInstituicaoEnsino = lista.Any() ? lista.FirstOrDefault().SeqInstituicaoEnsino : 0L;
            string descricaoAtoNormativo = BuscarDescricaoAtoNormativo(seqInstituicaoEnsino);
            SMCUploadFile imagemCabecalho = EntidadeImagemService.BuscarImagemEntidade(seqInstituicaoEnsino, TipoImagem.LogotipoTimbrado);
            EntidadeRodapeData rodapeEntidadeData = EntidadeService.BuscarInformacoesRodapeEntidade(SIGLA_ENTIDADE.CRA, TOKEN_TIPO_ENTIDADE.SETOR);
            string rodapeEntidade = rodapeEntidadeData == null ? string.Empty : rodapeEntidadeData.ToString();

            parameters.Add(new ReportParameter("ImagemCabecalho", imagemCabecalho == null ? string.Empty : Convert.ToBase64String(imagemCabecalho.FileData)));
            parameters.Add(new ReportParameter("DescricaoAtoNormativo", descricaoAtoNormativo));
            parameters.Add(new ReportParameter("NomeUsuarioLogado", model.NomeUsuarioLogado));
            parameters.Add(new ReportParameter("RodapeEntidade", rodapeEntidade));

            var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/DisciplinaCursadaRelatorio.rdlc", lista, "DSDisciplinaCursada", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Declaração de disciplinas cursadas", "application/pdf");
            return arquivo;

        }

        private byte[] RelatorioHistoricoEscolar(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            var lista = AlunoService.RelatorioHistoricoEscolar(model.SelectedValues, model.ImprimirComponenteCurricularSemCreditos, model.ExibirMediaNotas);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var subDataSourceBancas = lista.SelectMany(a => a.TrabalhosAcademicos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarBancas", subDataSourceBancas));

                var subDataSourceBancasAvaliacoes = subDataSourceBancas.SelectMany(a => a.Avaliacoes).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarBancasAvaliacoes", subDataSourceBancasAvaliacoes));

                var subDataSourceBancasAvaliacoesComissao = subDataSourceBancasAvaliacoes.SelectMany(a => a.ComissaoExaminadora).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarBancasAvaliacoesComissao", subDataSourceBancasAvaliacoesComissao));

                var subDataSourceFormacoes = lista.SelectMany(a => a.FormacaoEspecifica).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarFormacaoEspecifica", subDataSourceFormacoes));

                var subDataSourceAdmissaoObservacoes = lista.SelectMany(a => a.AdmissaoObservacao).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarAdmissaoObservacoes", subDataSourceAdmissaoObservacoes));

                //var subDataSourceExame = lista.SelectMany(a => a.ComponentesExame).ToList();
                //e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarComponentesExame", subDataSourceExame));

                var subDataSourceAproveitamentoCreditos = lista.SelectMany(a => a.AproveitamentoCreditos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarAproveitamentoCreditos", subDataSourceAproveitamentoCreditos));

                var subDataSourceComponentesConcluidos = lista.SelectMany(a => a.ComponentesConcluidos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarComponentesConcluidos", subDataSourceComponentesConcluidos));

                var subDataSourceTiposMobilidade = lista.SelectMany(a => a.TiposMobilidade).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarTiposMobilidade", subDataSourceTiposMobilidade));

                var subDataSourceObservacoes = lista.SelectMany(a => a.Observacoes).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarObservacao", subDataSourceObservacoes));
            };

            long seqInstituicaoEnsino = lista.Any() ? lista.FirstOrDefault().SeqInstituicaoEnsino : 0L;
            string descricaoAtoNormativo = BuscarDescricaoAtoNormativo(seqInstituicaoEnsino);

            EntidadeRodapeData rodapeEntidadeData = EntidadeService.BuscarInformacoesRodapeEntidade(SIGLA_ENTIDADE.CRA, TOKEN_TIPO_ENTIDADE.SETOR);
            SMCUploadFile imagemCabecalho = EntidadeImagemService.BuscarImagemEntidade(seqInstituicaoEnsino, TipoImagem.LogotipoTimbrado);
            string rodapeEntidade = rodapeEntidadeData == null ? string.Empty : rodapeEntidadeData.ToString();

            parameters.Add(new ReportParameter("ImagemCabecalho", imagemCabecalho == null ? string.Empty : Convert.ToBase64String(imagemCabecalho.FileData)));
            parameters.Add(new ReportParameter("NomeUsuarioLogado", model.NomeUsuarioLogado));
            parameters.Add(new ReportParameter("DescricaoAtoNormativo", descricaoAtoNormativo));
            parameters.Add(new ReportParameter("RodapeEntidade", rodapeEntidade));

            var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/HistoricoEscolarRelatorio.rdlc", lista, "DSHistoricoEscolar", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Custom, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Histórico escolar", "application/pdf");
            return arquivo;
        }

        private string BuscarDescricaoAtoNormativo(long seqInstituicaoEnsino)
        {
            DadosAtoNormativoData atoNormativo = AtoNormativoService.BuscarUltimoAtoNormativoVigente(seqInstituicaoEnsino);
            return (atoNormativo?.Descricao != null && !string.IsNullOrWhiteSpace(atoNormativo?.Descricao))
                    ? atoNormativo.Descricao : string.Empty;
        }

        private byte[] RelatorioHistoricoEscolarInterno(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            parameters.AddRange(AdicionarParametrosNomeInstituicaoImagemCabecalho(model.SeqInstituicaoEnsino));
            var lista = AlunoService.RelatorioHistoricoEscolarInterno(model.SelectedValues);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                //var subDataSourceExame = lista.SelectMany(a => a.ComponentesExame).ToList();
                //e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarComponentesExame", subDataSourceExame));

                var subDataSourceAproveitamentoCreditos = lista.SelectMany(a => a.AproveitamentoCreditos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarAproveitamentoCreditos", subDataSourceAproveitamentoCreditos));

                var subDataSourceComponentesConcluidos = lista.SelectMany(a => a.ComponentesConcluidos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarComponentesConcluidos", subDataSourceComponentesConcluidos));

                var subDataSourceComponentesSemApuracao = lista.SelectMany(a => a.ComponentesSemApuracao).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarComponentesSemApuracao", subDataSourceComponentesSemApuracao));

                var subDataSourceTotaisComponentesConcluidos = lista.SelectMany(a => a.TotaisComponentesConcluidos).ToList();
                e.DataSources.Add(new ReportDataSource("DSHistoricoEscolarTotaisComponentesConcluidos", subDataSourceTotaisComponentesConcluidos));
            };

            var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("NomeUsuarioLogado", model.NomeUsuarioLogado));
            parameters.Add(new ReportParameter("ExibirProfessor", model.ExibeProfessor.GetValueOrDefault().ToString()));

            var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/HistoricoEscolarInternoRelatorio.rdlc", lista, "DSHistoricoEscolar", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Custom, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Histórico escolar", "application/pdf");


            return arquivo;

        }

        private byte[] RelatorioListagemAssinatura(RelatorioListarVO model, List<ReportParameter> parameters)
        {
            parameters.AddRange(AdicionarParametrosNomeInstituicaoImagemCabecalho(model.SeqInstituicaoEnsino));
            var filtroAluno = new RelatorioFiltroData() { Seqs = model.SelectedValues.ToArray() };

            var result = AlunoService.BuscarAlunosRelatorio(filtroAluno, true).TransformList<RelatorioListarItemVO>();

            // Como o método para buscar alunos é usado também na pesquisa de quem vai aparecer no relatório, 
            // a concatenação do ciclo com o nome será aqui
            result.SMCForEach(c => c.Nome = $"{c.Nome} - {c.DescricaoCicloLetivo}");

            var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();

            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("TituloRelatorio", model.TituloListagem));
            parameters.Add(new ReportParameter("ExibirCampoAssinatura", model.ExibirCampoAssinatura.GetValueOrDefault().ToString()));

             var retorno = SMCGenerateReport("Areas/ALN/Reports/Relatorio/ListagemAssinatura.rdlc", result, "DSListagemAssinatura", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Portrait, parameters);

            var arquivo = SautinSoftHelper.ConvertDocumentPdf(retorno, "Listagem assinatura", "application/pdf");

            return arquivo;
        }

        private List<ItemDeclaracaoMatriculaData> RemoverRegistrosNulos(List<ItemDeclaracaoMatriculaData> items, long seqCicloLetivo, long[] alunosSelecionados)
        {
            var itens = items.Where(c => !string.IsNullOrEmpty(c.DescricaoComponenteCurricular)).ToList();

            ValidarDeclaracaoMatriculaAlunos(itens, seqCicloLetivo, alunosSelecionados);

            return items;
        }

        /// <summary>
        /// Validar Alunos com Matrícula
        /// </summary>
        /// <param name="itens"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <param name="alunosSelecionados"></param>
        private void ValidarDeclaracaoMatriculaAlunos(List<ItemDeclaracaoMatriculaData> itens, long seqCicloLetivo, long[] alunosSelecionados)
        {
            ///Validar Alunos com Matrícula
            if (!itens.SMCAny())
            {
                var alunosResultado = AlunoService.BuscarAlunosRelatorio(new ServiceContract.Areas.ALN.Data.RelatorioFiltroData() { NumerosRegistrosAcademicos = alunosSelecionados.ToList() });

                var NomesAlunos = alunosResultado.TransformList<RelatorioListarItemVO>().Select(a => $"{a.NumeroRegistroAcademico} - {a.Nome}");

                var cicloLetivoDescricao = CicloLetivoService.BuscarDescricaoFormatadaCicloLetivo(seqCicloLetivo);

                throw new SMCApplicationException(string.Format(UIResource.MSG_AlunoSemDeclaracao, cicloLetivoDescricao, string.Join(",<br>", NomesAlunos)));
            }
        }

        private string BuscarLocalDataString(InstituicaoEnsinoData instituicao)
        {
            var cidade = ((EnderecoData)instituicao.Enderecos.SMCFirst())?.NomeCidade ?? string.Empty;
            var dataExtenso = SMCDateTimeExtension.SMCDataPorExtenso(DateTime.Now);

            return cidade + ", " + dataExtenso;
        }

        private void ValidarModelo(RelatorioListarVO model)
        {
            if ((model.SelectedValues == null || !model.SelectedValues.Any()) && model.TipoRelatorio == TipoRelatorio.Nenhum)
                throw new SMCApplicationException("Clique no botão 'Pesquisar' e selecione um ou mais alunos para gerar o relatório.");
            else if (model.SelectedValues == null || !model.SelectedValues.Any())
                throw new SMCApplicationException("Favor selecionar ao menos um aluno para gerar o relatório");
        }

        [HttpGet]
        [AllowAnonymous]
        public string Teste()
        {
            return "OK";
        }
    }
}