using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoMatrizCurricularComponenteGrupoListarVO : ISMCMappable, ISMCSeq
    {
        /// <summary>
        /// Sequencial do GrupoCurricularComponente
        /// </summary>
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public string GrupoCurricularComponente { get; set; }

        //public IEnumerable<DivisaoMatrizCurricularComponenteGrupoTreeVO> HierarquiaGruposCurriculares { get; set; }

        public List<GrupoCurricularComponenteListarVO> GruposCurricularesComponentes { get; set; }
    }
}
