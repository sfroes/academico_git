using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaConsultarCandidatoListaData : ISMCMappable
    {
        public long SeqInscricao { get; set; }

        public string Candidato { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string Oferta { get; set; }

        public short? NumeroChamada { get; set; }

        public TipoChamada? TipoChamada { get; set; }

        public DateTime? DataCadastroIngressante { get; set; }

        public SituacaoIngressante? SituacaoIngressante { get; set; }

        public DateTime? DataSituacaoIngressante { get; set; }
    }
}