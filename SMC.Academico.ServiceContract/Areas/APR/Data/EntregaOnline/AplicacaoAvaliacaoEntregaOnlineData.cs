using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline
{
    public class AplicacaoAvaliacaoEntregaOnlineData : ISMCMappable
    {
        public long SeqAplicacaoAvaliacao { get; set; }

        #region Cabecalho

        public string DescricaoOrigemAvaliacao { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
        public short? QuantidadeMaximaPessoasGrupo { get; set; }
        public string Instrucao { get; set; }
        public long? SeqArquivoAnexadoInstrucao { get; set; }
        public Guid? UidArquivoAnexadoInstrucao { get; set; }
        public long SeqOrigemAvaliacao { get; set; }

        #endregion Cabecalho

        public List<EntregaOnlineData> EntregasOnline { get; set; }
    }
}