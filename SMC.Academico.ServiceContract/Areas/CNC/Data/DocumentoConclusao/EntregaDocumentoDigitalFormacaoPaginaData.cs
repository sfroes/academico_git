using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class EntregaDocumentoDigitalFormacaoPaginaData : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public string DescricaoFormacaoEspefica { get; set; }
    }
}
