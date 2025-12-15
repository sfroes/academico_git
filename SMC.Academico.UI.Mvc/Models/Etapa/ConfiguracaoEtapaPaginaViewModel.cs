using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Models
{
    public class ConfiguracaoEtapaPaginaViewModel
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqPaginaEtapaSgf { get; set; }

        public string TokenPagina { get; set; }

        public short Ordem { get; set; }

        public string TituloPagina { get; set; }

        public bool ExibeMenu { get; set; }

        public List<ArquivoSecaoPaginaViewModel> Arquivos { get; set; }

        public List<TextoSecaoPaginaViewModel> TextosSecao { get; set; }
    }
}