using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoOriginalData : ISMCMappable
    {
        public string CriadoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Justificativa { get; set; }

        public string JustificativaComplementar { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}