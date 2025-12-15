using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    /// <summary>
    /// Lista Configuração de Avaliação.
    /// </summary>
    public class ConfiguracaoAvaliacaoPpaListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public DateTime? DataLimiteRespostas { get; set; }

        public TipoAvaliacaoPpa TipoAvaliacaoPpa { get; set; }

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
    }
}
