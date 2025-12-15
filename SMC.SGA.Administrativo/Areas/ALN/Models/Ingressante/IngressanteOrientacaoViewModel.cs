using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteOrientacaoViewModel : SMCViewModelBase
    {
        #region [ Datasource ]

        [SMCDataSource]
        [SMCHidden]
        public List<SMCSelectListItem> Colaboradores { get; set; }

        [SMCDataSource]
        [SMCHidden]
        public List<SMCSelectListItem> TiposParticipacaoOrientacao { get; set; }

        [SMCDataSource]
        [SMCHidden]
        public List<SMCSelectListItem> InstituicoesExternas { get; set; }

        #endregion [ Datasource ]

        [SMCHidden]
        public long Seq { get; set; }

        [SMCDependency(nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqTipoOrientacao), nameof(IngressanteController.BuscarColaboradores), "Ingressante", true, new[] { nameof(IngressanteDynamicModel.SeqEntidadeResponsavel), "Ofertas_0__SeqCampanhaOferta" })]
        [SMCHidden(SMCViewMode.ReadOnly)]
        [SMCRequired]
        [SMCSelect(nameof(Colaboradores), autoSelectSingleItem: true, UseCustomSelect = true, NameDescriptionField = nameof(NomeColaborador))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCUnique]
        public long SeqColaborador { get; set; }

        [SMCHidden]
        public string NomeColaborador { get; set; }

        [SMCDependency(nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqTipoOrientacao), nameof(IngressanteController.BuscarTiposParticipacao), "Ingressante", true, new[] { nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqTermoIntercambio), nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCHidden(SMCViewMode.ReadOnly)]
        [SMCRequired]
        [SMCSelect(nameof(TiposParticipacaoOrientacao), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoTipoParticipacaoOrientacao))]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCHidden]
        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        [SMCDependency(nameof(SeqColaborador), nameof(IngressanteController.BuscarInstituicoesExternasColaborador), "Ingressante", true, new[] { nameof(TipoParticipacaoOrientacao), nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqTermoIntercambio), nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqInstituicaoEnsinoExterna), nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(TipoParticipacaoOrientacao), nameof(IngressanteController.BuscarInstituicoesExternasColaborador), "Ingressante", true, new[] { nameof(SeqColaborador), nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqTermoIntercambio), nameof(PessoaAtuacaoTermoIntercambioViewModel.SeqInstituicaoEnsinoExterna), nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCHidden(SMCViewMode.ReadOnly)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicoesExternas), autoSelectSingleItem: true, NameDescriptionField = nameof(NomeInstituicaoExterna))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoExterna { get; set; }

        [SMCHidden]
        public string NomeInstituicaoExterna { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid10_24)]
        public string ColaboradorParticipacaoConfirmacao { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid14_24)]
        public string NomeInstituicaoExternaConfirmacao => NomeInstituicaoExterna;
    }
}