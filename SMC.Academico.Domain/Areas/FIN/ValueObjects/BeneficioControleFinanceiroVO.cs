using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class BeneficioControleFinanceiroVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public long SeqCicloLetivo { get; set; }

        public int SeqContratoBeneficioFinanceiro { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime? DataExclusao { get; set; }
                
        public string UsuarioExclusao { get; set; }
    }
}
