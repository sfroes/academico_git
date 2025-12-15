using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class InstituicaoNivelBeneficioData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqBeneficio { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public bool ObrigatorioAdesaoContrato { get; set; }

        public bool ObrigatorioCondicaoPagamento { get; set; }

        public short? NumeroParcelasPadraoCondicaoPagamento { get; set; }

        public TipoDeducao TipoDeducao { get; set; }

        public FormaDeducao FormaDeducao { get; set; }

        public decimal? ValorDeducao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public bool IncluirDesbloqueioTemporario { get; set; }
    }
}
