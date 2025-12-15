using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoAtualizadaVO : ISMCMappable
    {
        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}