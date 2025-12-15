using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoCabecalhoData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public int QuantidadeSolicitacoesProcesso { get; set; }

        public bool ExibirQuantidade { get; set; }
    }
}
