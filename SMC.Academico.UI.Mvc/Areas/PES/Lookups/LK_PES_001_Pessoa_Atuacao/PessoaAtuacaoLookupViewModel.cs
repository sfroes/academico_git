using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaAtuacaoLookupViewModel : ISMCMappable, ISMCLookupData
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public long? Seq { get; set; }

        [SMCHidden]
        [SMCDescription]
        public string NomeLista
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NomeFormatado))
                    return (TipoAtuacao.HasValue) ? NomeFormatado + " - " + TipoAtuacao.SMCGetDescription() : NomeFormatado;
                return null;
            }
        }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
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
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string Cpf { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string NumeroPassaporte { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string Descricao { get; set; }
    }
}