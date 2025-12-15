using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoEtapaPaginaVO : ISMCMappable
    {       
        public long Seq { get; set; }
      
        public long SeqConfiguracaoEtapa { get; set; }
      
        public long SeqPaginaEtapaSgf { get; set; }

        public string DescricaoPagina { get; set; }

        public string TokenPagina { get; set; }

        public short Ordem { get; set; }

        public string TituloPagina { get; set; }

        public bool ExibeMenu { get; set; }

        public long? SeqFormulario { get; set; }

        public long? SeqVisaoFormulario { get; set; }

        public string DescricaoVisaoFormulario { get; set; }

        public long SeqEtapaSgf { get; set; }

        public bool ExibeFormulario { get; set; }

        public bool CamposReadyOnly { get; set; }

        public ConfiguracaoDocumento? ConfiguracaoDocumento { get; set; }
    }
}