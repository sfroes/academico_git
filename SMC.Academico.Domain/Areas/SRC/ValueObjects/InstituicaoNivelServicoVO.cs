using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class InstituicaoNivelServicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long SeqServico { get; set; }

        [SMCMapProperty("InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino")]
        public long SeqNivelEnsino { get; set; }

        [SMCMapProperty("InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno")]
        public long SeqTipoVinculoAluno { get; set; }

        [SMCMapProperty("InstituicaoNivelTipoVinculoAluno.TipoVinculoAluno.Descricao")]
        public string DescricaoTipoVinculoAluno { get; set; }

        public int? SeqTipoTransacaoFinanceira { get; set; }
    }
}
