using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using System;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoRematriculaVO : ISMCMappable
    {

        #region [Processo]

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public long SeqCicloLetivoProcesso { get; set; }

        #endregion

        #region [Serviço]

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        public long SeqServico { get; set; }

        public string TokenServico { get; set; }

        #endregion

        #region [Aluno]

        public long SeqPessoaAtuacao { get; set; }

        public string NomeAluno { get; set; }

        public long SeqEntidadeVinculo { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqEntidadeCurso { get; set; }

        public long SeqEntidadeInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long? SeqMatrizCurriculaOferta { get; set; }

        public bool CriaBloqueioImpedimentoPrazo { get; set; }

        #endregion
    }
}