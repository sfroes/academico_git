using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SelecaoTurmaPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCSelectListItem> GruposPrograma { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SELECAO_TURMA_MATRICULA;
        
        [SMCHidden]
        [SMCMultiline]
        public string Justificativa { get; set; }

        [SMCConditionalReadonly(nameof(TotalRegistros), SMCConditionalOperation.NotEqual, "0", PersistentValue = true)]
        [SMCSelect(nameof(GruposPrograma))]
        public SMCEncryptedLong SeqGrupoPrograma { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqProcessoEtapa { get; set; }
              
        [SMCHidden]
        public List<TurmaMatriculaItemViewModel> TurmasMatriculaItem { get; set; }

        [SMCHidden]
        public int TotalRegistros { get { return TurmasMatriculaItem.SMCCount(); } }

        [SMCHidden]
        public bool ExibirPertencePlanoEstudo { get; set; }

        [SMCHidden]
        public bool? ExigirCurso { get; set; }

        [SMCHidden]
        public bool? ExigirMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? TodosCancelados { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }       
    }
}