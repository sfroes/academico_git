using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class RelatorioLogAtualizacaoColaboradorFiltroData : ISMCMappable
    {
        public DateTime? DataInicioReferencia { get; set; }

        public DateTime? DataFimReferencia { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqColaborador { get; set; }
    }
}
