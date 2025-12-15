using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AvaliacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [Datasource]

        public List<SMCDatasourceItem<TipoAvaliacao>> TiposAvaliacoes { get; set; }

        #endregion

        [SMCHidden]
        [SMCParameter]
        [SMCFilterKey]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCFilter(true,true)]
        [SMCSelect(nameof(TiposAvaliacoes))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoAvaliacao? TipoAvaliacao { get; set; }

        [SMCFilter(true,true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public string Sigla { get; set; }

        [SMCFilter(true,true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid16_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        [SMCHidden]
        public bool DiarioFechado { get;  set; }

    }
}