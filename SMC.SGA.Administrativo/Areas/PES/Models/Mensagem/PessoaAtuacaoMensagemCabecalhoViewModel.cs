using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoMensagemCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public string NomePessoa
        {
            get
            {
                var nomePessoa = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomePessoa += $"{NomeSocial} ({Nome})";
                    else
                        nomePessoa += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomePessoa += $"{Nome}";
                }
                return nomePessoa;
            }
        }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCHidden]
        public string DescricaoSituacaoMatricula { get; set; }
    }
}