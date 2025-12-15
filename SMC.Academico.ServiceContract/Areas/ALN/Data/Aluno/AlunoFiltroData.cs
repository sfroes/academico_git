using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    [DataContract]
    public class AlunoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [DataMember]
        public long? NumeroRegistroAcademico { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public long? SeqSituacaoMatricula { get; set; }

        [DataMember]
        public List<long> SeqsSituacaoMatriculaCicloLetivo { get; set; }

        [DataMember]
        public List<long> SeqEntidadesResponsaveis { get; set; }

        [DataMember]
        public long? SeqLocalidade { get; set; }

        [DataMember]
        public long? SeqNivelEnsino { get; set; }

        [DataMember]
        public long? SeqCursoOferta { get; set; }

        [DataMember]
        public long? SeqFormacaoEspecifica { get; set; }

        [DataMember]
        public long? SeqTurno { get; set; }

        [DataMember]
        public long? SeqTipoVinculoAluno { get; set; }

		[SMCKeyModel]
        [DataMember]
        public long[] Seqs { get; set; }

        [DataMember]
        public long? SeqPessoa { get; set; }

        [DataMember]
        public string Cpf { get; set; }

        [DataMember]
        public string NumeroPassaporte { get; set; }

        [DataMember]
        public bool? VinculoAlunoAtivo { get; set; }

        [DataMember]
        public long? SeqFormaIngresso { get; set; }

        [DataMember]
        public long? SeqCicloLetivoIngresso { get; set; }

        [DataMember]
        public long? SeqCicloLetivoSituacaoMatricula { get; set; }

        [DataMember]
        public long? SeqTipoTermoIntercambio { get; set; }

		[DataMember]
		public bool? AlunoDI { get; set; }

        [DataMember]
        public long? SeqInstituicaoEnsino { get; set; }

        [DataMember]
        public long? SeqUsuarioSAS { get; set; }

        [DataMember]
        public int? CodigoAlunoMigracao { get; set; }
    }
}