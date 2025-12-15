using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]
        public List<SMCDatasourceItem> EntidadesProcessos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoServicoService), nameof(ITipoServicoService.BuscarTiposServicosPorInstituicaoNivelEnsinoSelect))]
        public List<SMCDatasourceItem> TiposServico { get; set; } = new List<SMCDatasourceItem>();

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IServicoService), nameof(IServicoService.BuscarServicosPorTipoServicoSelect), values: new string[] { nameof(SeqTipoServico) })]
        public List<SMCDatasourceItem> Servicos { get; set; }

        #endregion DataSources

        #region Campos Auxiliares

        [SMCHidden]
        public TipoUnidadeResponsavel TipoUnidadeResponsavel { get { return TipoUnidadeResponsavel.EntidadeResponsavel; } }

        [SMCHidden]
        public bool UsarNomeReduzido { get { return true; } }

        #endregion

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        [SMCSelect(nameof(EntidadesProcessos), AutoSelectSingleItem = true)]
        [SMCFilter(true, true)]
        public long? SeqUnidadeResponsavel { get; set; }

        [SMCOrder(2)]
        [SMCSelect(nameof(TiposServico), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCFilter(true, true)]
        public long? SeqTipoServico { get; set; }

        [SMCOrder(3)]
        [SMCSelect(nameof(Servicos), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqTipoServico), nameof(ProcessoController.BuscarServicosPorTipoServicoSelect), "Processo", true)]
        [SMCFilter(true, true)]
        public long? SeqServico { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid14_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(5)]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid5_24)]
        [SMCFilter(true, true)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCOrder(6)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        [SMCFilter(true, true)]
        public bool? ListarProcessosEncerrados { get; set; }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Filter && !ListarProcessosEncerrados.HasValue)
            {
                ListarProcessosEncerrados = false;
            }
        }
    }
}