using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class DatasEventoLetivoData : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public short Ano { get; set; }

        public short Numero { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public TipoAluno? TipoAluno { get; set; }

        public long? SeqTipoEventoAgd { get; set; }
    }
}
