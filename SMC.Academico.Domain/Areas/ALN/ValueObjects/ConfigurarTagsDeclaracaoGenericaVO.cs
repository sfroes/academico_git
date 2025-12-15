using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ConfigurarTagsDeclaracaoGenericaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public bool? PermiteEditarDado { get; set; }

        public string InformacaoTag { get; set; }

        public TipoPreenchimentoTag TipoPreenchimentoTag { get; set; }

        public string DescricaoTag { get; set; }

        public string Valor { get; set; }
    }
}
