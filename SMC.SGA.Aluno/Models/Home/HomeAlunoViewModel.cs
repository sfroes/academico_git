using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.ALN.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Models
{
    public class HomeAlunoViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        [SMCSelect(nameof(CiclosLetivos))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid20_24, SMCSize.Grid18_24, SMCSize.Grid14_24)]
        [SMCRequired]
        public long? SeqCicloLetivo { get; set; }

        public List<HomeCursoViewModel> Cursos { get; set; }

        public List<PlanoEstudoItemViewModel> Atividades { get; set; }

        public List<HomePublicacaoBdpViewModel> Publicacoes { get; set; }

        public int TotalSolicitacoesNovas { get; set; }

        public int TotalSolicitacoesEmAndamento { get; set; }

        public int TotalSolicitacoesConcluidas { get; set; }

        public int TotalSolicitacoesEncerradas { get; set; }

        public long? SeqPublicacaoBdp { get; set; }

        public bool ExibirIntegralizacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public bool RedirecionarTermoCiencia { get; set; }

        public string UrlTermoCiencia { get; set; }

        #region Questionário Survey Monkey

        public int? CodigoAlunoMigracao { get; set; }

        public bool RedirecionarQuestionarioSurveyMonkey { get; set; }

        public string UrlQuestionarioSurveyMonkey { get; set; }

        #endregion Questionário Survey Monkey

        #region Dados da renovação

        public bool ExisteRematriculaAberta { get; set; }

        public DateTime? DataInicioRematricula { get; set; }

        public DateTime? DataFimRematricula { get; set; }

        public long? SeqSolicitacaoRematricula { get; set; }

        public TipoMatricula? TipoMatricula { get; set; }

        #endregion Dados da renovação

        #region Avaliação CPA (Concluinte)

        public bool ExibirBannerAvaliacaoCpa { get; set; }

        public int CodigoAmostraPpa { get; set; }

        public string UrlAvaliacaoCpa { get; set; }

        #endregion Avaliação CPA (Concluinte)

        #region Avaliação CPA (Semestral)

        public bool ExibirBannerAvaliacaoSemestralCpa { get; set; }

        public string UrlAvaliacaoSemestralCpa { get; set; }

        #endregion  Avaliação CPA (Semestral)

        #region TESTE_FILE

        [SMCFilter(true, true)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile Arquivo { get; set; }

        public string LinkUrl { get; set; }

        #endregion

        #region Validação de exibição aluno

        public bool PermitirVisualizarBannerTurmasLinhaTempo { get; set; }
        public bool PermitiPesquisarSolicitacaoAluno { get; set; }
        public bool PermitirExibirIntegralizacaoCurricular { get; set; }

        #endregion
    }
}