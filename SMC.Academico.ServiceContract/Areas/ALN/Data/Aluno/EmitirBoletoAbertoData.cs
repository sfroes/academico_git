using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EmitirBoletoAbertoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public string DescricaoServico { get; set; }

        public List<EmitirBoletoAbertoParcelaData> Parcelas { get; set; }
    }
}
