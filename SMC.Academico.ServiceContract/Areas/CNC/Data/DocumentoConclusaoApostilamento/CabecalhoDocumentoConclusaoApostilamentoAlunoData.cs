using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class CabecalhoDocumentoConclusaoApostilamentoAlunoData : ISMCMappable
    {
        public long NumeroRegistroAcademico { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string NomeCompleto { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }
    }
}
