using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class ConfiguracaoAvaliacaoPpaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string Descricao { get; set; }

        public TipoAvaliacaoPpa TipoAvaliacaoPpa { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public DateTime? DataLimiteRespostas { get; set; }

        public int? CodigoAvaliacaoPpa { get; set; }

        public string DescricaoAvaliacaoPpa { get; set; }

        public int? CodigoOrigemPpa { get; set; }

        public string DescricaoOrigemPpa { get; set; }

        public int? SeqTipoInstrumentoPpa { get; set; }
        public string DescricaoTipoInstrumentoPpa { get; set; }

        public int? CodigoInstrumentoPpa { get; set; }

        public int? CodigoAplicacaoQuestionarioSgq { get; set; }

        public int? SeqEspecieAvaliadorPpa { get; set; }

        public bool? CargaRealizada { get; set; }

        public string ParteFixaNomeAvaliacao { get; set; }

        public ConfiguracaoAvaliacaoPpaTurmaFiltroVO FiltroConfiguracaoAvaliacaoPpaTurma { get; set; }
    }
}
