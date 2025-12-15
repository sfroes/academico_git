using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IProgramaService), nameof(IProgramaService.BuscarSituacoesPrograma), values: new string[] { nameof(ListarInativos) })]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCIgnoreProp]
        private bool ListarInativos { get; set; } = true;

        #endregion [ DataSources ]

        [SMCHidden]
        public long? SeqTipoEntidade { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid7_24)]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCOrder(2)]
        [SMCSelect("Situacoes")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long? SeqSituacaoAtual { get; set; }

        [SMCOrder(3)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoPrograma? TipoPrograma { get; set; }
    }
}