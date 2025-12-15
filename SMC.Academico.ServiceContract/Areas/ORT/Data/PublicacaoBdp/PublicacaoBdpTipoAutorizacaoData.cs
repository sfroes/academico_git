using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpTipoAutorizacaoData : ISMCMappable
    {
        public long Seq { get; set; }
        
        public string TipoDescricao { get; set; }
    }
}
