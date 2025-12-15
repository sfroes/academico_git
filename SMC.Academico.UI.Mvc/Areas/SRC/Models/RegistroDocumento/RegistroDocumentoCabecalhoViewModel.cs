using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class RegistroDocumentoCabecalhoViewModel : SMCViewModelBase
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }
        public string DescricaoProcesso { get; set; }

        public string Solicitante
        {
            get
            {
                var soliciante = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        soliciante += $"{NomeSocial} ({Nome})";
                    else
                        soliciante += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        soliciante += $"{Nome}";
                }
                return soliciante;
            }
        }
    }
}