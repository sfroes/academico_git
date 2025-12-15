using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaInstrucoesFinaisMatriculaViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.INSTRUCOES_FINAIS_MATRICULA;

        public override string ChaveTextoBotaoProximo => "Botao_Concluirprocesso";

        public bool BoletoZerado { get; set; }

        public bool BoletoComValor { get; set; }

        [SMCCurrency(true)]
        [SMCSize(SMCSize.Grid24_24)]
        public decimal ValorTotalMatricula { get; set; }

        public List<PaginaEmissaoBoletoParcelaMatriculaViewModel> Parcelas { get; set; }

        public long SeqArquivoContrato { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataAdesao { get; set; }

        public Guid? CodigoAdesao { get; set; }

        public string TermoAdesao { get; set; }

        public bool BeneficioIncluiDesbloqueioTemporario { get; set; }

        public bool BeneficioDeferidoCobranca { get; set; }

        public string BackUrl { get; set; }
    }
}