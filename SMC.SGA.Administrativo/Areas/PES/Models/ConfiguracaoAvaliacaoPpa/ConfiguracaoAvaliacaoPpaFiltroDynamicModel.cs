using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        /// <summary>
        /// NV01 Listar as entidades responsáveis que na hierarquia organizacional são as superiores as entidades do tipo "Programa" e 
        /// de acordo com as regras.
        /// RN_USG_001 - Filtro por Instituição de Ensino  
        /// RN_USG_005 - Filtro por Entidade
        /// </summary>
        /// 
        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(EntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        [SMCOrder(0)]
        [SMCFilter(true)]
        [SMCSize(SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCOrder(2)]
        [SMCFilter(true)]
        [SMCSize(SMCSize.Grid5_24)]
        [CicloLetivoLookup]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCOrder(3)]
        [SMCFilter(true)]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCOrder(4)]
        [SMCFilter(true)]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true,true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24)]
        public List<TipoAvaliacaoPpa> ListaTipoAvaliacaoPpa { get; set; }

        [SMCOrder(5)]
        [SMCFilter(true)]
        [SMCSize(SMCSize.Grid5_24)]
        public int? CodigoAvaliacaoPpa { get; set; }
    }
}