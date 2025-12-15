using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoTipoEntidadeFormacaoEspecifica
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaFiltroData : ISMCMappable
    {
        public long SeqTipoEntidade { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}
