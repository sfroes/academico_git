using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DispensaMatrizExcecaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDispensa { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public DateTime DataInicioExcecao { get; set; }

        public DateTime? DataFimExcecao { get; set; }
    }
}
