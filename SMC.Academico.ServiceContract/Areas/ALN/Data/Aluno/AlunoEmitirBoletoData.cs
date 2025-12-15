using SMC.Financeiro.ServiceContract.BLT.Data;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoEmitirBoletoData : BoletoData
    {        
        [DataMember]
        public string RegistroAcademico { get; set; }

        [DataMember]
        public string DescricaoCurso { get; set; }       
    }
}
