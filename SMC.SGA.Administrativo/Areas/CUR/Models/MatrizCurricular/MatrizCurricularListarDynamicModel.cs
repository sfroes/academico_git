using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqCurriculoCursoOferta { get; set; }

        public string Codigo { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        [SMCIgnoreProp]
        public string DescricaoFormatada
        {
            get
            {
                return string.IsNullOrEmpty(DescricaoComplementar) ? $"{Descricao}" : $"{Descricao} - {DescricaoComplementar}";
            }
        }

        public List<MatrizCurricularGridOfertaViewModel> Ofertas { get; set; }

        [SMCHidden]
        public bool ContemOfertaAtiva { get; set; }
    }
}