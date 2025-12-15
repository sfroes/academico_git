using SMC.Financeiro.ServiceContract.BLT.Data;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class BoletoMatriculaVO : BoletoData
    {
        public long SeqSolicitacaoMatricula { get; set; }

        public long SeqIngressante { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoProcesso { get; set; }
    }
}