using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class TextoSecaoPaginaData : ISMCMappable
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