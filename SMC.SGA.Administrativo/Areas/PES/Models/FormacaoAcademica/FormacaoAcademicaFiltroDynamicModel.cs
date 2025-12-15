using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FormacaoAcademicaFiltroDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        //[SMCKey]
        //[SMCOrder(0)]
        //[SMCReadOnly]
        //[SMCRequired]
        //[SMCSize(SMCSize.Grid2_24)]
        //public long SeqTitulacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        //[SMCSize(SMCSize.Grid11_24)]
        //public string Nome { get; set; }
        //[SMCSize(SMCSize.Grid11_24)]
        //public string NomeSocial { get; set; }
        //[SMCSize(SMCSize.Grid8_24)]
        //public string NomeCompleto
        //{
        //    get
        //    {
        //        var nomeCompleto = string.Empty;

        //        if (!string.IsNullOrEmpty(NomeSocial))
        //        {
        //            if (!string.IsNullOrEmpty(Nome))
        //                nomeCompleto += $"{NomeSocial} ({Nome})";
        //            else
        //                nomeCompleto += $"{NomeSocial}";
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(Nome))
        //                nomeCompleto += $"{Nome}";
        //        }
        //        return nomeCompleto;
        //    }
        //}

        //[SMCCpf]
        //[SMCValueEmpty("-")]
        //[SMCSize(SMCSize.Grid8_24)]
        //public string Cpf { get; set; }

        //[SMCValueEmpty("-")]
        //[SMCSize(SMCSize.Grid8_24)]
        //public string NumeroPassaporte { get; set; }

        //[SMCIgnoreProp]
        //public string CpfOuPassaporte => !string.IsNullOrEmpty(Cpf) ? SMCMask.ApplyMaskCPF(Cpf) : NumeroPassaporte;
    }
}