using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteOrientacaoPessoaAtuacaoDetailViewModel : SMCViewModelBase
    {
        #region [ Datasource ]

        [SMCDataSource(SMCStorageType.None)]
        [SMCHidden]
        public List<SMCSelectListItem> Colaboradores { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCHidden]
        public List<SMCSelectListItem> InstituicoesExternas { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCHidden]
        public List<SMCSelectListItem> TiposParticipacaoOrientacao { get; set; }

        #endregion [ Datasource ]

        [SMCIgnoreProp]
        public bool Ativo { get => true; }

        [SMCHidden]
        public long Seq { get; set; }

        //[SMCDependency(nameof(IngressanteOfertaDetailViewModel.SeqCampanhaOferta), nameof(IngressanteController.BuscarOfertasMatrizCurricular), "Ingressante", false)]
        [SMCDependency("Ofertas_0__SeqCampanhaOferta", nameof(IngressanteController.BuscarColaboradores), "Ingressante", true)]
        [SMCRequired]
        [SMCSelect(nameof(Colaboradores), UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        [SMCUnique]
        public long SeqColaborador { get; set; }

        [SMCDependency(nameof(IngressanteDynamicModel.SeqTipoOrientacao), nameof(IngressanteController.BuscarTiposParticipacao), "Ingressante", true, includedProperties: new[] { nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCRequired]
        [SMCSelect(nameof(TiposParticipacaoOrientacao), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCDependency(nameof(SeqColaborador), nameof(IngressanteController.BuscarInstituicoesExternasColaborador), "Ingressante", true)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicoesExternas), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        public long SeqInstituicaoExterna { get; set; }
    }
}