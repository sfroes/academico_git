using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteGrupoTreeViewModel : SMCViewModelBase, ISMCMappable, ISMCSeq, ISMCTreeNode
    {
        /// <summary>
        /// Sequencial do GrupoCurricularComponente
        /// </summary>
        [SMCKey]
        public long Seq { get; set; }

        public long? SeqPai { get; set; }

        [SMCDescription]
        public string DescricaoGrupo { get; set; }
    }
}