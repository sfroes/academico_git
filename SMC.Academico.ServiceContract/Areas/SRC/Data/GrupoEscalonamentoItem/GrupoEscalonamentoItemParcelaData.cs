using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoItemParcelaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoEscalonamentoItem { get; set; }

        public short? NumeroParcela { get; set; }

        public string Descricao { get; set; }

        public DateTime DataVencimentoParcela { get; set; }

        public decimal ValorPercentualParcela { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public bool? ServicoAdicional { get; set; }

        public bool DesativarPercentualParcela { get; set; }
        public bool HouveAlteracaoParcela { get; set; }
    }
}