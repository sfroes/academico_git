using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool Ativo { get; set; }

        [SMCDescription]
        public string DescricaoCompleta
        {
            get
            {
                return string.IsNullOrEmpty(DescricaoComplementar) ?
                    $"{Codigo} - {Descricao}" :
                    $"{Codigo} - {Descricao} - {DescricaoComplementar}";
            }
        }

        public List<CurriculoCursoOfertaListarViewModel> CursosOferta { get; set; }
    }
}