using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ArquivoSecaoPaginaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapaPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }

        public string TokenSecao { get; set; }

        public int Ordem { get; set; }

        public string LinkArquivo { get; set; }

        public string MensagemArquivo { get; set; }

        public long SeqArquivoAnexado { get; set; }
        
        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}
