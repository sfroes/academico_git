using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCMaxLength(15)]
        public string DescricaoTipoTrabalho { get; set; }

        [SMCMaxLength(500)]
        public string Titulo { get; set; }

        public List<AutorViewModel> Autores { get; set; }

        public List<string> NomesAutores { get; set; }
    }
}