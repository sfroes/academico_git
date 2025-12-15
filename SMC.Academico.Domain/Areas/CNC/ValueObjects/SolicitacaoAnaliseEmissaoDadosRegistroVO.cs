using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDadosRegistroVO : ISMCMappable
    {
        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string UsuarioRegistro { get; set; }

        public int? NumeroPublicacaoDOU { get; set; }

        public int? NumeroSecaoDOU { get; set; }

        public int? NumeroPaginaDOU { get; set; }

        public DateTime? DataPublicacaoDOU { get; set; }

        public DateTime? DataEnvioPublicacaoDOU { get; set; }
    }
}