using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CabecalhoDocumentoConclusaoApostilamentoAlunoVO : ISMCMappable
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
