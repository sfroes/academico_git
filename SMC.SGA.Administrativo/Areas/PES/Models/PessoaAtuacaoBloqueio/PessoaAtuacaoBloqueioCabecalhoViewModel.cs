using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioCabecalhoViewModel : SMCViewModelBase
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

        public string DescricaoMotivoBloqueio { get; set; }

        public string DescricaoMotivoBloqueioFormatado
        {
            get
            {
                var retorno = string.Empty;

                if (!string.IsNullOrEmpty(DescricaoTipoBloqueio))
                    retorno = $"{DescricaoTipoBloqueio}";
                if (!string.IsNullOrEmpty(DescricaoMotivoBloqueio) && string.IsNullOrEmpty(retorno))
                    retorno += $"{DescricaoMotivoBloqueio}";
                else
                    retorno += $" - {DescricaoMotivoBloqueio}";

                return retorno;
            }
        }

        [SMCDescription]
        public string Descricao { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string ResponsavelBloqueio { get; set; }

        public string DescricaoTipoBloqueio { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public string DescricaoReferenciaAtuacao { get; set; }
    }
}