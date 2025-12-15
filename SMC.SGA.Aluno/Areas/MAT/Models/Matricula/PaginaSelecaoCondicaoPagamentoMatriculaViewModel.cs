using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaSelecaoCondicaoPagamentoMatriculaViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.SELECAO_CONDICAO_PGTO_MATRICULA;

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCCurrency(true)]
        public decimal ValorTotal { get; set; }

        public List<SMCDatasourceItem> CondicoesPagamento { get; set; }

        [SMCRadioButtonList(nameof(CondicoesPagamento))]
        [SMCOrientation(SMCOrientation.Vertical)]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid15_24,SMCSize.Grid19_24)]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(TermoAderido), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public int? SeqCondicaoPagamento { get; set; }

        [SMCHidden]
        public bool TermoAderido { get; set; }
    }
}