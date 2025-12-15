using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class InstituicaoNivelTipoTermoIntercambioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }

        public bool ConcedeFormacao { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public bool PermiteIngresso { get; set; }

        public bool PermiteSaidaIntercambio { get; set; }

        public long SeqNivelEnsino { get; set; }
    }
}