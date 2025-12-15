using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class PessoaAtuacaoBeneficioResponsavelFinanceiroVO : ISMCMappable
    {

        public long Seq { get; set; }

        public virtual long SeqPessoaAtuacaoBeneficio { get; set; }
        
        public virtual long SeqPessoaJuridica { get; set; }
        
        public virtual decimal ValorPercentual { get; set; }
        
        public PessoaJuridicaVO PessoaJuridica { get; set; }

        [SMCMapProperty("PessoaAtuacaoBeneficioResponsavelFinanceiro.PessoaJuridica.RazaoSocial")]
        public string NomePessoaJuridica { get; set; }

        public TipoResponsavelFinanceiro TipoResponsavelFinanceiro { get; set; }

        public string RazaoSocial { get; set; }
    }
}
