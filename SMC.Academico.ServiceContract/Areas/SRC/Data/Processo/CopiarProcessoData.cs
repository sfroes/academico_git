using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CopiarProcessoData : ISMCMappable
    {
        public string MensagemInformativa { get; set; }

        public CopiarProcessoOrigemData ProcessoOrigem { get; set; }

        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string TokenTipoServico { get; set; }
        public string TokenServico { get; set; }

        public decimal? ValorPercentualServicoAdicional { get; set; }

        public bool ExibirSecaoGrupoEscalonamento { get; set; }

        public List<CopiarProcessoDetalheEtapaData> EtapasCopiar { get; set; }

        public List<CopiarProcessoDetalheGrupoEscalonamentoData> GruposEscalonamentoCopiar { get; set; }      
    }
}
