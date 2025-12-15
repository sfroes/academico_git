using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CabecalhoCancelamentoSolicitacaoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public string Protocolo { get; set; }

        public string Nome { get; set; }
        public string NomeSocial { get; set; }

        public string Processo { get; set; }

        public string EtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }

        public DateTime DataSolicitacao { get; set; }
        public string DescricaoServico { get; set; }
    }
}