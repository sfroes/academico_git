using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoComponenteCurricularService), nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect),
            values: new string[] { nameof(SeqInstituicaoNivelResponsavel) })]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoComponenteCurricularService), nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarEntidadesPorTipoComponenteSelect),
            values: new string[] { nameof(SeqInstituicaoNivelResponsavel), nameof(SeqTipoComponenteCurricular) })]
        public List<SMCDatasourceItem> EntidadesResponsavel { get; set; }

        #endregion [ DataSource ]

        [SMCFilter]
        [SMCOrder(1)]
        [SMCSelect(nameof(InstituicaoNiveis))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public long? SeqInstituicaoNivelResponsavel { get; set; }

        [SMCHidden]
        public bool filtro { get { return true; } }

        [SMCDependency(nameof(SeqInstituicaoNivelResponsavel), nameof(ComponenteCurricularController.BuscarTipoComponenteCurricularSelect), "ComponenteCurricular", true, nameof(filtro))]
        [SMCFilter]
        [SMCOrder(2)]
        [SMCSelect(nameof(TiposComponenteCurricular), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid10_24)]
        public long? SeqTipoComponenteCurricular { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        public string Codigo { get; set; }

        [SMCFilter]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid16_24)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        public string Sigla { get; set; }

        [SMCFilter]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public int? CodigoComponenteLegado { get; set; }

        [SMCFilter]
        [SMCMaxLength(10)]
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public string BancoLegado { get; set; }

        [SMCFilter]
        [SMCOrder(9)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public TipoOrganizacao? TipoOrganizacao { get; set; }

        [SMCDependency(nameof(SeqInstituicaoNivelResponsavel), nameof(ComponenteCurricularController.BuscarEntidadesPorTipoComponenteSelect), "ComponenteCurricular", true)]
        [SMCDependency(nameof(SeqTipoComponenteCurricular), nameof(ComponenteCurricularController.BuscarEntidadesPorTipoComponenteSelect), "ComponenteCurricular", true)]
        [SMCFilter]
        [SMCOrder(10)]
        [SMCSelect(nameof(EntidadesResponsavel))]
        [SMCSize(SMCSize.Grid19_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public long? SeqEntidade { get; set; }
    }
}