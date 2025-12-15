using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class ComponenteCurricularSolicitacaoViewModel : SolicitacaoServicoPaginaViewModelBase
    {

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SELECAO_COMPONENTE_CURRICULAR;

        [SMCHidden]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> DivisoesComponentesSelect { get; set; }

        [SMCDetail(min: 1, max: 1, HideMasterDetailButtons = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public SMCMasterDetailList<DivisaoComponenteCurricularSolicitacaoViewModel> DivisoesComponente { get; set; }
    }
}