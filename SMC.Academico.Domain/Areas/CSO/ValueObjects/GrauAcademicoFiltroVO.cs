using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class GrauAcademicoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqCurso { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public bool? Ativo { get; set; }

        public bool? GrauAcademicoAtivo { get; set; }

        /// <summary>
        /// Retorna todos indiferente dos filtros
        /// </summary>
        public bool RetornarTodos { get; set; }
    }
}