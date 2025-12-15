using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models.InstituicaoNivelBeneficio
{
    public class InstituicaoNivelBeneficioNavigationGroup : SMCNavigationGroup
    {
        public InstituicaoNivelBeneficioNavigationGroup(SMCViewModelBase model) : base(model)
        {
            this.AddItem("GRUPO_ConfiguracaoBeneficio",
             "Index",
             "ConfiguracaoBeneficio",
             new string[] { UC_FIN_003_01_05.PESQUISAR_CONFIGURACAO_BENEFICIO },
             parameters: new SMCNavigationParameter[]
             {
                   new SMCNavigationParameter("seqInstituicaoNivelBeneficio", "SeqInstituicaoNivelBeneficio"),
                   new SMCNavigationParameter("seqInstituicaoNivel", "SeqInstituicaoNivel"),
                   new SMCNavigationParameter("seqBeneficio", "SeqBeneficio")
             })
                .HideForModel<ConfiguracaoBeneficioDynamicModel>();

            this.AddItem("GRUPO_BeneficioHistoricoValorAuxilio",
             "Index",
             "BeneficioHistoricoValorAuxilio",
             new string[] { UC_FIN_003_01_03.PESQUISAR_VALOR_AUXILIO_BENEFICIO },
             parameters: new SMCNavigationParameter[]
             {
                   new SMCNavigationParameter("seqInstituicaoNivelBeneficio", "SeqInstituicaoNivelBeneficio"),
                   new SMCNavigationParameter("seqInstituicaoNivel", "SeqInstituicaoNivel"),
                   new SMCNavigationParameter("seqBeneficio", "SeqBeneficio")
             })
                .HideForModel<BeneficioHistoricoValorAuxilioDynamicModel>();
        }


    }
}