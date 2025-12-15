using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class MensagemFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public long? SeqTipoMensagem { get; set; }

        public string TokenTipoMensagem { get; set; }

        public bool? MensagensValidas { get; set; }

        public TipoUsoMensagem? TipoUsoMensagem { get; set; }

        public CategoriaMensagem? CategoriaMensagem { get; set; }

    }
}