using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaEmissaoBoletoParcelaResponsavelMatriculaViewModel : SMCViewModelBase
    {
        public long SeqResponsavel { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string NomeResponsavel { get; set; }

        public long SeqTitulo { get; set; }

        public long SeqServico { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public SituacaoTitulo? SituacaoTitulo { get; set; }
                
        [SMCCurrency(true)]
        [SMCSize(SMCSize.Grid3_24)]
        public decimal? ValorTitulo { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoTitulo { get; set; }

    }
}