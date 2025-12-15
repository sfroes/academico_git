using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoItemCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }

        public string DescricaoEtapa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public DateTime DataInicioEscalonamento { get; set; }

        public DateTime DataFimEscalonamento { get; set; }

        public short? NumeroDivisaoParcelas { get; set; }

        public bool Ativo { get; set; }
    }
}