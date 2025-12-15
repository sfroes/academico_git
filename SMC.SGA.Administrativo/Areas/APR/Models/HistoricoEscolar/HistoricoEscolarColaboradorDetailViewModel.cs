using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "GrupoComponenteCurricular", Size = SMCSize.Grid24_24)]
    public class HistoricoEscolarColaboradorDetailViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [ColaboradorLookup]
        [SMCConditionalReadonly(nameof(HistoricoEscolarDynamicModel.SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRSC1")]
        [SMCConditionalReadonly(nameof(NomeColaboradorCondicional), SMCConditionalOperation.NotEqual, "", RuleName = "CRSC2")]
        [SMCConditionalRule("CRSC1 || CRSC2")]
        [SMCConditionalRequired(nameof(NomeColaboradorCondicional), "")]
        [SMCSize(SMCSize.Grid11_24)]
        [SMCUnique]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        /// <summary>
        ///  Criado para funcionar como intermediário entre seq e nome para fazer as condicionals. Caso faça condicional circular dá erro no javascript.
        /// </summary>
        [SMCHidden]
        [SMCDependency(nameof(NomeColaborador))]
        public string NomeColaboradorCondicional { get; set; }

        [SMCConditionalReadonly(nameof(HistoricoEscolarDynamicModel.SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRNC1")]
        [SMCConditionalReadonly(nameof(SeqColaborador), SMCConditionalOperation.NotEqual, "", RuleName = "CRNC2")]
        [SMCConditionalRule("CRNC1 || CRNC2")]
        [SMCConditionalRequired(nameof(SeqColaborador), "")]
        [SMCSize(SMCSize.Grid11_24)]
        [SMCUnique]
        public string NomeColaborador { get; set; }
    }
}