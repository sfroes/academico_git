using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class EventoLetivoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoEvento { get; set; }

        public string TipoEvento { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string CicloLetivo { get; set; }

        public List<string> NivelEnsino { get; set; }
    }
}