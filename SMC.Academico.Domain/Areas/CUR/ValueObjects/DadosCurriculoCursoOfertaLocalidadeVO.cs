using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DadosCurriculoCursoOfertaLocalidadeVO : ISMCMappable
    {
        public long SeqCursoOfertaLocalidade { get; set; }
        public string CodigoCurriculoMigracao { get; set; }
        public long SeqGrauAcademico { get; set; }
        public DateTime DataReferencia { get; set; }        
    }
}
