using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CNC.Models.Titulacao;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TitulacaoDynamicModel : SMCDynamicViewModel
    {
        public TitulacaoDynamicModel()
        {
            Ativo = true;
        }

        #region DataSources
        
        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoDocumentoService), nameof(ITipoDocumentoService.BuscarTiposDocumentos))]
        public List<SMCDatasourceItem> Titulacoes { get; set; }

        [SMCDataSource]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrauAcademicoService), nameof(IGrauAcademicoService.BuscarGrauAcademicoAtivoSelect))]
        public List<SMCDatasourceItem> GrauAcademicoSelect { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid6_24)]
        public virtual string DescricaoFeminino { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid6_24)]
        public virtual string DescricaoMasculino { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid15_24, SMCSize.Grid3_24)]
        public virtual string DescricaoAbreviada { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid15_24, SMCSize.Grid6_24)]
        public string DescricaoXSD { get; set; }

        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCMapForceFromTo]
        public bool Ativo { get; set; }

        [CursoLookup]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid8_24)]
        [SMCOrder(6)]
        [SMCInclude("Curso")]
        public CursoLookupViewModel SeqCurso { get; set; }

        [SMCOrder(7)]
        [SMCSelect(nameof(GrauAcademicoSelect))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid8_24)]
        public long? SeqGrauAcademico { get; set; }

        [SMCHidden]
        public bool GrauAcademicoAtivo { get { return true; } }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(8)]
        public SMCMasterDetailList<TitulacaoDocumentoCompDynamicModel> DocumentosComprobatorios { get; set; }



        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CNC_002_04_01.MANTER_TITULACAO,
                              tokenInsert: UC_CNC_002_04_01.MANTER_TITULACAO,
                              tokenRemove: UC_CNC_002_04_01.MANTER_TITULACAO,
                              tokenList: UC_CNC_002_04_01.MANTER_TITULACAO)
                   .Service<ITitulacaoService>(
                              index: nameof(ITitulacaoService.BuscarTitulacoes),
                              save: nameof(ITitulacaoService.SalvarTitulacao),
                              edit: nameof(ITitulacaoService.BuscarTitulacao));
        }
    }
}