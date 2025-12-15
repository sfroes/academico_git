using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }
               
        public string DescricaoTipoTurma { get; set; }

        public bool TurmaCompartilhada { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public long SeqCicloLetivoInicio { get; set; }

        public long SeqCicloLetivoFim { get; set; }

        public DateTime DataInicioPeriodoLetivo { get; set; }

        public DateTime DataFimPeriodoLetivo { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }

        public List<DivisaoTurmaData> DivisoesTurma { get; set; }

        public List<TurmaOfertaMatrizData> TurmaOfertasMatriz { get; set; }

        public List<TurmaDivisoesData> TurmaDivisoes { get; set; }

        public List<TurmaCabecalhoConfiguracoesData> TurmaConfiguracoesCabecalho { get; set; }

        public long SeqLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public long SeqTurno { get; set; }

        public string DescricaoTurno { get; set; }

        public long SeqCurso { get; set; }

        public string DescricaoCurso { get; set; }

        public string DescricaoCursoLocalidadeTurno { get; set; }

        public bool ConfirmarAlteracao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool DesativarExcluir { get; set; }

        public bool DesativarExcluirValidacaoEventoAula { get; set; }

        public string MensagemDesativarExcluir { get; set; }

        public bool AulaLancada { get; set; }

        public bool PossuiNotaLancada { get; set; }

        public bool EhOuPossuiDesdobramento { get; set; }
        
        public long SeqOrigemAvaliacao { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        public bool ResponsavelTurma { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        public bool AgendaTurmaConfigurada { get; set; }

        public bool GradeConfigurada { get; set; }

        public bool PossuiDivisaoOrientacao { get; set; }
    }
}
