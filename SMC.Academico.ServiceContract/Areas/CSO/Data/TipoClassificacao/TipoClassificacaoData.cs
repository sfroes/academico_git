using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoClassificacaoData : ISMCMappable
    {

        public long Seq { get; set; }

        public long SeqTipoHierarquiaClassificacao { get; set; }

        public string Descricao { get; set; }
    }
}
