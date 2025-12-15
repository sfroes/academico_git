using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TipoServicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool ExigeEscalonamento { get; set; }

        public long SeqClasseTemplateProcessoSgf { get; set; }

        public IntegracaoFinanceira IntegracaoFinanceira { get; set; }

        public bool ObrigatorioIdentificarParcela { get; set; }
    }
}
