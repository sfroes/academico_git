using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoDivisaoComponenteData : ISMCMappable
    {
        public virtual long Seq { get; set; }
            
        public virtual long SeqDivisaoComponente { get; set; }

		public long? SeqOrigemAvaliacao { get; set; }

        public string DescricaoDivisaoComponente { get; set; }
    }
}
