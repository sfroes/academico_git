using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class EventoLetivoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqTipoEvento { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long SeqCicloLetivo { get; set; }

        public TipoAluno TipoAluno { get; set; }

        public List<EventoLetivoNivelEnsinoVO> NiveisEnsino { get; set; }

        public List<EventoLetivoLocalidadeVO> Localidades { get; set; }

        public List<EventoLetivoEntidadeResponsavelVO> EntidadesResponsaveis { get; set; }
    }
}