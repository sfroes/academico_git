using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class ConfiguracaoNumeracaoTrabalhoOfertaListaData : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }
    }
}
