using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoAnexoVO : ISMCMappable
    {
        public long? SeqArquivoAnexado { get; set; }

        public Guid? UidArquivoAnexado { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public DateTime? DataEntrega { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public ArquivoAnexado ArquivoAnexado { get; set; }

        public string Observacao { get; set; }
    }
}