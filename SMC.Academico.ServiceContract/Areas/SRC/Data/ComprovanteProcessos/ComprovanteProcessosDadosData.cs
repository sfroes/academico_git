using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ComprovanteProcessosDadosData : ISMCMappable
    {
        public string Aluno { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string LocalidadeUnidadeResponsavel { get; set; }

        public string Vinculo { get; set; }

        public string VinculoInstitucional { get; set; }

        public string Titulo { get; set; }

        public string TotalCreditos { get; set; }

        public string NumeroRegistroAcademico { get; set; }

        public string CodigoPessoaCAD { get; set; }

        public long TotalTurmas { get; set; }

        public long TotalAtividades { get; set; }
    }
}
