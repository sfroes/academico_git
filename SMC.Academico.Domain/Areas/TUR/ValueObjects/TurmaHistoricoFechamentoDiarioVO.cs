using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaHistoricoFechamentoDiarioVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public bool DiarioFechado { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataInclusao { get; set; }

        public string Usuario { get; set; }

    }
}
