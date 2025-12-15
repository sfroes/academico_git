using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoRenovacaoMatriculaVO
    {
        public long SeqPessoaAtuacao { get; set; }

        public PessoaAtuacao PessoaAtuacao { get; set; }

        public bool PessoaAtuacaoAtiva { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public AlunoHistoricoCicloLetivo AlunoHistoricoCicloLetivo { get; set; }

        public DateTime? DataPrevisaoConclusao { get; set; }

        public long CodigoAlunoSGP { get; set; }

        public int AnoCicloLetivo { get; set; }

        public int NumeroCicloLetivo { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public IEnumerable<AlunoTurmaSGPData> Turmas { get; set; }

        public Pessoa Orientador { get; set; }
        
        public int? CodigoMigracaoOrientador { get; set; }

        public Pessoa Coorientador { get; set; }

        public int? CodigoMigracaoCoorientador { get; set; }

        public decimal? PercentualServicoAdicional { get; set; }

        public long? SeqConfiguracaoTipoNotificacao { get; set; }

        public long? SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        public List<string> Emails { get; set; }

        public string NomeSocialSolicitante { get; set; }

        public string NomeSolicitante { get; set; }

        public DateTime DataLimiteConclusao { get; set; }
        
        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public List<OrientacaoVO> OrientacoesPessoaAtuacao { get; set; }
    }
}
