using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects 
{
    public class PosicaoConsolidadaEtapaVO : ISMCMappable
    {
        public string Etapa { get; set; }

        public int NaoIniciada { get; set; }

        public int EmAndamento { get; set; }

        public int FinalizadaComSucesso { get; set; }

        public int FinalizadaSemSucesso { get; set; }

        public int Cancelada { get; set; }

        public int Total { get; set; }
    }
}
