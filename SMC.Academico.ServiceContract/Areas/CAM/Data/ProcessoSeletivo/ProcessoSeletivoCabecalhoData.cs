using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoCabecalhoData : ISMCMappable
    {
        public long SeqCampanha { get; set; }

        public string Campanha { get; set; }

        public List<string> CiclosLetivos { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string TipoProcesso { get; set; }

        public List<string> NiveisEnsino { get; set; }

    }
}
