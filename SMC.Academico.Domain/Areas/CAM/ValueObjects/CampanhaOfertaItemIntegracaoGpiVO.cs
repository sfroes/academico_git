using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaOfertaItemIntegracaoGpiVO : ISMCMappable
    {

        public long Seq { get; set; }
        public string TokenTipoOferta { get; set; }
        public long? SeqTurma { get; set; }
        public long? SeqColaborador { get; set; }
        public long? SeqCursoOfertaLocalidadeTurno { get; set; }
        public long? SeqFormacaoEspecifica { get; set; }
        public string Curso { get; set; }
        public string CursoOferta { get; set; }
        public string Localidade { get; set; }
        public string Turno { get; set; }
        public string AreaConcentracao { get; set; }
        public string LinhaPesquisa { get; set; }
        public string EixoTematico { get; set; }
        public string AreaTematica { get; set; }
        public string Orientador { get; set; }
    }
}
