using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConsultaDivisoesMatrizCurricularViewModel : SMCViewModelBase
    {
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Grupos curriculares com seus componentes que foram associados à matriz curricular mas não foram associados a nenhuma divisão
        /// </summary>
        public GrupoCurricularListarDynamicModel[] ComponentesACursar { get; set; }

        public List<SMCTreeViewNode<GrupoCurricularListarDynamicModel>> ComponentesACursarTree
        {
            get { return SMCTreeView.For(ComponentesACursar); }
        }

        public List<ConsultaDivisaoMatrizCurricularItemViewModel> DivisoesMatrizCurricular { get; set; }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }
    }
}