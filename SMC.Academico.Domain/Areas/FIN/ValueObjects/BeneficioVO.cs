using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class BeneficioVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoBeneficio { get; set; }

        public string Descricao { get; set; }

        public int? SeqBeneficioFinanceiro { get; set; }

        public TipoResponsavelFinanceiro? TipoResponsavelFinanceiro { get; set; }

        public AssociarResponsavelFinanceiro AssociarResponsavelFinanceiro { get; set; }

        public List<PessoaJuridicaVO> ResponsaveisFinanceiros { get; set; }

        public bool DeducaoValorParcelaTitular { get; set; }

        public bool IncluirDesbloqueioTemporario { get; set; }

        public bool RecebeCobranca { get; set; }

        public string JustificativaNaoRecebeCobranca { get; set; }

        public bool ExigeCodigoPessoaAssociado { get; set; }

        public bool ExigeIdentificacaoParentesco { get; set; }

        public bool ExigeControleConcessaoBolsa { get; set; }

        public bool BeneficioIntercambio { get; set; }

        public bool ConcessaoAteFinalCurso { get; set; }
    }   
}
