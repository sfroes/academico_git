using SMC.SGA.Administrativo.Areas.ALN.Views.Relatorio.App_LocalResources;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework;
using SMC.Academico.Common.Areas.ALN.Enums;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConfigurarRelatorioDeclaracaoGenericaViewModel : SMCViewModelBase
    {
        #region CABECALHO

        public string NomeCivil { get; set; }
        [SMCValueEmpty("-")]
        public string NomeSocial { get; set; }
        public string DescricaoNivelEnsino { get; set; }
        public string DescricaoTipoDocumentoAcademico { get; set; }
        public string DescricaoIdioma { get; set; }

        #endregion CABECALHO

        [SMCHidden]
        public TipoRelatorio TipoRelatorio { get; set; }
        [SMCHidden]
        public long SeqNivelEnsinoPorGrupoDocumentoAcademico { get; set; }
        [SMCHidden]
        public long SeqTipoDocumentoAcademico { get; set; }
        [SMCHidden]
        public SMCLanguage IdiomaDocumentoAcademico { get; set; }
        [SMCHidden]
        public long SeqAluno { get; set; }
        [SMCHidden]
        public long SeqDadosPessoais { get; set; }
        [SMCHidden]
        public int? CodigoUnidadeSeo { get; set; }
        [SMCHidden]
        public int? CodigoAlunoMigracao { get; set; }
        [SMCHidden]
        public string NomeAlunoOficial { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        public string MensagemInformativa => UIResource.Mensagem_Informativa;

        public SMCMasterDetailList<ConfigurarTagsDeclaracaoGenericaViewModel> Tags { get; set; }
    }
}