using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class RegimeLetivoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short? NumeroItensAno { get; set; }

        public bool AlterarNumero { get; set; }
    }
}
