using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeHierarquiaClassificacaoVO
    {
        public long Seq { get; set; }

        public long? SeqTipoClassificacao { get; set; }

        public string Descricao { get; set; }

        public short? QuantidadeMinima { get; set; }

        public short? QuantidadeMaxima { get; set; }

        public List<ClassificacaoVO> Classificacoes { get; set; }
    }
}
