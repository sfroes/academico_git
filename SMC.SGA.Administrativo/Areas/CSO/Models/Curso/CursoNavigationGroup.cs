using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoNavigationGroup : SMCNavigationGroup
    {
        public CursoNavigationGroup(SMCViewModelBase model) :
            base(model)
        {
            this.AddItem("GRUPO_Alterar",
                         "Editar",
                         "Curso",
                         new string[] { UC_CSO_001_01_02.MANTER_CURSO },
                         parameters: new SMCNavigationParameter("seq", "SeqEntidade"))
                .HideForModel<CursoDynamicModel>();

            this.AddItem("GRUPO_FormacaoEspecifica",
                         "Index",
                         "CursoFormacaoEspecifica",
                         new string[] { UC_CSO_001_01_04.PESQUISAR_FORMACAO_ESPECIFICA_CURSO },
                         parameters: new SMCNavigationParameter("seqCurso", "Seq"))
                .HideForModel<CursoDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<FormacaoEspecificaDynamicModel>();

            this.AddItem("GRUPO_AssociacaoCursoUnidade",
                         "Index",
                         "CursoUnidade",
                         new string[] { UC_CSO_001_01_02.MANTER_CURSO },
                         parameters: new SMCNavigationParameter("seqEntidade", "SeqEntidade"))
                .HideForModel<CursoDynamicModel>(SMCViewMode.List | SMCViewMode.Insert);

            this.AddItem(text: "GRUPO_Curriculo",
                         action: "Index",
                         controller: "Curriculo",
                         securitytokens: new string[] { UC_CUR_001_01_01.PESQUISAR_CURRICULO },
                         area: "CUR",
                         parameters: new SMCNavigationParameter("seqCurso", "SeqEntidade"))
                .HideForModel<CursoDynamicModel>(SMCViewMode.List | SMCViewMode.Insert);

            this.AddItem("GRUPO_AlterarEntidadeHistoricoSituacao",
                         "Index",
                         "CursoHistoricoSituacao",
                         new string[] { UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqEntidade", "SeqEntidade"),
                             new SMCNavigationParameter("seqTipoEntidade", "SeqTipoEntidade")
                         })
                .HideForModel<CursoDynamicModel>(SMCViewMode.List | SMCViewMode.Insert)
                .HideForModel<CursoHistoricoSituacaoDynamicModel>();
        }
    }
}