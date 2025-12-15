using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoHistoricoDownloadListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoDocumentoConclusao { get; set; }

        public TipoArquivoDigital TipoArquivoDigital { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public virtual string EnderecoIP { get; set; }

        public virtual string NomeServidorHost { get; set; }
    }
}
