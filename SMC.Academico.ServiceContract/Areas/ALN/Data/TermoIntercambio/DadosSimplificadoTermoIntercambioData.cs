using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class DadosSimplificadoTermoIntercambioData : ISMCMappable
    {
        public string DescricaoTipoTermo { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public DateTime? DataInicioTipoTermo { get; set; }

        public DateTime? DataFimTipoTermo { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }
    }
}