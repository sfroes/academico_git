using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoExternaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        public InstituicaoExternaFiltroDynamicModel()
        {
            this.Ativo = true;
        }

        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid5_24)]
        public string Nome { get; set; }

        [SMCOrder(2)]
        [SMCFilter(true, true)]
        [SMCSelect("Paises", "Codigo", "Nome", StorageType = SMCStorageType.Cache)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public int? CodigoPais { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ILocalidadeService), "BuscarPaisesValidosCorreios")]
        [SMCDataSource("Paises", StorageType = SMCStorageType.Cache)]
        public List<SMCDatasourceItem> Paises { get; set; }

        [SMCOrder(3)]
        [SMCFilter(true, true)]
        [SMCSelect("CategoriasInstituicaoEnsino", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqCategoriaInstituicaoEnsino { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        [SMCDataSource(dataSource: "CategoriaInstituicaoEnsino")]
        public List<SMCDatasourceItem> CategoriasInstituicaoEnsino { get; set; }

        [SMCOrder(4)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        [SMCOrder(5)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public bool? Ativo { get; set; }
    }
}