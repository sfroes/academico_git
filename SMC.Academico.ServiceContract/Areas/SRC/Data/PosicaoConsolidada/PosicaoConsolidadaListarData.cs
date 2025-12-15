using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class PosicaoConsolidadaListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string GrupoEscalonamento { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public string DescricaoProcesso { get; set; }

        public List<PosicaoConsolidadaEtapaData> Etapas { get; set; }
    }
}