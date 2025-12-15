using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoMatrizCurricularComponenteGrupoTreeVO : ISMCMappable, ISMCSeq, ISMCTreeNode
    {
        /// <summary>
        /// Sequencial do GrupoCurricularComponente
        /// </summary>
        public long Seq { get; set; }

        public long? SeqPai { get; set; }

        public string DescricaoGrupo { get; set; }
    }
}
