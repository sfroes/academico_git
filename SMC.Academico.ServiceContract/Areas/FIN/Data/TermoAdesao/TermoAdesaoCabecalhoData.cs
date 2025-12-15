using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoCabecalhoData : ISMCMappable
    {
        public long SeqContrato { get; set; }

        public string NumeroRegistro { get; set; }
         
        [SMCMapProperty("Descricao")]
        public string NomeContrato { get; set; }
        
        public List<TermoAdesaoListarNivelEnsinoData> NiveisEnsino { get; set; } 

        public List<TermoAdesaoListarCursoData> Cursos { get; set; } 
         
        public string DataInicioValidade { get; set; }
         
        public string DataFimValidade { get; set; }
    }
}
