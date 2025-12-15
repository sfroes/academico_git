using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class InstituicaoNivelBeneficioListaData : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqBeneficio { get; set; }

        public List<InstituicaoNivelBeneficioListarConfiguracaBeneficioData> ConfiguracoesBeneficio { get; set; }

        public List<InstituicaoNivelBeneficioListarBeneficioHistoricoValorAuxilioData> BeneficiosHistoricosValoresAuxilio { get; set; }

        [SMCMapProperty("Beneficio.Descricao")]
        public string DescricaoBeneficio { get; set; }

        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }
    }
}
