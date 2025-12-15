using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework.Mapper;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    [DataContract(Namespace = NAMESPACES.MODEL, IsReference = true)]
    public class ConfiguracaoEtapaPaginaData : ISMCMappable
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

        public List<ArquivoSecaoPaginaData> Arquivos { get; set; }

        public List<TextoSecaoPaginaData> TextosSecao { get; set; }
    }
}