using SMC.Financeiro.ServiceContract.BLT.Data;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoEmitirBoletoVO : BoletoData
    {
        public string RegistroAcademico { get; set; }

        public string DescricaoCurso { get; set; }
    }
}
