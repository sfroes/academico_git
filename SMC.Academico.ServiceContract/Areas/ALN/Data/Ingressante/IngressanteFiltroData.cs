using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class IngressanteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public long? SeqPessoa { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public long? SeqCampanhaCicloLetivo { get; set; }

        public string NumeroPassaporte { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqNivelEnsino { get; set; }
    }
}