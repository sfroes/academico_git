using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqParceriaIntercambio { get; set; }

        public long? SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public bool? Ativo { get; set; }

        public string DescricaoParceria { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public TipoMobilidade? TipoMobilidade { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public long? SeqParceriaIntercambioTipoTermo { get; set; }
    }
}