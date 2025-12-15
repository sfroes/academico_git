using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoMatrizCurricularComponenteGrupoListaData : ISMCMappable, ISMCSeq
    {
        /// <summary>
        /// Sequencial do GrupoCurricularComponente
        /// </summary>
        public long Seq { get; set; }
              
        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public string GrupoCurricularComponente { get; set; }

        //public List<DivisaoMatrizCurricularComponenteGrupoTreeData> HierarquiaGruposCurriculares { get; set; }

        public List<GrupoCurricularComponenteListarData> GruposCurricularesComponentes { get; set; }
    }
}
