using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EntregaDocumentoDigitalFormacaoPaginaVO : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public string DescricaoFormacaoEspefica { get; set; }
    }
}
