using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource()]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoOfertaCursoService),
            nameof(IInstituicaoNivelTipoOfertaCursoService.BuscarInstituicaoNivelTipoOfertaCursoSelect),
            values: new string[] { nameof(SeqCurso) })]
        public List<SMCDatasourceItem> TiposOfertaCurso { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCParameter]
        public long SeqCurso { get; set; }

        [SMCKey]
        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid20_24)]
        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(SeqCurso))]
        [SMCDependency(nameof(ApenasAtivos))]
        [SMCInclude(ignore: true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        public FormacaoEspecificaLookupViewModel FormacaoEspecifica { get; set; }

        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }

        [SMCHidden]
        [SMCDependency(nameof(FormacaoEspecifica) + ".Seq")]
        public long? SeqFormacaoEspecifica { get { return FormacaoEspecifica?.Seq ?? null; } set { FormacaoEspecifica = new FormacaoEspecificaLookupViewModel() { Seq = value }; } }

        [SMCDependency(nameof(SeqFormacaoEspecifica), nameof(CursoOfertaController.RecuperarNomeCursoOferta), "CursoOferta", false, nameof(SeqCurso))]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoDocumentoConclusao { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCSelect(nameof(TiposOfertaCurso))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoOfertaCurso { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24)]
        public bool Ativo { get; set; }

        [SMCMapForceFromTo]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime? DataLiberacao { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "Curso")
                   .Header("CabecalhoCursoOferta")
                   .Service<ICursoOfertaService>(insert: nameof(ICursoOfertaService.VerificarDependenciasCursoOferta),
                                                 edit: nameof(ICursoOfertaService.BuscarCursoOferta),
                                                 save: nameof(ICursoOfertaService.SalvarCursoOferta))
                   .Tokens(tokenInsert: UC_CSO_001_01_03.MANTER_OFERTA_CURSO,
                           tokenEdit: UC_CSO_001_01_03.MANTER_OFERTA_CURSO,
                           tokenRemove: UC_CSO_001_01_03.MANTER_OFERTA_CURSO,
                           tokenList: UC_CSO_001_01_03.MANTER_OFERTA_CURSO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
            {
                this.DataLiberacao = DateTime.Now;
                this.Ativo = true;
            }
        }

        #endregion [ Configurações ]
    }
}