using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TitulacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public CursoTipoFormacao? CursoTipoFormacao { get; set; }

        public bool? CursoOuGrauAcademicoCurso { get; set; }

        /// <summary>
        /// Flag para apresentar no select a descrição abreviada ou completa
        /// </summary>
        public bool? DescricaoAbrevida { get; set; }

        /// <summary>
        /// Quando informado retorna a descrição masculina ou feminina
        /// </summary>
        public Sexo? Sexo { get; set; }

        /// <summary>
        /// Quando setado considera as titulações vinculadas ao curso informado ou ao grau academico do curso informado
        /// </summary>
        public bool? SeqCursoOuGrauAcademicoCurso { get; set; }

        public List<long> ListaGrauAcademico { get; set; }

        public long? SeqCursoFormacaoEspecifica { get; set; }
    }
}
