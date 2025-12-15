using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoAtualizadaVO : ISMCMappable
    {
        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}