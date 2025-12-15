using SMC.Academico.Common.Areas.ORT.Enums;
using System;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class PublicacaoBdpHistoricoSituacaoVO
    {
        public virtual long Seq { get; set; }

        public virtual long SeqPublicacaoBdp { get; set; }

        public virtual SituacaoTrabalhoAcademico SituacaoTrabalhoAcademico { get; set; }

        public long? SeqNotificacaoEmailDestinatario { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}