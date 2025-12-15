using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DispensaHistoricoVigenciaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDispensa { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }
    }
}
