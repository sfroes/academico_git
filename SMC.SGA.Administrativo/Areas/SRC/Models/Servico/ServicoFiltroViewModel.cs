using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposServico { get; set; }

        #endregion

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]        
        [SMCFilter(true, true)]
        public long? Seq { get; set; }

        [SMCSelect(nameof(TiposServico))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid6_24)]        
        [SMCFilter(true, true)]
        public long? SeqTipoServico { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCFilter(true, true)]      
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid11_24)]
        [SMCFilter(true, true)]        
        public string Descricao { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true, "TipoServico.Descricao", SMCSortDirection.Ascending)]
        public string DescTipoServico { get; set; }

        #endregion
    }
}