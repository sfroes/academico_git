using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class PublicacaoBdpHistoricoSituacaoViewModel : SMCViewModelBase
    {
        public virtual long Seq { get; set; }

        public virtual long SeqPublicacaoBdp { get; set; }

        public virtual SituacaoTrabalhoAcademico SituacaoTrabalhoAcademico { get; set; }

        public long? SeqNotificacaoEmailDestinatario { get; set; }
    }
}