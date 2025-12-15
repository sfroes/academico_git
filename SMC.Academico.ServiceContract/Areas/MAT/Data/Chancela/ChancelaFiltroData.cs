using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ChancelaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public Guid? Codigo { get; set; }

        public bool ApenasProcessoVigente { get; set; }

        public long? SeqProcesso { get; set; }

        public bool ApenasAguardandoChancela { get; set; }
    }
}