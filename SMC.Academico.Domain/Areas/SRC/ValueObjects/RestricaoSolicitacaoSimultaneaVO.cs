using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class RestricaoSolicitacaoSimultaneaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public long SeqServicoRestricao { get; set; }
    }
}
