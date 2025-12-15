using System;
using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularNavigationGroup : SMCNavigationGroup
    {
        public MatrizCurricularNavigationGroup(SMCViewModelBase model) :
            base(model)
        {
            this.AddItem("GRUPO_MatrizCurricularConfiguracaoComponente",
                         "Index",
                         "MatrizCurricularConfiguracaoComponente",
                         new string[] { UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqMatrizCurricular", "SeqMatrizCurricular"),
                             new SMCNavigationParameter("seqCurriculoCursoOferta", "SeqCurriculoCursoOferta")
                         })
                .HideForModel<MatrizCurricularConfiguracaoComponenteFiltroDynamicModel>()
                .HideForModel<MatrizCurricularDynamicModel>(SMCViewMode.Filter | SMCViewMode.Insert);

            this.AddItem("GRUPO_DivisaoMatrizCurricularComponente",
                         "Index",
                         "ConfiguracaoComponenteMatriz",
                         new string[] { UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqMatrizCurricular", "SeqMatrizCurricular"),
                             new SMCNavigationParameter("seqCurriculoCursoOferta", "SeqCurriculoCursoOferta")
                         })
                .HideForModel<DivisaoMatrizCurricularComponenteDynamicModel>()
                .HideForModel<DivisaoMatrizCurricularComponenteFiltroDynamicModel>()
                .HideForModel<MatrizCurricularDynamicModel>(SMCViewMode.Filter | SMCViewMode.Insert);

            this.AddItem("GRUPO_ConsultaDivisoesMatrizCurricular",
                         "Index",
                         "ConsultaDivisoesMatrizCurricular",
                         new string[] { UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqMatrizCurricular", "SeqMatrizCurricular"),
                             new SMCNavigationParameter("seqCurriculoCursoOferta", "SeqCurriculoCursoOferta")
                         })
                .HideForModel<ConsultaDivisoesMatrizCurricularViewModel>()
                .HideForModel<MatrizCurricularDynamicModel>(SMCViewMode.Filter | SMCViewMode.Insert);

            this.AddItem("GRUPO_Requisito",
                         "Index",
                         "Requisito",
                         new string[] { UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqMatrizCurricular", "SeqMatrizCurricular"),
                             new SMCNavigationParameter("seqCurriculoCursoOferta", "SeqCurriculoCursoOferta")
                         })
                .HideForModel<RequisitoFiltroDynamicModel>()
                .HideForModel<RequisitoDynamicModel>(SMCViewMode.Filter | SMCViewMode.Insert)
                .HideForModel<MatrizCurricularDynamicModel>(SMCViewMode.Filter | SMCViewMode.Insert);
        }
    }
}