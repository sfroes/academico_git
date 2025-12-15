using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosConfirmacaoSolicitacaoPadraoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }
        public string DescricaoOriginal { get; set; }
    }
}