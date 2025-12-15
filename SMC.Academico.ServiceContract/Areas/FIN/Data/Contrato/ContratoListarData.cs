using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN
{
    public class ContratoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public string NumeroRegistro { get; set; }
         
        public string Descricao { get; set; }
         
        public DateTime DataInicioValidade { get; set; }
         
        public string DataFimValidade { get; set; }
        
        public List<ContratoCursosListarData> Cursos { get; set; }
         
        public List<ContratoNiveisEnsinoData> NiveisEnsino { get; set; }
    }
}
