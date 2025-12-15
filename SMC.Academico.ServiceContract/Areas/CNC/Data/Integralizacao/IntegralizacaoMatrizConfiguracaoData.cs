using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoMatrizConfiguracaoData : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public SituacaoComponenteIntegralizacao SituacaoComponente { get; set; }

        public bool ExibirInformacao { get; set; }

        public List<long> SeqsHistoricosEscolar { get; set; }

        public long? SeqHistoricoEscolarUltimo { get; set; }

        public long? SeqPlanoEstudo { get; set; }

        public long? SeqPlanoEstudoAntigo { get; set; }

        public string SiglaComponente { get; set; }
    }
}
