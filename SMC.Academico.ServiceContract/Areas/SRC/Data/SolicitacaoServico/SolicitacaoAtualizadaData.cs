using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoAtualizadaData : ISMCMappable
    {
        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}