using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaLocalidadeNavigationGroup : SMCNavigationGroup
    {
        public CursoOfertaLocalidadeNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                         "Editar",
                         "CursoOfertaLocalidade",
                         new string[] { UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE },
                         parameters: new SMCNavigationParameter("seq", "SeqEntidade"))
                .HideForModel<CursoOfertaLocalidadeDynamicModel>();

            this.AddItem("GRUPO_AlterarEntidadeHistoricoSituacao",
                         "Index",
                         "CursoOfertaLocalidadeHistoricoSituacao",
                         new string[] { UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidade", "Seq"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade")
                         })
                .HideForModel<CursoOfertaLocalidadeDynamicModel>(SMCViewMode.Insert)
                .HideForModel<CursoOfertaLocalidadeHistoricoSituacaoDynamicModel>();
        }
    }
}