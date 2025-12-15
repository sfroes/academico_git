using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoOriginalVO : ISMCMappable
    {
        public bool ExigeJustificativa { get; set; }

        public string CriadoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Justificativa { get; set; }

        public string JustificativaComplementar { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}