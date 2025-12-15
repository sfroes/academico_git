using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaDivisoesData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long? SeqOrigemMaterial { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public bool PermitirGrupo { get; set; }

        public bool GerarOrientacao { get; set; }

        public string DivisaoTurmaRelatorioDescricao { get; set; }

        public bool CriterioAprovacaoFrequencia { get; set; }

        public bool TurmaDiarioAberto { get; set; }

        public bool TurmaCancelada { get; set; }

        public bool TurmaVigente { get; set; }

        public bool DivisaoTurmaPossuiConfiguracaoesGrade { get; set; }

        public List<TurmaDivisoesDetailData> DivisoesComponentes { get; set; }

        public List<TurmaDivisoesDetailData> DivisoesComponentesDisplay { get; set; }

        public short Numero { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public bool DiarioFechado { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }
        
        public bool? PermiteLancamentoFrequencia { get; set; }
    }
}