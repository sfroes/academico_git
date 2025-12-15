using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaConsultarCandidatoListaVO : ISMCMappable
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

        //----------------FILTRO-------------
        public long? SeqChamada { get; set; }

        public List<long?> SeqsCiclosLetivos { get; set; }

        public long? SeqConvocacao { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }
    }
}