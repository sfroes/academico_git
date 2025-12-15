using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioPessoaData : ISMCMappable
    { 
        public long Seq { get; set; }
         
        public long SeqTermoIntercambioTipoMobilidade { get; set; }
         
        public string Cpf { get; set; }
         
        public string Passaporte { get; set; }
    }
}
