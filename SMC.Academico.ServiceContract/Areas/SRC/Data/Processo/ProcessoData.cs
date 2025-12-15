using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqTipoServico { get; set; }

        public long SeqServico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public bool ValidarSituacaoFutura { get; set; }

        public decimal? ValorPercentualServicoAdicional { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public List<long> SeqsEntidadesCompartilhadas { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public List<EntidadeData> EntidadesResponsaveis { get; set; }

        public List<EntidadeData> EntidadesCompartilhadas { get; set; }

        public List<ProcessoUnidadeResponsavelDetalheData> UnidadesResponsaveis { get; set; }

        public List<ProcessoEtapaSGFData> EtapasSGF { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public long? SeqAgendamentoSat { get; set; }

        public SituacaoAgendamento? SituacaoAgendamento { get; set; }
    }
}