using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource("TipoVinculoAluno")]
        [SMCServiceReference(typeof(IALNDynamicService))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFormaIngressoService), nameof(IFormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect))]
        public List<SMCDatasourceItem> FormasIngresso { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICampanhaService), nameof(ICampanhaService.BuscarCampanhasSelect))]
        public List<SMCDatasourceItem> Campanhas { get; set; }

        #endregion [ DataSources ]

        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid9_24)]
        public long? SeqEntidadeResponsavel { get; set; }

        [SMCDependency(nameof(SeqCicloLetivo), nameof(CampanhaController.BuscarCampanhas), "Campanha", "CAM", false)]
        [SMCDependency(nameof(SeqEntidadeResponsavel), nameof(CampanhaController.BuscarCampanhas), "Campanha", "CAM", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Campanhas), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public long? SeqCampanha { get; set; }

        [SMCDependency(nameof(SeqCampanha), nameof(IngressanteController.BuscarProcessosSeletivos), "Ingressante", "ALN", true)]
        [SMCFilter(true, true)]
        [SMCSelect(autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqProcessoSeletivo { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCDependency(nameof(SeqProcessoSeletivo), nameof(IngressanteController.BuscarTiposVinculoAluno), "Ingressante", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposVinculoAluno), "Seq", "Descricao", SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqTipoVinculoAluno { get; set; }

        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.BuscarFormasIngresso), "Ingressante", true, new[] { nameof(SeqProcessoSeletivo) })]
        [SMCFilter(true, true)]
        [SMCSelect(autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqFormaIngresso { get; set; }

        [CampanhaOfertaLookup]
        [SMCDependency(nameof(SeqCampanha))]
        [SMCDependency(nameof(SeqCicloLetivo))]
        [SMCDependency(nameof(SeqEntidadeResponsavel))]
        [SMCDependency(nameof(SeqProcessoSeletivo))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public CampanhaOfertaLookupViewModel SeqCampanhaOferta { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCMapProperty("Nome")]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid12_24)]
        public string NomeFiltro { get; set; }

        [SMCCpf]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string Cpf { get; set; }

        [SMCFilter(true, true)]
        [SMCMaxLength(250)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public string NumeroPassaporte { get; set; }
    }
}