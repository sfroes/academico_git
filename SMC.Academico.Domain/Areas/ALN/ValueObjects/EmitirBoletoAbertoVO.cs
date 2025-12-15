using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class EmitirBoletoAbertoVO
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public string DescricaoServico { get; set; }

        public List<EmitirBoletoAbertoParcelaVO> Parcelas { get; set; }

    }
}
