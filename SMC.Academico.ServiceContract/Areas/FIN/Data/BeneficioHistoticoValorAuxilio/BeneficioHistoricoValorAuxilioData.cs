using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class BeneficioHistoricoValorAuxilioData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelBeneficio { get; set; }

        public decimal? ValorAuxilio { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqBeneficio { get; set; }

        public bool FlagUltimoValorAuxilio { get; set; }
    }
}
