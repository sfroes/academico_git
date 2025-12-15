using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoData : EntidadeData, ISMCMappable, ISMCSeq​​
    {
        public long SeqNivelEnsino { get; set; }

        public TipoCurso TipoCurso { get; set; }

        public NivelEnsinoHierarquiaData NivelEnsino { get; set; }

        public List<HierarquiaEntidadeItemData> HierarquiasEntidades { get; set; }

        public long[] SeqsHierarquiaEntidadeItem { get; set; }
    }
}