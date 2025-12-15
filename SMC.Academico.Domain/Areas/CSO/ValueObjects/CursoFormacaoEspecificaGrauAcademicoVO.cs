using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoFormacaoEspecificaGrauAcademicoVO : ISMCMappable
    {
        public long? SeqGrauAcademico { get; set; }
       
        public string DescricaoGrauAcademico { get; set; }

        public long SeqFormacaoEspecifica { get; set; }
    }
}
