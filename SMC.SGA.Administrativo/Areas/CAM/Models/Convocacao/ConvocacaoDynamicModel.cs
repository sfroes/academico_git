using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IProcessoSeletivoService), nameof(IProcessoSeletivoService.BuscarProcessosSeletivosSelect))]
        public List<SMCDatasourceItem> ProcessosSeletivosDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICampanhaCicloLetivoService), nameof(ICampanhaCicloLetivoService.BuscarCampanhasCicloLetivoSelect))]
        public List<SMCDatasourceItem> CampanhasCicloLetivoDataSource { get; set; }

        #endregion [ DataSources ]

        #region [ SMCHiden ]

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #endregion [ SMCHiden ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSelect(nameof(ProcessosSeletivosDataSource), AutoSelectSingleItem = true)]
        public long SeqProcessoSeletivo { get; set; }

        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid15_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid5_24)]
        [SMCSelect(nameof(CampanhasCicloLetivoDataSource), AutoSelectSingleItem = true)]
        public long SeqCampanhaCicloLetivo { get; set; }

        [SMCMinValue(0)]
        [SMCMaxValue(100)]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid5_24)]
        public short QuantidadeChamadasRegulares { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCInclude(nameof(Chamadas))]
        public SMCMasterDetailList<ChamadaViewModel> Chamadas { get; set; }
              
        [SMCHidden]
        public List<long> SeqsGrupoEscalonamentoBanco { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Assert("MSG_Asset_Modificar_GrupoEscalonamento", x => (x as ConvocacaoDynamicModel).Chamadas.Any(w => w.SituacaoChamada == SituacaoChamada.AguardandoLiberacaoParaMatricula && !(x as ConvocacaoDynamicModel).SeqsGrupoEscalonamentoBanco.Contains(w.SeqGrupoEscalonamento)))
                   .Button("ConsultarCandidatos", "ConsultarCandidatos", "Campanha",
                           UC_CAM_001_03_01.CONSULTAR_CANDIDATO,
                           i => new
                           {
                               seqCampanha = SMCDESCrypto.EncryptNumberForURL(((ConvocacaoListarDynamicModel)i).SeqCampanha),
                               seqConvocacao = SMCDESCrypto.EncryptNumberForURL(((ConvocacaoListarDynamicModel)i).Seq)
                           })
                   .Detail<ConvocacaoListarDynamicModel>("_DetailList")
                   .DisableInitialListing(true)
                   .Javascript("Areas/CAM/Convocacao")
                   .Service<IConvocacaoService>(index: nameof(IConvocacaoService.ListarConvocacoes),
                                                 save: nameof(IConvocacaoService.SalvarConvocacao),
                                                 edit: nameof(IConvocacaoService.AlterarConvocacao))
                   .Tokens(tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                           tokenEdit: UC_CAM_001_05_02.MANTER_CONVOCACAO,
                           tokenRemove: UC_CAM_001_05_02.MANTER_CONVOCACAO,
                           tokenList: UC_CAM_001_05_01.PESQUISAR_CONVOCACAO);
        }

        #endregion [ Configuração ]
    }
}