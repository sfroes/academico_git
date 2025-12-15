using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IBeneficioHistoticoValorAuxilioService : ISMCService
    {
        /// <summary>
        /// Paginação dos valores auxilios de uma determinada intituição nivel beneficio
        /// </summary>
        /// <param name="filtro">Fitros</param>
        /// <returns>Lista paginada de valores auxilio de uma determianda instituição nivel beneficio </returns>
        SMCPagerData<BeneficioHistoricoValorAuxilioData> BuscarDadosValoresAuxilio(BeneficioHistoricoValorAuxilioFitroData filtros);

        /// <summary>
        /// Buscar o valor de auxilio rererente a uma determinda instiuição nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBenefico">Seq intituição nivel beneficio</param>
        /// <returns>Os valores auxilio de um determinado nivel beneficio</returns>
        BeneficioHistoricoValorAuxilioData BuscarDadosValorAuxilio(BeneficioHistoricoValorAuxilioData beneficioHistoricoValorAuxilio);

        /// <summary>
        /// Salvar o historico valor auxilio
        /// </summary>
        /// <param name="beneficioHistorico">Modelo benefico historico valor auxlio</param>
        /// <returns>Seq beneficio historico valor auxilio</returns>
        long SalvarBeneficioHistoricoValorAuxilio(BeneficioHistoricoValorAuxilioData beneficioHistoricoValorAuxilio);

    }
}
