using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.ValueObjects
{
    public class TelefoneVO : ISMCMappable
    {
        public long Seq { get; set; }

        public int CodigoPais { get; set; }

        public int CodigoArea { get; set; }

        public string Numero { get; set; }

        public TipoTelefone TipoTelefone { get; set; }
    }
}
