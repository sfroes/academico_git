using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoDocumentoRequeridoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        [SMCParameter]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }
      
        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public short MinimoObrigatorio { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public bool UploadObrigatorio { get; set; }

        public List<GrupoDocumentoRequeridoItemListarViewModel> Itens { get; set; }

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