using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class OfertaCampanhaLookupListaData : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqCampanha { get; set; }

        public string TipoOferta { get; set; }

        public string TipoOfertaToken { get; set; }

        public string CursoOferta { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public string AreaConcentracao { get; set; }

        public string LinhaPesquisa { get; set; }

        public string EixoTematico { get; set; }

        public string Orientador { get; set; }

        /// <summary>
        /// RN_TUR_025 Exibição Descrição Turma
        /// </summary>
        public string Turma { get; set; }

        public string Vagas { get; set; }
    }
}
