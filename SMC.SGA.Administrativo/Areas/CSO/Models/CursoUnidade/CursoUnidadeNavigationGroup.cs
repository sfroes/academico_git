using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoUnidadeNavigationGroup : SMCNavigationGroup
    {
        public CursoUnidadeNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                         "Editar",
                         "CursoUnidade",
                         new string[] { UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE },
                         parameters: new SMCNavigationParameter("seq", "SeqEntidade"))
                .HideForModel<CursoUnidadeDynamicModel>();

            this.AddItem("GRUPO_OfertaCursoLocalidade",
                         "Incluir",
                         "CursoOfertaLocalidade",
                         new string[] { UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE },
                         parameters: new SMCNavigationParameter("seqEntidade", "Seq"))
                .HideForModel<CursoUnidadeDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<CursoOfertaLocalidadeDynamicModel>();

            this.AddItem("GRUPO_AlterarEntidadeHistoricoSituacao",
                         "Index",
                         "CursoUnidadeHistoricoSituacao",
                         new string[] { UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidade", "SeqEntidade"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade")
                         })
                .HideForModel<CursoUnidadeDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<CursoUnidadeHistoricoSituacaoDynamicModel>();
        }
    }
}