using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TotalizadorSolicitacaoServicoVO : ISMCMappable
    {
        public int Novas { get; set; }

        public int EmAndamento { get; set; }

        public int Concluidas { get; set; }

        public int Encerradas { get; set; }
    }
}