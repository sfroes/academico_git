using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class RelatorioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public TipoRelatorio TipoRelatorio { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public string Nome { get; set; }

        public long? SeqsEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqSituacaoMatricula { get; set; }

        /// <summary>
        /// Lista de RAs dos alunos
        /// </summary>
        public List<long> NumerosRegistrosAcademicos { get; set; }

        /// <summary>
        /// Array de sequencias de pessoa atuacao
        /// </summary>
        public long[] Seqs { get; set; }
    }
}