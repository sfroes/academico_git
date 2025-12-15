using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DeclaracaoGenericaAlunoVO : ISMCMappable
    {
        public int? CodigoAlunoMigracao { get; set; }
        public string NomeAluno { get; set; }
        public string NomeSocialAluno { get; set; }
        public string DescricaoCursoOfertaLocalidade { get; set; }

    }
}
