using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models.EntregaOnline
{
    public class HistoricoSituacaoEntregaOnlineViewModel : SMCViewModelBase
    {
        #region Cabecalho

        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public List<string> Alunos { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        #endregion Cabecalho

        public long SeqEntregaOnline { get; set; }
        public List<HistoricoSituacaoEntregaOnlineItemViewModel> Situacoes { get; set; }
    }
}