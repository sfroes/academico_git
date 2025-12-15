using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCOrder(0)]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24)]
        public string Codigo { get; set; }

        [SMCOrder(2)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCConditionalReadonly(nameof(AtivoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? Ativo { get; set; }

        [SMCHidden]
        [SMCDescription]
        public string CodigoOuDescricao { get; set; }

        [SMCHidden]
        public List<long> SeqsMatrizCurricular { get; set; }

        [SMCHidden]
        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        [SMCHidden]
        public bool AtivoLeitura { get; set; }

        [SMCHidden]
        public bool? ConfiguracaoComponenteAtivo { get; set; }
    }
}