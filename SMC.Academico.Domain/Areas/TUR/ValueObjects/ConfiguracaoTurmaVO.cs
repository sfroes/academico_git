using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class ConfiguracaoTurmaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public short? QuantidadeVagas { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }


        public long SeqOrigemAvaliacao { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public long? SeqCriterioAprovacao { get; set; }
        public long? SeqEscalaApuracao { get; set; }
        public short? NotaMaxima { get; set; }
        public short? PercentualNotaAprovado { get; set; }
        public short? PercentualFrequenciaAprovado { get; set; }
        public string DescricaoEscalaApuracao { get; set; }
        public bool ApuracaoFrequencia { get; set; }
        public bool ApuracaoNota { get; set; }
        public  TipoEscalaApuracao? TipoEscalaApuracao { get; set; }
        public long SeqConfiguracaoComponente { get; set; }
        public long? SeqMatrizCurricularOferta { get; set; }
        public bool? PermiteAvaliacaoParcial { get; set; }
        public bool? OcorreuAlteracaoManual { get; set; }
        public bool Principal { get; set; }
        public List<ConfiguracaoTurmaDivisaoComponenteVO> DivisaoComponente { get; set; }

        public List<SMCDatasourceItem> ListaCriterioAprovacao { get; set; }
        public List<SMCDatasourceItem> ListaEscalaApuracao { get; set; }
    }
}
