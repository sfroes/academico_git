using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaLookupViewModel : ISMCMappable, ISMCLookupData
    {
        [SMCKey]
        public long? Seq { get; set; }

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
                }

                return nomeFormatado;
            }
        }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }
    }
}