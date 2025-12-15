using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class DatasEventoLetivoVO : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public short Ano { get; set; }

        public short Numero { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public TipoAluno? TipoAluno { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }
    }
}