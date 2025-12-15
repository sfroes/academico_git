using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoCopiarListarItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string DescricaoEtapa { get; set; }

        public int QuantidadeParcelas { get; set; }

        public List<SMCDatasourceItem> Escalonamentos { get; set; }

        public List<string> DescricaoEscalonamentos => Escalonamentos?.Select(s => s.Descricao).ToList();

        public string DescricaoEscalonamento { get; set; }
    }
}