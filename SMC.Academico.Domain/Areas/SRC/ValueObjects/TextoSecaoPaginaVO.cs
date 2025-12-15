using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TextoSecaoPaginaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapaPagina { get; set; }

        public string DescricaoPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }

        public string DescricaoSecao { get; set; }

        public string TokenSecao { get; set; }

        public string Texto { get; set; }
        
        public bool CamposReadyOnly { get; set; }
    }
}
