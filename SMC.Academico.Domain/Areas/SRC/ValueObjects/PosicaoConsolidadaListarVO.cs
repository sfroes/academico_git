using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class PosicaoConsolidadaListarVO : ISMCMappable
    {
        public PosicaoConsolidadaListarVO()
        {
            this.Etapas = new List<PosicaoConsolidadaEtapaVO>();
        }

        public long Seq { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public string GrupoEscalonamento { get; set; }

        public string DescricaoProcesso { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public List<PosicaoConsolidadaEtapaVO> Etapas { get; set; }
    }
}