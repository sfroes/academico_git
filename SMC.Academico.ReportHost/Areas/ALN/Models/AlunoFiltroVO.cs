using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class AlunoFiltroVO : ISMCMappable
    {
        public SMCPageSetting PageSettings { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public long? SeqSituacaoMatricula { get; set; }

        public List<long> SeqEntidadesResponsaveis { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqPessoa { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool? VinculoAlunoAtivo { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public bool? AlunoDI { get; set; }
    }
}