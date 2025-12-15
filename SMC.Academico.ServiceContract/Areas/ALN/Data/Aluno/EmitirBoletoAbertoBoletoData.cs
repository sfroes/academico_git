using SMC.Framework.Mapper;
using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EmitirBoletoAbertoBoletoData : ISMCMappable
    {
        public string NomePessoa { get; set; }

        public SituacaoTitulo SituacaoTitulo { get; set; }

        public int SeqTitulo { get; set; }

        public int SeqServico { get; set; }
    }
}
