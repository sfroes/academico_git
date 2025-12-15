using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DadosSimplificadoTermoIntercambioVO : ISMCMappable
    {
        public string DescricaoTipoTermo { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public DateTime? DataInicioTipoTermo { get; set; }

        public DateTime? DataFimTipoTermo { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }
    }
}
