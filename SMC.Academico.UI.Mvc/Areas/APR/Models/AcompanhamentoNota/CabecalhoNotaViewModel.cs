using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class CabecalhoNotaViewModel : SMCViewModelBase, ISMCMappable
    {
        public string nomeInstituicao { get; set; }
        public string ImagemCabecalho { get; set; }
        public string titulo { get; set; }
    }
}