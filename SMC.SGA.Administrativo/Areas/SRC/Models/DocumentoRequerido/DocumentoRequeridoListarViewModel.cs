using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentoRequeridoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        [SMCParameter]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCSortable(true, true)]
        public string DescricaoTipoDocumento { get; set; }

        [SMCSortable(true, false)]
        public bool Obrigatorio { get; set; }

        [SMCSortable(true, false)]
        public bool PermiteUploadArquivo { get; set; }

        [SMCSortable(true, false)]
        public bool ObrigatorioUpload { get; set; }

        public VersaoDocumento VersaoDocumento { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotaoSituacaoEtapaEncerrada";
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotaoSituacaoEtapaLiberada";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}