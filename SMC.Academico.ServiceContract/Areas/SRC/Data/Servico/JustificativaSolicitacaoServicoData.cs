using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class JustificativaSolicitacaoServicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }
    }
}
