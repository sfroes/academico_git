using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioPessoaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambioTipoMobilidade { get; set; }

        [SMCDependency(nameof(Cpf), nameof(TermoIntercambioController.CpfOuPassaportObrigatorio), "TermoIntercambio", false, new[] { nameof(Passaporte) })]
        [SMCDependency(nameof(Passaporte), nameof(TermoIntercambioController.CpfOuPassaportObrigatorio), "TermoIntercambio", false, new[] { nameof(Cpf) })]
        [SMCHidden]
        public bool CampoObrigatorio => string.IsNullOrEmpty(Cpf) && string.IsNullOrEmpty(Passaporte);

        [SMCConditionalRequired(nameof(CampoObrigatorio), true)]
        [SMCCpf]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Cpf { get; set; }

        [SMCConditionalRequired(nameof(CampoObrigatorio), true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Passaporte { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert | SMCViewMode.ReadOnly)]
        public string CPFouPassaporte
        {
            get
            {
                if (!string.IsNullOrEmpty(Cpf))
                {
                    try
                    {
                        return Convert.ToUInt64(Cpf).ToString(@"000\.000\.000\-00");
                    }
                    catch
                    {
                        return Cpf;
                    }
                }
                else
                {
                    return Passaporte;
                }
            }
        }
    }
}