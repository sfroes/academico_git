using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class AvaliacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public TipoAvaliacao TipoAvaliacao { get; set; }

        public string Descricao { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public string DescricaoDivisaoFormatada { get; set; }

        public int? CodigoUnidade { get; set; }

        public string Instrucao { get; set; }

        public bool AvaliacaoGeral { get; set; }

        public bool ReplicaNota { get; set; }

        public short Valor { get; set; }

        public int TotalNotasLancadas { get; set; }

        public decimal? Media { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqArquivoAnexadoInstrucao { get; set; }

        public SMCUploadFile ArquivoAnexadoInstrucao { get; set; }

        public List<AplicacaoAvaliacaoData> AplicacoesAvaliacao { get; set; }

        #region ConsultaAvaliacao

        public SituacaoEntregaOnline Situacao { get; set; }

        public string Sigla { get; set; }

        public string Local { get; set; }

        public DateTime Data { get; set; }

        public int Nota { get; set; }

        public bool EntregaWeb { get; set; }

        #endregion ConsultaAvaliacao
    }
}