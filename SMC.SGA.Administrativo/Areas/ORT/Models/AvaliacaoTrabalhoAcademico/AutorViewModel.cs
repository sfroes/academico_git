using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AutorViewModel : SMCViewModelBase
    {
        public string Nome { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCurso { get; set; }
    }
}