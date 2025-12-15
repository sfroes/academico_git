using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline
{
    public class HistoricoSituacaoEntregaOnlineData : ISMCMappable
    {
        #region Cabecalho

        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public List<string> Alunos { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        #endregion Cabecalho

        public long SeqEntregaOnline { get; set; }
        public List<HistoricoSituacaoEntregaOnlineItemData> Situacoes { get; set; }
    }
}