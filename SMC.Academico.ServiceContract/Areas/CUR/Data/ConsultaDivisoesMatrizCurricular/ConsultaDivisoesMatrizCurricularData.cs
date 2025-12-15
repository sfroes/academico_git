using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConsultaDivisoesMatrizCurricularData : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Grupos curriculares com seus componentes que foram associados à matriz curricular mas não foram associados a nenhuma divisão
        /// </summary>
        public GrupoCurricularListaData[] ComponentesACursar { get; set; }

        public List<ConsultaDivisaoMatrizCurricularItemData> DivisoesMatrizCurricular { get; set; }
    }
}
