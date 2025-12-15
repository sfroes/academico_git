using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConsultaDivisoesMatrizCurricularVO : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Grupos curriculares com seus componentes que foram associados à matriz curricular mas não foram associados a nenhuma divisão
        /// </summary>
        public GrupoCurricularListaVO[] ComponentesACursar { get; set; }

        public IEnumerable<ConsultaDivisaoMatrizCurricularItemVO> DivisoesMatrizCurricular { get; set; }
    }
}
