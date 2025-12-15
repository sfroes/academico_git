using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class HistoricoSituacaoMatrizCurricularOfertaDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IHistoricoSituacaoMatrizCurricularOfertaService), nameof(IHistoricoSituacaoMatrizCurricularOfertaService.SituacoesMatrizCurricularOferta),
            values: new string[] { nameof(SeqMatrizCurricularOferta) })]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCOrder(1)]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        [SMCOrder(2)]
        [SMCRequired]
        public long SeqMatrizCurricularOferta { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect("Situacoes")]
        [SMCSize(SMCSize.Grid12_24)]
        public SituacaoMatrizCurricularOferta SituacaoMatrizCurricularOferta { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string Descricao
        {
            get { return SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta); }
        }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(5)]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public bool Ultimo { get { return !this.DataFim.HasValue; } }

        [SMCHidden]
        public bool PrimeiroRegistro { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .HeaderIndex("CabecalhoMatrizCurricularOferta")
                   .Header("CabecalhoMatrizCurricularOferta")
                   .Service<IHistoricoSituacaoMatrizCurricularOfertaService>(delete: nameof(IHistoricoSituacaoMatrizCurricularOfertaService.ExcluirHistoricoSituacaoMatrizCurricularOferta),
                                                                              index: nameof(IHistoricoSituacaoMatrizCurricularOfertaService.BuscarHistoricosSituacoesMatrizCurricularOferta),
                                                                                save: nameof(IHistoricoSituacaoMatrizCurricularOfertaService.SalvarHistoricoSituacaoMatrizCurricularOferta))
                   .ButtonBackIndex("Index", "MatrizCurricular", model => new { SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as HistoricoSituacaoMatrizCurricularOfertaDynamicModel).SeqCurriculoCursoOferta) })
                   .IgnoreFilterGeneration()
                   .ConfigureButton((button, model, action) =>
                   {
                       if (action == SMCDynamicButtonAction.Remove)
                       {
                           var historico = (HistoricoSituacaoMatrizCurricularOfertaDynamicModel)model;
                           button.Hide(!historico.Ultimo);
                       }
                   })
                   .Tokens(tokenList: UC_CUR_001_05_03.MANTER_HISTORICO_SITUACAO_MATRIZ_CURRICULAR,
                           tokenInsert: UC_CUR_001_05_03.MANTER_HISTORICO_SITUACAO_MATRIZ_CURRICULAR,
                           tokenEdit: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                           tokenRemove: UC_CUR_001_05_03.MANTER_HISTORICO_SITUACAO_MATRIZ_CURRICULAR);
        }

        //public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        //{
            //navigationGroup = new HistoricoSituacaoMatrizCurricularOfertaNavigationGroup(this);
        //}

        #endregion [ Configurações ]
    }
}