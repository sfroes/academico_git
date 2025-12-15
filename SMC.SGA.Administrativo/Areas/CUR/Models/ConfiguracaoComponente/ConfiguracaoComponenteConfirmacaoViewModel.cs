using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Views.ConfiguracaoComponente.App_LocalResources;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteConfirmacaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCIgnoreProp]
        public IEnumerable<ConfiguracaoComponenteDivisaoViewModel> DivisoesExcluidas { get; set; }

        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string Mensagem
        {
            get
            {
                if (DivisoesExcluidas.SMCCount() == 1)
                {
                    return string.Format(UIResource.MensagemConfirmacao_ExclusaoDivisao, DivisoesExcluidas.Single().Numero);
                }
                else if (DivisoesExcluidas.SMCCount() > 1)
                {
                    var numeros = DivisoesExcluidas
                        .OrderBy(o => o.Numero)
                        .Select(s => s.Numero.GetValueOrDefault());

                    var numerosArg0 = string.Join(", ", numeros.Take(numeros.Count() - 1));
                    return string.Format(UIResource.MensagemConfirmacao_ExclusaoDivisoes, numerosArg0, numeros.Last());
                }
                else
                {
                    return null;
                }
            }
        }
    }
}