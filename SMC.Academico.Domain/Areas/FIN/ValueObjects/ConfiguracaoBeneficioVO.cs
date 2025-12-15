using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ConfiguracaoBeneficioVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelBeneficio { get; set; }

        public TipoDeducao TipoDeducao { get; set; }

        public FormaDeducao FormaDeducao { get; set; }

        public decimal? ValorDeducao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public bool FlagUltimaConfiguracao { get; set; }

        public bool AssociacaoPessoaBeneficio { get; set; }

        public DateTime? DataBanco { get; set; }
    }
}
