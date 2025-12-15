using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class PessoaAtuacaoBeneficioResponsavelFinanceiroData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public virtual long SeqPessoaAtuacaoBeneficio { get; set; }

        public virtual long SeqPessoaJuridica { get; set; }

        public virtual decimal ValorPercentual { get; set; }

        public PessoaJuridicaData PessoaJuridica { get; set; }

        [SMCMapProperty("PessoaAtuacaoBeneficioResponsavelFinanceiro.PessoaJuridica.RazaoSocial")]
        public string NomePessoaJuridica { get; set; }

        public TipoResponsavelFinanceiro TipoResponsavelFinanceiro { get; set; }

        public string RazaoSocial { get; set; }

    }
}
