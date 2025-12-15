using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class RequisitoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public List<RequisitoItemData> Itens { get; set; }
    }
}
