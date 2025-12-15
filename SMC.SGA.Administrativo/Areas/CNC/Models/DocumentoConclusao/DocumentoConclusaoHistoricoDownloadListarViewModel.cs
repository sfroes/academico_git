using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoHistoricoDownloadListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoDocumentoConclusao { get; set; }

        [SMCSortable(true, true)]
        public TipoArquivoDigital TipoArquivoDigital { get; set; }

        [SMCSortable(true, true)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public virtual string EnderecoIP { get; set; }

        public virtual string NomeServidorHost { get; set; }
    }
}