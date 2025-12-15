using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitanteViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCDescription]
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
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string Telefone { get; set; }

        public string EnderecoEletronico { get; set; }

        public bool EAluno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public int? CodigoAlunoMigracao { get; set; }
    }
}