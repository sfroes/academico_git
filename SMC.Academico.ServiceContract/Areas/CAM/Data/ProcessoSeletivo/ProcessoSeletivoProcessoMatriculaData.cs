using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoProcessoMatriculaData : ISMCMappable
    {
        public List<SMCDatasourceItem> Processos { get; set; }

        public long Seq { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqProcesso { get; set; }
    }
}
