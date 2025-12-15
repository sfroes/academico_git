using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoListarDynamicModel : SMCDynamicViewModel, ISMCStatefulView, ISMCSeq
    {
        #region [ Hidden ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        public long SeqNivelEnsino { get; set; }

        #endregion [ Hidden ] 

        [SMCReadOnly]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoComponenteCurricular { get; set; }

        [SMCOrder(1)]
        [SMCMaxLength(15)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCReadOnly]
        public string DescricaoSituacaoAprovacao { get { return SituacaoHistoricoEscolar?.SMCGetDescription(); } }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public List<AvaliacaoTrabalhoAcademicoAvaliacaoViewModel> Avaliacoes { get; set; }
    }
}