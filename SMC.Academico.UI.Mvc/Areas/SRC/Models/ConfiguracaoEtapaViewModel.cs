using SMC.Academico.UI.Mvc.Models;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class ConfiguracaoEtapaViewModel
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public string OrientacaoEtapa { get; set; }

        public string DescricaoTermoEntregaDocumentacao { get; set; }

        public List<ConfiguracaoEtapaPaginaViewModel> ConfiguracoesPagina { get; set; }
    }
}