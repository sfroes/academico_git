using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ClassificacaoPeriodicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short AnoInicio { get; set; }

        public short AnoFim { get; set; }

        public bool Atual { get; set; }
    }
}
