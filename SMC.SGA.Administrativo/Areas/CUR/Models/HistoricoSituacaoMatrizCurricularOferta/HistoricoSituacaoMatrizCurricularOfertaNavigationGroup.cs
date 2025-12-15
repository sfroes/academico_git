using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class HistoricoSituacaoMatrizCurricularOfertaNavigationGroup : SMCNavigationGroup
    {
        public HistoricoSituacaoMatrizCurricularOfertaNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_MatrizCurricular",
                "Index",
                "MatrizCurricular",
                new string[] { UC_CUR_001_05_01.PESQUISAR_MATRIZ_CURRICULAR },
                parameters: new SMCNavigationParameter("SeqCurriculoCursoOferta", "SeqCurriculoCursoOferta")
            )
            .HideForModel<HistoricoSituacaoMatrizCurricularOfertaDynamicModel>(SMCViewMode.Insert | SMCViewMode.Edit);
        }
    }
}