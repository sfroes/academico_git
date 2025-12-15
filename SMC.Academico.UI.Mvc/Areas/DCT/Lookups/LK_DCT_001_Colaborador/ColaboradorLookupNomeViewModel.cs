using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class ColaboradorLookupNomeViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }
        
        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCDescription]
        public string NomeFormatado
        {
            get
            {
                var nomeFormatado = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeFormatado += $"{NomeSocial} ({Nome})";
                    else
                        nomeFormatado += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeFormatado += $"{Nome}";
                    else
                        return null;
                }

                return nomeFormatado;
            }
        }
    }
}
