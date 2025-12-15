using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ConvocacaoLiberacaoDeMatriculaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Ingressante { get; set; } 
    }
} 