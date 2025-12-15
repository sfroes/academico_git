using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class MotivoBloqueioFiltroData : ISMCMappable
    {
        public List<long> SeqTipoBloqueio { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool RemoverIntegracao { get; set; }
    }
}