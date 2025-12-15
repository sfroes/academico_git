using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AssociacaoIngressanteLoteCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long? SeqIngressante { get; set; }

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

        public bool Falecido { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCValueEmpty("-")]
        public long? CodigoMigracaoAluno { get; set; }

        public long? RA { get; set; }
    }
}