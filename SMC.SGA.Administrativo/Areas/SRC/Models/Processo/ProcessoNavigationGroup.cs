using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoNavigationGroup : SMCNavigationGroup
    {
        public ProcessoNavigationGroup(SMCViewModelBase model) :
            base(model)
        {
            this.AddItem("GRUPO_GrupoEscalonamento",
                         "Index",
                         "GrupoEscalonamento",
                         new string[] { UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqProcesso", "SeqProcesso"),
                         })
                .HideForModel<GrupoEscalonamentoDynamicModel>()
                .HideForModel<GrupoEscalonamentoListarDynamicModel>();

            this.AddItem("GRUPO_Escalonamento",
                         "Index",
                         "Escalonamento",
                         new string[] { UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqProcesso", "SeqProcesso"),
                         })
                .HideForModel<SolicitacaoServicoFiltroViewModel>()
                .HideForModel<SolicitacaoServicoListarViewModel>()
                .HideForModel<PosicaoConsolidadaDynamicModel>()
                .HideForModel<PosicaoConsolidadaFiltroDynamicModel>()
                .HideForModel<PosicaoConsolidadaListarDynamicModel>();

            this.AddItem("GRUPO_SolicitacaoServico",
                 "Index",
                 "SolicitacaoServico",
                 new string[] { UC_SRC_004_01_01.PESQUISAR_SOLICITACAO },
                 parameters: new SMCNavigationParameter[]
                 {
                                 new SMCNavigationParameter("seqProcesso", "SeqProcesso"),
                 })
                .HideForModel<SolicitacaoServicoFiltroViewModel>()
                .HideForModel<SolicitacaoServicoListarViewModel>();

            this.AddItem("GRUPO_PosicaoConsolidada",
                         "Index",
                         "PosicaoConsolidada",
                         new string[] { UC_SRC_005_01_01.CONSULTAR_POSICAO_CONSOLIDADA },
                         parameters: new SMCNavigationParameter[]
                         {
                             new SMCNavigationParameter("seqProcesso", "SeqProcesso"),
                         })
                .HideForModel<PosicaoConsolidadaDynamicModel>()
                .HideForModel<PosicaoConsolidadaFiltroDynamicModel>()
                .HideForModel<PosicaoConsolidadaListarDynamicModel>();
        }
    }
}