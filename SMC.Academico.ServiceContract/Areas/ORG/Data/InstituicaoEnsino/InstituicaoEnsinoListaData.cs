using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoEnsinoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("Descricao")] // Para popular o combo precisa dessa conversão!
        public string Nome { get; set; }

        public string Sigla { get; set; }

        public string NomeReduzido { get; set; }

        [SMCMapProperty("Mantenedora.Nome")]
        public string NomeMantenedora { get; set; }
    }
}