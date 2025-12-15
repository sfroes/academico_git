using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("Servico.SeqTipoServico")]
        public long SeqTipoServico { get; set; }

        [SMCMapProperty("Servico.Seq")]
        public long SeqServico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }        

        public bool ValidarSituacaoFutura { get; set; }

        public decimal? ValorPercentualServicoAdicional { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public List<long> SeqsTipoEntidade { get; set; }

        public List<EntidadeVO> EntidadesResponsaveis { get; set; }

        public List<EntidadeVO> EntidadesCompartilhadas { get; set; }

        public List<ProcessoUnidadeResponsavelDetalheVO> UnidadesResponsaveis { get; set; }

        public List<ProcessoEtapaSGFVO> EtapasSGF { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public long? SeqAgendamentoSat { get; set; }

        public SituacaoAgendamento? SituacaoAgendamento { get; set; }
    }
}