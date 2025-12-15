using SMC.Academico.Common.Constants;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        [SMCParameter]
        public DateTime? DataDepositoSecretaria { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        public List<AvaliacaoTrabalhoAcademicoListaViewModel> Componentes { get; set; }
    }
}