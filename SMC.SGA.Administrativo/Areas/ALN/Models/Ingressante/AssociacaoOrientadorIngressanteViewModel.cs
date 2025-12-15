using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AssociacaoOrientadorIngressanteViewModel : SMCViewModelBase
    {
        #region DataSources

        public List<SMCDatasourceItem> TiposOrientacao { get; set; }

        public List<SMCDatasourceItem> Colaboradores { get; set; }

        public List<SMCDatasourceItem> TiposParticipacaoOrientacao { get; set; }

        #endregion DataSources

        [SMCHidden]
        [SMCKey]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long? SeqTipoIntercambio { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCSelect(nameof(TiposOrientacao), AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqTipoOrientacao { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<AssociacaoOrientadorIngressanteItemViewModel> Orientacoes { get; set; }
    }
}