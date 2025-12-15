using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.FIN.Lookups;
using SMC.Academico.UI.Mvc.Areas.FIN.Lookups.LK_FIN_001_Pessoa_Juridica;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel : SMCViewModelBase
    {
        [PessoaJuridicaLookup]
        [SMCConditionalReadonly("IdAssociarResponsavelFinanceiro", SMCConditionalOperation.Equals, (int)SMC.Academico.Common.Areas.FIN.Enums.AssociarResponsavelFinanceiro.Exige, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly("IdExisteResponsaveisFinanceiros", SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalReadonly("CamposSomenteLeituraPossuiControleFinanceiro", SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R3")]
        [SMCConditionalRule("(R1 && R2) || R3")]        
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid14_24, SMCSize.Grid12_24, SMCSize.Grid14_24)]        
        public PessoaJuridicaLookupViewModel ResponsaveisFinanceiro { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public long? SeqPessoaJuridica
        {
            get { return ResponsaveisFinanceiro?.Seq; }
            set
            {
                ResponsaveisFinanceiro = ResponsaveisFinanceiro ?? new PessoaJuridicaLookupViewModel();
                ResponsaveisFinanceiro.Seq = value.GetValueOrDefault();
            }
        }

        [SMCDependency(nameof(PessoaAtuacaoBeneficioDynamicModel.SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarTipoResponsavelFinanceiroSelect), "PessoaAtuacaoBeneficio", true)]
        [SMCRequired]
        [SMCSelect(nameof(PessoaAtuacaoBeneficioDynamicModel.ListaTipoResponsavelFinanceiro), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public TipoResponsavelFinanceiro TipoResponsavelFinanceiro { get; set; }

        [SMCConditionalReadonly(nameof(TipoResponsavelFinanceiro), SMCConditionalOperation.Equals, TipoResponsavelFinanceiro.ConvenioParceiro, PersistentValue = true)]
        [SMCCurrency(AllowZero = true)]
        [SMCDependency(nameof(TipoResponsavelFinanceiro), nameof(PessoaAtuacaoBeneficioController.BuscarValorPercentual), "PessoaAtuacaoBeneficio", true)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMaxValue(100)]
        [SMCMinValue(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]         
        public decimal? ValorPercentual { get; set; }

        [SMCIgnoreProp]
        public string RazaoSocial { get; set; }
    }
}