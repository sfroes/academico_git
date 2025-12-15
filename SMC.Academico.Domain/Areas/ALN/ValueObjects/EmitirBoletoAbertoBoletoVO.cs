using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class EmitirBoletoAbertoBoletoVO
    {
        public string NomePessoa { get; set; }

        public SituacaoTitulo SituacaoTitulo { get; set; }

        public int SeqTitulo { get; set; }

        public int SeqServico { get; set; }
    }
}
