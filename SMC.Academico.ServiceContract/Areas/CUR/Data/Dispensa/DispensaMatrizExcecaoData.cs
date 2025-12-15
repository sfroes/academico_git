using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DispensaMatrizExcecaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDispensa { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public DateTime DataInicioExcecao { get; set; }

        public DateTime? DataFimExcecao { get; set; }
    }
}
