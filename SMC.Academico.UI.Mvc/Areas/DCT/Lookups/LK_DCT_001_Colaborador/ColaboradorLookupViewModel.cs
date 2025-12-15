using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class ColaboradorLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCSize(SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCSize(SMCSize.Grid5_24)]
        public string Nome { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public string NomeSocial { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public Sexo? Sexo { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataNascimento { get; set; }

        [SMCCpf]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string NumeroPassaporte { get; set; }
    }
}