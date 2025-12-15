using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoSituacaoReabrirVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public long SeqSituacao { get; set; }
        
        public PermissaoServico PermissaoServico { get; set; }    
    }
}
