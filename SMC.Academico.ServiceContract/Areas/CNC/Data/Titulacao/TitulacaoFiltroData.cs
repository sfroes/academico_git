using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TitulacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public CursoTipoFormacao? CursoTipoFormacao { get; set; }

        public Sexo? Sexo { get; set; }

        public bool? SeqCursoOuGrauAcademicoCurso { get; set; }

        public List<long> ListaGrauAcademico { get; set; }

        public long? SeqCursoFormacaoEspecifica { get; set; }
    }
}