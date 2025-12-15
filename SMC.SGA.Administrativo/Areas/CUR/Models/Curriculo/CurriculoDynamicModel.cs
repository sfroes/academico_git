using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    // Wizard0 Seleção de curso
    [SMCStepConfiguration(UseOnTabs = false)]
    // Wizard1 Tab0 Dados do currículo com partial
    [SMCStepConfiguration(ActionStep = "Step1", Partial = "_DadosCurriculo")]
    public class CurriculoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCSeq
    {
        #region [ DataSources ]

        [SMCDataSource(StorageType = SMCStorageType.Session)]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaService), nameof(ICursoOfertaService.BuscarCursoOfertasAtivasSelect),
            values: new string[] { nameof(SeqCursoParametro) })]
        public List<SMCDatasourceItem> CursosOfertaDataSource { get; set; }

        #endregion [ DataSources ]

        #region [ Hidden ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long NumeroSequencial { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool PermiteCreditoComponenteCurricular { get; set; }

        //FIX: O SMCServiceReference não recebe um Lookup como parâmetro
        [SMCHidden]
        [SMCParameter]
        [SMCStep(0)]
        public long SeqCursoParametro { get { return this.SeqCurso?.Seq ?? 0; } }

        #endregion [ Hidden ]

        #region [ Wizard0 ]

        [CursoLookup]
        [SMCHidden(SMCViewMode.Edit)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(0)]
        public CursoLookupViewModel SeqCurso { get; set; }

        #endregion [ Wizard0 ]

        #region [ HeaderWizard0 ]

        [SMCIgnoreProp]
        public string SiglaCurso { get; set; }

        [SMCIgnoreProp]
        public string NomeCurso { get; set; }

        [SMCIgnoreProp]
        public string DescricaoCurso { get { return $"{SeqCurso?.Seq.GetValueOrDefault().ToString("0000")} - {NomeCurso}"; } }

        #endregion [ HeaderWizard0 ]

        #region [ Aba0 Wizard1 ]

        [SMCCssClass("smc-breakline")]
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        [SMCOrder(1)]
        public override long Seq { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        [SMCOrder(2)]
        public string Codigo { get; set; }

        [SMCDescription]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        [SMCOrder(3)]
        public string Descricao { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        [SMCOrder(4)]
        public bool Ativo { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCStep(1, 0)]
        public string DescricaoComplementar { get; set; }

        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 1)]
        [SMCMapForceFromTo]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        [SMCOrder(6)]
        public SMCMasterDetailList<CurriculoCursoOfertaViewModel> CursosOferta { get; set; }

        /// <summary>
        /// Ofertas de curso sem o campo crédito.
        /// Criado para contornar a limitação do grid que não suporta conditional display
        /// </summary>
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 1)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<CurriculoCursoOfertaSemCreditoViewModel> CursoOfertaSemCredito
        {
            get
            {
                if (CursosOferta == null)
                    return null;
                var cursosOfetaSemCredito = new SMCMasterDetailList<CurriculoCursoOfertaSemCreditoViewModel>();
                cursosOfetaSemCredito.AddRange(CursosOferta.TransformList<CurriculoCursoOfertaSemCreditoViewModel>());
                return cursosOfetaSemCredito;
            }
        }

        #endregion [ Aba0 Wizard1 ]

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard(SMCDynamicWizardEditMode.Tab)
                   .Header("CabecalhoCurriculo") //FIX: Utilzar header partial após correção para wizard e tab
                   .Detail<CurriculoListarDynamicModel>("_DetailList")
                   .Button("CadastoGrupoCurricular", "Index", "GrupoCurricular",
                           UC_CUR_001_01_03.PESQUISAR_GRUPO_CURRICULAR,
                           i => new { seqCurriculo = SMCDESCrypto.EncryptNumberForURL((i as ISMCSeq).Seq) })
                   .Tokens(tokenInsert: UC_CUR_001_01_02.MANTER_CURRICULO,
                           tokenEdit: UC_CUR_001_01_02.MANTER_CURRICULO,
                           tokenRemove: UC_CUR_001_01_02.MANTER_CURRICULO,
                           tokenList: UC_CUR_001_01_01.PESQUISAR_CURRICULO)
                   .Service<ICurriculoService>(index: nameof(ICurriculoService.BuscarCurriculos),
                                               save: nameof(ICurriculoService.SalvarCurriculo),
                                               edit: nameof(ICurriculoService.BuscarCurriculo));

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqCurso"))
                options.ButtonBackIndex("Index", "Curso", x => new { area = "CSO" });
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
            if (viewMode == SMCViewMode.Insert)
            {
                this.Ativo = true;
            }
        }

        #endregion [ Configurações ]
    }
}