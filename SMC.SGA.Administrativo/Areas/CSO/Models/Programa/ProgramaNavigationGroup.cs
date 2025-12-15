using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaNavigationGroup : SMCNavigationGroup
    {
        public ProgramaNavigationGroup(SMCViewModelBase model) :
            base(model)
        {
            this.AddItem(CSO.Views.Programa.App_LocalResources.UIResource.GRUPO_Alterar,
                         "Editar",
                         "Programa",
                         new string[] { UC_CSO_002_01_02.MANTER_PROGRAMA },
                         parameters: new SMCNavigationParameter("seq", "SeqEntidade"))
                .HideForModel<ProgramaDynamicModel>();

            this.AddItem("GRUPO_FormacaoEspecifica",
                         "Index",
                         "FormacaoEspecifica",
                         new string[] { UC_CSO_002_01_03.PESQUISAR_FORMACAO_ESPECIFICA_PROGRAMA },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidadeResponsavel", "SeqEntidade"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade"),
                             new SMCNavigationParameter("seqHierarquiaEntidadeItem", "SeqHierarquiaEntidadeItem")
                         })
                .HideForModel<ProgramaDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<FormacaoEspecificaDynamicModel>();

            this.AddItem("GRUPO_Curso",
                         "Index",
                         "Curso",
                         new string[] { UC_CSO_001_01_01.PESQUISAR_CURSO },
                         parameters: new SMCNavigationParameter("seqEntidade", "SeqHierarquiaEntidadeItem"))
                .HideForModel<ProgramaDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<CursoDynamicModel>();

            this.AddItem("GRUPO_Proposta",
                         "Index",
                         "ProgramaProposta",
                         new string[] { UC_CSO_002_01_05.PESQUISAR_PROPOSTA },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidade", "SeqEntidade"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade"),
                             new SMCNavigationParameter("seqHierarquiaEntidadeItem", "SeqHierarquiaEntidadeItem")
                         })
                .HideForModel<ProgramaDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<ProgramaPropostaDynamicModel>();

            this.AddItem("GRUPO_AlterarEntidadeHistoricoSituacao",
                         "Index",
                         "ProgramaHistoricoSituacao",
                         new string[] { UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidade", "SeqEntidade"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade"),
                             new SMCNavigationParameter("seqHierarquiaEntidadeItem", "SeqHierarquiaEntidadeItem")
                         })
                .HideForModel<ProgramaDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<ProgramaHistoricoSituacaoDynamicModel>();
        }
    }
}