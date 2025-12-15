using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CabecalhoConfiguracaoProcessoViewModel : SMCViewModelBase
    {        
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoCicloLetivo { get; set; }       

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCValueEmpty("-")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }        
    }
}