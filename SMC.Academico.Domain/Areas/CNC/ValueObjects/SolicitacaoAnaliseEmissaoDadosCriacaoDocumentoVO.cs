using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO : ISMCMappable
    {
        public bool? EmissaoDiploma { get; set; }

        public bool? EmissaoHistorico { get; set; }

        public bool? EmissaoDiplomaHistorico { get; set; }

        public long? SeqTipoDocumentoAcademico { get; set; }

        public long? SeqGrupoRegistro { get; set; }

        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        public long? SeqOrgaoRegistro { get; set; }

        public long? SeqConfiguracaoGad { get; set; }

        public long? SeqConfiguracaoHistoricoGAD { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string UsuarioRegistro { get; set; }

        public int? NumeroPublicacaoDOU { get; set; }

        public int? NumeroSecaoDOU { get; set; }

        public int? NumeroPaginaDOU { get; set; }

        public DateTime? DataPublicacaoDOU { get; set; }

        public DateTime? DataEnvioPublicacaoDOU { get; set; }

        public string IdentificadorNumeroRegistroAtualizacaoHistorico { get; set; }
    }
}