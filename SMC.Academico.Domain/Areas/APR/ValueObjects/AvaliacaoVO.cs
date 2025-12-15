using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class AvaliacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public TipoAvaliacao TipoAvaliacao { get; set; }

        public string Descricao { get; set; }

        public string Instrucao { get; set; }

        public bool AvaliacaoGeral { get; set; }

        public bool ReplicaNota { get; set; }

        public short Valor { get; set; }

        public int TotalNotasLancadas { get; set; }

        public decimal? Media { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqArquivoAnexadoInstrucao { get; set; }

        public SMCUploadFile ArquivoAnexadoInstrucao { get; set; }

        public List<AplicacaoAvaliacaoVO> AplicacoesAvaliacao { get; set; }

        public int? CodigoUnidade { get; set; }

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