using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    /// <summary>
    /// Representa um grupo de divisão matriz curricular componente
    /// </summary>
    public class DivisaoMatrizCurricularComponenteListarDynamicModel : SMCDynamicViewModel
    {
        /// <summary>
        /// Sequencial do grupo curricular
        /// </summary>
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public List<DivisaoMatrizCurricularComponenteGrupoTreeViewModel> HierarquiaGruposCurriculares { get; set; }

        //public List<SMCTreeViewNode<DivisaoMatrizCurricularComponenteGrupoTreeViewModel>> ArvoreHierarquiaGruposCurriculares
        //{
        //    get { return SMCTreeView.For(this.HierarquiaGruposCurriculares); }
        //}

        public string GrupoCurricularComponente { get; set; }

        public List<GrupoCurricularComponenteListarItemViewModel> GruposCurricularesComponentes { get; set; }
    }
}