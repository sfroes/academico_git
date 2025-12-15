using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaHistoricoFechamentoDiarioData : ISMCMappable
    {
        public long? Seq { get; set; }

        public bool? DiarioFechado { get; set; }

        public long? SeqTurma { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataInclusao { get; set; }

        public string Usuario { get; set; }
    }
}
