using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class SelecaoOfertaCampanhaLookupFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanha { get; set; }

        public long? Seq { get; set; }

        /// <summary>
        /// Tipo Object necessário para implementação do lookup receber seq duplo, 
        /// da linha selecionada
        /// </summary>
        [SMCKeyModel]
        public object[] Seqs { get; set; }

        public long SeqTipoOferta { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqOrientador { get; set; }

        public string Turma { get; set; }
    }
}
