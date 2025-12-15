using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaOfertaVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public string CursoOferta { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public string AreaConcentracao { get; set; }

        public string LinhaPesquisa { get; set; }

        public string EixoTematico { get; set; }

        public string AreaTematica { get; set; }

        public long? SeqOrientador { get; set; }

        public string Orientador { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqTipoOferta { get; set; }

        public string TipoOferta { get; set; }

        public string TipoOfertaToken { get; set; }

        public string Turma { get; set; }

        public long? SeqTurma { get; set; }

        public string Vagas { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }
    }
}
