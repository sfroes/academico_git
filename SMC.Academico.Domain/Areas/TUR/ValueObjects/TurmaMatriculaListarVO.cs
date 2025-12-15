using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaMatriculaListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public short NumeroGrupo { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string TurmaFormatado { get { return $"{Codigo}.{Numero} - {DescricaoConfiguracaoComponenteTurma}"; } }

        public string DescricaoTipoTurma { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long SeqConfiguracaoComponentePrincipal { get; set; }

        public string DescricaoConfiguracaoComponenteTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public List<TurmaDivisoesVO> TurmaDivisoes { get; set; }

        public List<DivisaoTurmaVO> DivisoesTurma { get; set; }

        public List<TurmaMatriculaListarDetailVO> TurmaMatriculaDivisoes { get; set; }

        public bool Habilitar { get; set; }

        public short? Credito { get; set; }

        public TurmaDivisoesVO TurmaDivisoesItem { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        public long? SeqIngressante { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        public bool PreRequisito { get; set; }

        public bool ObrigatorioOrientador { get; set; }

        public List<long> SeqsConfiguracoesComponentes { get; set; }

        public List<long> SeqsTurmaCoRequisitos { get; set; }

        public string Situacao { get; set; }

        public string SituacaoEtapa { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public MotivoSituacaoMatricula? MotivoEtapa { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        public string TokenEtapa { get; set; }

        public string DescricaoLocalidadeAluno { get; set; }

        public string DescricaoLocalidadePrincipal { get; set; }

        public string DescricaoTurnoLocalidadeAluno { get; set; }

        public string DescricaoTurnoLocalidadePrincipal{ get; set; }

        public ComponenteCurricularVO ComponenteCurricular { get; set; }

        public long SeqTurma { get; set; }

        public string DescricaoCursoLocalidadeTurno
        {
            get
            {
                if (!string.IsNullOrEmpty(DescricaoLocalidadeAluno))
                    return $"{DescricaoLocalidadeAluno} - {DescricaoTurnoLocalidadeAluno}";
                else
                    return $"{DescricaoLocalidadePrincipal} - {DescricaoTurnoLocalidadePrincipal}";
            }
        }
    }
}