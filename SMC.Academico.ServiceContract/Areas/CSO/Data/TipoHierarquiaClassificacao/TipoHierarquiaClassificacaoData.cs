using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoHierarquiaClassificacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
