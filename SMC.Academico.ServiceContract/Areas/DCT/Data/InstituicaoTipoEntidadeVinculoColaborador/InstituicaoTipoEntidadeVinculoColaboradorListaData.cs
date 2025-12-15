using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoTipoEntidadeVinculoColaboradorListaData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        public string DescricaoInstituicaoTipoEntidade { get; set; }
              
        [SMCMapProperty("TipoVinculoColaborador.Descricao")]
        public string DescricaoTipoVinculoColaborador { get; set; }
    }
}
