using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class PessoaAtuacaoBeneficioMatriculaVO : ISMCMappable
    {
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public long SeqBeneficio { get; set; }

        public string DescricaoBeneficio { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public string TipoBeneficio { get; set; }

        public decimal? ValorBeneficio { get; set; }

        public FormaDeducao? FormaDeducao { get; set; }

        public bool OrdenacaoBolsa { get; set; }

        public bool ExibeValoresTermoAdesao { get; set; }
    }
}