using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoApostilamentoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCParameter]
        [SMCFilter(true, true)]
        public long SeqDocumentoConclusao { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public DateTime DataInclusao { get; set; }

        #endregion

        #region Habilitar Botoes

        [SMCHidden]
        public ClasseSituacaoDocumento ClasseSituacaoDocumentoAtual { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovo
        {
            get
            {
                return ClasseSituacaoDocumentoAtual == ClasseSituacaoDocumento.Valido;
            }
        }

        [SMCHidden]
        public string MensagemBotaoNovo
        {
            get
            {
                if (this.ClasseSituacaoDocumentoAtual != ClasseSituacaoDocumento.Valido)
                {
                    return "MensagemDesabilitaBotaoNovo";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}