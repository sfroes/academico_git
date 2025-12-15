using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class AdesaoContratoBeneficiosViewModel : SMCViewModelBase
    {
        public string DescricaoBeneficio { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataFimVigencia { get; set; }

        public string TipoBeneficio { get; set; }

        public decimal? ValorBeneficio { get; set; }

        public FormaDeducao? FormaDeducao { get; set; }

        public string DescricaoValorBeneficio
        {
            get
            {
                string retorno = string.Empty;

                if (ValorBeneficio.HasValue)
                    retorno = ValorBeneficio.ToString();
                else
                    return "-";

                if (FormaDeducao == Academico.Common.Areas.FIN.Enums.FormaDeducao.PercentualBolsa)
                    retorno += "%";
                else
                    retorno = "R$" + retorno;

                return retorno;
            }
        }
    }
}