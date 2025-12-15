using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoDocumentoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public bool HabilitaCampo { get; set; }

        [SMCKey]
        [SMCHidden]
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        public long? SeqPessoaAtuacao { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCSortable(true, true)]
        public string DescricaoTipoDocumento { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSortable(true, true)]
        public DateTime DataEntrega { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string NumeroProtocoloSolicitado { get; set; }

        [SMCSize(SMCSize.Grid16_24)]
        public string Observacao { get; set; }

        [SMCHidden]
        public Guid? UidArquivo { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public long? SeqTipoDocumento { get; set; }

        [SMCHidden]
        public long? SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public string DescricaoSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool ExibeSolServico
        {
            get
            {
                if (string.IsNullOrEmpty(NumeroProtocoloSolicitado))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ExibeSolServico = value;
            }
        }
    }
}