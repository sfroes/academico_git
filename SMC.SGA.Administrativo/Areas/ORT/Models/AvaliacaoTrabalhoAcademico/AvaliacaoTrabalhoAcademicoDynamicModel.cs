using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoDynamicModel : SMCDynamicViewModel, ISMCStatefulView, ISMCSeq
    {
        #region [ Hidden ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        public long? SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqNivelEnsino { get; set; }
                
        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }


        public TipoAvaliacao TipoAvaliacaoBanca { get => TipoAvaliacao.Banca; }

        [SMCHidden]
        public long? SeqCalendario { get; set; }

        [SMCHidden]
        public bool ExibirMotivoCancelamento { get => DataCancelamento.HasValue; }

        #endregion [ Hidden ]

        #region [ Data Source ]

        [SMCDataSource]
        public List<SMCSelectItem> TiposEvento { get; set; }


        #endregion [ Data Source ]

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(TiposEvento), AutoSelectSingleItem = true)]
        public long SeqTipoEvento { get; set; }


        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        public DateTime Data { get; set; }


        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        public DateTime Hora { get; set; }


        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        public string Local { get; set; }


        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public DateTime? DataCancelamento { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMotivoCancelamento), true)]
        [SMCOrder(5)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid6_24)]
        public string MotivoCancelamento { get; set; }

        [SMCDetail]
        [SMCOrder(6)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public SMCMasterDetailList<AvaliacaoTrabalhoAcademicoMembroBancaViewModel> MembrosBanca { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .IgnoreFilterGeneration()
                .Detail<AvaliacaoTrabalhoAcademicoListarDynamicModel>("_DetailList")
                .ButtonBackIndex("Index", "TrabalhoAcademico")
                   .HeaderIndex("CabecalhoAvaliacaoTrabalhoAcademico")
                   .Header("CabecalhoAvaliacaoTrabalhoAcademico")
                   .IgnoreFilterGeneration()
                .Tokens(tokenList: UC_ORT_002_02_03.PESQUISAR_AVALIACAO_TRABALHO_ACADEMICO,
                       tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION)
                .Service<IAplicacaoAvaliacaoService>(
                        index: nameof(IAplicacaoAvaliacaoService.BuscarAvaliacoesTrabalhoAcademico),
                        edit: nameof(IAplicacaoAvaliacaoService.BuscarAvaliacoesTrabalhoAcademicoBancaExaminadora),
                        insert: nameof(IAplicacaoAvaliacaoService.BuscarAvaliacoesTrabalhoAcademicoBancaExaminadoraInsert));

      

        }

        #endregion [ Configurações ]
    }
}