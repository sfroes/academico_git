using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaFiltroData : ISMCMappable
    {
        public long? SeqPessoa { get; set; }

        public long? SeqUsuarioSAS { get; set; }
    }
}