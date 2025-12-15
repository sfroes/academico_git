using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoIntercambioParticipanteData : ISMCMappable
    {
        public List<SMCDatasourceItem> Instituicoes { get; set; }

        public long? SeqColaborador { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

		public DateTime? DataInicio { get; set; }

		public DateTime? DataFim { get; set; }

		public long? SeqOrientacaoColaborador { get; set; }
	}
}