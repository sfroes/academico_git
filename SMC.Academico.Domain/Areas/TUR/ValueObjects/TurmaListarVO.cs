using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string DescricaoTipoTurma { get; set; }

        public bool TurmaCompartilhada { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public long SeqCicloLetivoInicio { get; set; }

        public long? SeqCicloLetivoFim { get; set; }

        public DateTime DataInicioPeriodoLetivo { get; set; }

        public DateTime DataFimPeriodoLetivo { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }
        public long? SeqAgendaTurma { get; set; }

        public List<TurmaOfertaMatrizVO> TurmaOfertasMatriz { get; set; }

        public List<TurmaDivisoesVO> TurmaDivisoes { get; set; }

        public List<DivisaoTurmaVO> DivisoesTurma { get; set; }

        public List<TurmaCabecalhoConfiguracoesVO> TurmaConfiguracoesCabecalho { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string DescricaoCursoLocalidadeTurno
        {
            get
            {
                var descricao = DescricaoLocalidade ?? DescricaoLocalidadePrincipal;

                if (string.IsNullOrEmpty(DescricaoTurno))
                    return descricao;
                else
                    return $"{descricao} - {DescricaoTurno}";
            }
        }

        public long? SeqLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoLocalidadePrincipal { get; set; }

        public long? SeqTurno { get; set; }

        public string DescricaoTurno { get; set; }

        public long? SeqCurso { get; set; }

        public string DescricaoCurso { get; set; }

        public bool ConfirmarAlteracao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool DesativarExcluir { get; set; }

        public bool DesativarExcluirValidacaoEventoAula { get; set; }

        public string MensagemDesativarExcluir { get; set; }

        public bool AulaLancada { get; set; }

        public bool PossuiNotaLancada { get; set; }

        public bool EhOuPossuiDesdobramento { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set;}

        public bool ResponsavelTurma { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        public bool AgendaTurmaConfigurada { get; set; }

        public bool GradeConfigurada { get; set; }

        public RestricaoTurmaMatrizVO RestricaoTurmaMatrizOfertaPrincipal { get; set; }

        public bool PossuiDivisaoOrientacao { get; set; }
    }
}
