using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class InstrucoesIniciaisSolicitacaoPadraoPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> JustificativasSolicitacao { get; set; }

        #endregion DataSources

        [SMCHidden]
        public bool ExigeJustificativa { get; set; }

        [SMCSelect(nameof(JustificativasSolicitacao), AutoSelectSingleItem = true)]
        [SMCConditionalRequired(nameof(ExigeJustificativa), SMCConditionalOperation.Equals, true)]
        [SMCConditionalDisplay(nameof(ExigeJustificativa), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqJustificativa { get; set; }

        [SMCConditionalRequired(nameof(ExigeJustificativa), SMCConditionalOperation.Equals, true)]
        [SMCConditionalDisplay(nameof(ExigeJustificativa), SMCConditionalOperation.Equals, true)]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string ObservacoesJustificativa { get; set; }

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_INSTRUCOES_INICIAIS;
    }
}