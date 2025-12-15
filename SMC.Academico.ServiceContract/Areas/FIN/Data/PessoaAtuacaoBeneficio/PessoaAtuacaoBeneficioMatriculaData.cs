using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio
{
    public class PessoaAtuacaoBeneficioMatriculaData : ISMCMappable
    {
        public string DescricaoBeneficio { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public string TipoBeneficio { get; set; }

        public decimal? ValorBeneficio { get; set; }

        public FormaDeducao? FormaDeducao { get; set; }        
    }
}
