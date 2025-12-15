using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoUnidadeListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public long SeqTipoEntidade { get; set; }

        public string Nome { get; set; }

        public string NomeUnidade { get; set; }

        [SMCDescription]
        public string Titulo
        {
            get
            {
                string retorno = string.Empty;

                if (!string.IsNullOrEmpty(Nome))
                    retorno = $"{Seq:d4} - {Nome}";
                else
                    retorno = $"{Seq:d4}";

                //if (!string.IsNullOrEmpty(NomeUnidade))
                //    if (retorno.Length > 0)
                //        retorno += $" / {NomeUnidade}";
                //    else
                //        retorno = NomeUnidade;

                return retorno;
            }
        }

        public List<CursoOfertaLocalidadeListaViewModel> CursosOfertaLocalidade { get; set; }
    }
}