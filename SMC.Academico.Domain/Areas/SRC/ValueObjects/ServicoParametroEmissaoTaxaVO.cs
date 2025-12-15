using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoParametroEmissaoTaxaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public TipoEmissaoTaxa TipoEmissaoTaxa { get; set; }

        public short? NumeroDiasPrazoVencimentoTitulo { get; set; }

        public int? SeqMensagemTitulo { get; set; }

        public short? CodigoBancoEmissaoTitulo { get; set; }

        public string CodigoAgenciaEmissaoTitulo { get; set; }

        public string CodigoContaEmissaoTitulo { get; set; }

        public string CodigoCarteiraEmissaoTitulo { get; set; }
    }
}
