using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularEmentaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string Ementa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
        
        public ComponenteCurricularData ComponenteCurricular { get; set; }
                
    }
}
