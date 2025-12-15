using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCHidden]        
        //[SMCServiceReference(typeof(IColaboradorService), nameof(IColaboradorService.BuscarColaboradoresOrientacaoSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqsAlunos) })]
        [SMCServiceReference(typeof(IColaboradorService), nameof(IColaboradorService.BuscarColaboradoresPorTurmaSelect), values: new[] { nameof(SeqTurma), nameof(TipoAtividadeColaborador) })]
        public List<SMCDatasourceItem> ListaColaboradores { get; set; }

        #endregion
        
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqDivisaoTurma { get; set; }

        [SMCHidden]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        public long[] SeqsAlunos { get; set; }

        [SMCHidden]
        public long? SeqOrientacao { get; set; }

        [SMCHidden]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCHidden]
        public TipoAtividadeColaborador TipoAtividadeColaborador { get; set; } = TipoAtividadeColaborador.Orientacao;

        [SMCHidden]
        [SMCParameter]
        public long SeqTurma { get; set; }

        [SMCIgnoreProp(SMCViewMode.Filter)]
        public string RaNome { get; set; }

        [SMCDetail]
        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<TurmaOrientacaoColaboradorMasterDetailsViewModel> Colaboradores { get; set; }

        [SMCIgnoreProp]
        public SituacaoVinculoOrietador SituacaoVinculoOrietador { get; set; }

        [SMCHidden]
        public bool ExisteHistoricoEscolarAluno { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Ajax()
                   .EditInModal(refreshIndexPageOnSubmit: true)
                   .Assert("MSG_Alerta_Historico_Escolar", x => (x as TurmaOrientacaoDynamicModel).ExisteHistoricoEscolarAluno)
                   .Detail<TurmaOrientacaoListarDynamicModel>("_DetailList")
                   .ModalSize(SMCModalWindowSize.Large)
                   .HeaderIndex("CabecalhoTurma")
                   .Header("CabecalhoTurma")
                   //.HeaderIndexList("CabecalhoListar")
                   .ViewPartialEdit("_EditTurmaOrientacao")
                   .ButtonBackIndex("index", "Turma")
                   .Service<IOrientacaoService>(index: nameof(IOrientacaoService.BuscarOrientacoesPorDivisaoTurma),
                                                edit: nameof(IOrientacaoService.BuscarOrientacaoPorDivisaoTurma),
                                                save: nameof(IOrientacaoService.SalvarOrientacaoTurma))
                   .Tokens(tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                              tokenEdit: UC_TUR_001_07_02.MANTER_ORIENTACAO_TURMA,
                            tokenRemove: UC_TUR_001_07_02.MANTER_ORIENTACAO_TURMA,
                             tokenList: UC_TUR_001_07_01.PESQUISAR_ORIENTACAO_TURMA);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if(viewMode == SMCViewMode.Edit)
            {
                this.Colaboradores.DefaultModel = new TurmaOrientacaoColaboradorMasterDetailsViewModel()
                {
                    TipoParticipacaoOrientacao = this.TipoParticipacaoOrientacao
                };
            }
        }

        #endregion [ Configurações ]
    }
}