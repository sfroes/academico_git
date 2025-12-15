using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class CabecalhoDocumentoConclusaoApostilamentoAlunoViewModel : ISMCMappable
    {
        [SMCMapProperty("RA")]
        public long NumeroRegistroAcademico { get; set; }

        [SMCMapProperty("CodigoMigracaoAluno")]
        [SMCValueEmpty("-")]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        public string NomeCompleto
        {
            get
            {
                var nomeCompleto = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{NomeSocial} ({Nome})";
                    else
                        nomeCompleto += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{Nome}";
                }
                return nomeCompleto;
            }
        }

        [SMCCpf]
        [SMCValueEmpty("-")]
        public string Cpf { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroPassaporte { get; set; }
    }
}