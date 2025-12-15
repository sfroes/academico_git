using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Areas.APR.Models.EntregaOnline
{
    public class AplicacaoAvaliacaoEntregaOnlineViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCIgnoreProp]
        public bool BackLancamentoNota { get; set; }

        #region Cabecalho

        public string DescricaoOrigemAvaliacao { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
        public short? QuantidadeMaximaPessoasGrupo { get; set; }

        #endregion Cabecalho

        #region Filtros/Ordenação

        [SMCSelect]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid16_24, Framework.SMCSize.Grid8_24)]
        public OrdenacaoCorrecaoEntregaOnline OrdenacaoCorrecaoEntregaOnline { get; set; }

        #endregion Filtros/Ordenação

        public List<EntregaOnlineViewModel> EntregasOnline { get; set; }

        [SMCHidden]
        public bool PostIndex { get; set; }
    }
}