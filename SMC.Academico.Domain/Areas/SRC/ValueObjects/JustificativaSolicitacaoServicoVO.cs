using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class JustificativaSolicitacaoServicoVO : ISMCMappable
    {       
        public long Seq { get; set; }
    
        public long SeqServico { get; set; }
     
        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }
    }
}
