using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoClassificacaoPorHierarquiaFiltroData : ISMCMappable
    {
        public long SeqHierarquiaClassificacao { get; set; }

        public bool Exclusivo { get; set; }

        public long? SeqClassificacaoSuperior { get; set; }
    }
}