using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string NomeEntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCSize(SMCSize.Grid18_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public TipoAvaliacaoPpa TipoAvaliacaoPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public DateTime DataFimVigencia { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public DateTime DataLimiteRespostas { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoDescricaoAvaliacaoPpa { get { return string.Join(" - ", CodigoAvaliacaoPpa, DescricaoAvaliacaoPpa); } }
        public int? CodigoAvaliacaoPpa { get; set; }
        public string DescricaoAvaliacaoPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoDescricaoOrigemAvaliacaoPpa { get { return string.Join(" - ", CodigoOrigemPpa, DescricaoOrigemPpa); } }
        public int? CodigoOrigemPpa { get; set; }
        public string DescricaoOrigemPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string CodigoDescricaoInstrumentoPpa { get { return string.Join(" - ", SeqTipoInstrumentoPpa, DescricaoTipoInstrumentoPpa); } }
        public string DescricaoTipoInstrumentoPpa { get; set; }
        public int? SeqTipoInstrumentoPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public int? CodigoInstrumentoPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public int? CodigoAplicacaoQuestionarioSgq { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public int? SeqEspecieAvaliadorPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public bool? CargaRealizada { get; set; }

        [SMCHidden]
        public ConfiguracaoAvaliacaoPpaTurmaFiltroData FiltroConfiguracaoAvaliacaoPpaTurma { get; set; }
    }
}