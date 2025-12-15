using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoHistoricoSemMatrizVO : ISMCMappable
    {     
        public long SeqComponente { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string CodigoComponente { get; set; }

        public string DescricaoComponente { get; set; }

        public string DescricaoComponenteAssunto { get; set; }

        public string Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public SituacaoComponenteIntegralizacao SituacaoComponente { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        public long SeqHistoricoEscolar { get; set; }

        public bool ExibirInformacao { get; set; }

        public List<long> SeqsHistoricosEscolar { get; set; }

        public long? SeqHistoricoEscolarUltimo { get; set; }

        public long? SeqPlanoEstudo { get; set; }

        public long? SeqPlanoEstudoAntigo { get; set; }

        public string SiglaComponente { get; set; }
    }
}
