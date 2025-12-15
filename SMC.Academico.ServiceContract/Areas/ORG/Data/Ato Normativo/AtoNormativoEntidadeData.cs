using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AtoNormativoEntidadeData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqAtoNormativo { get; set; }

        public long? SeqEntidade { get; set; }

        public string Nome { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public bool HabilitaCampo { get; set; }
    }
}