using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DispensaHistoricoVigenciaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDispensa { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }
    }
}
