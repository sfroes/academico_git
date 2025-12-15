using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class RequisitoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public List<RequisitoItemVO> Itens { get; set; }       
    }
}
