using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        [SMCParameter]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public long NumeroRegistroAcademico { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NomeAluno { get; set; }

        [SMCHidden]
        public long? SeqEntidadeCursoOfertaLocalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NomeEntidadeCursoOfertaLocalidade { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        [SMCHidden]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public short NumeroViaDocumento { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroViaDocumentoFormatado
        {
            get
            {
                if (NumeroViaDocumento == 0)
                    return $"-";

                return $"{NumeroViaDocumento}°";
            }
        }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool HabilitarBotaoExcluir { get; set; }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if (!this.HabilitarBotaoApostilamento)
                {
                    return "MensagemDesabilitaBotaoExcluir";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoApostilamento { get; set; }

        [SMCHidden]
        public string MensagemBotaoApostilamento
        {
            get
            {
                if (!this.HabilitarBotaoApostilamento)
                {
                    return "MensagemDesabilitaBotaoApostilamento";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}