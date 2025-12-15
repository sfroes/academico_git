using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class EventoLetivoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public bool ExibirNivelEnsino { get; set; }

        public bool ExibirLocalidade { get; set; }

        public bool ExibirEntidadeResponsavel { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqTipoEvento { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long SeqCicloLetivo { get; set; }

        public TipoAluno TipoAluno { get; set; }

        public List<EventoLetivoNivelEnsinoData> NiveisEnsino { get; set; }

        public List<EventoLetivoLocalidadeData> Localidades { get; set; }

        public List<EventoLetivoEntidadeResponsavelData> EntidadesResponsaveis { get; set; }
    }
}