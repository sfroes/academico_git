using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ArquivoVO : ISMCMappable
    {
        public System.Guid Codigo { get; set; }

        public string SiglaAplicacao { get; set; }

        public string Diretorio { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }

        public int Tamanho { get; set; }

        public string Descricao { get; set; }

        public System.DateTime DataUltimoDownload { get; set; }

        public int QuantidadeDownloads { get; set; }

        public byte[] Conteudo { get; set; }
    }
}
