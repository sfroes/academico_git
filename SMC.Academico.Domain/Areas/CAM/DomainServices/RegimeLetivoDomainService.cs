using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class RegimeLetivoDomainService : AcademicoContextDomain<RegimeLetivo>
    {
        /// <summary>
        /// Salva as informações do regime letivo
        /// </summary>
        /// <param name="regimeLetivo">Regime letivo a ser salvo</param>
        /// <returns>Sequencial do regime letivo salvo</returns>
        public long SalvarRegimeLetivo(RegimeLetivo regimeLetivo)
        {
            if (regimeLetivo.Seq > 0)
            {
                // Completa o regime letivo com as propriedades de navegação para validação
                var includes = IncludesRegimeLetivo.CiclosLetivos | IncludesRegimeLetivo.InstituicoesNivel | IncludesRegimeLetivo.Programas;
                var registro = this.SearchByKey(new RegimeLetivoFilterSpecification { Seq = regimeLetivo.Seq }, includes);

                if (registro.NumeroItensAno != regimeLetivo.NumeroItensAno &&
                    !((registro.CiclosLetivos.SMCCount() == 0) &&
                        (registro.InstituicoesNivel.SMCCount() == 0) &&
                        (registro.Programas.SMCCount() == 0)))
                {
                    throw new RegimeLetivoEmUsoException();
                }

                regimeLetivo.CiclosLetivos = registro.CiclosLetivos;
                regimeLetivo.InstituicoesNivel = registro.InstituicoesNivel;
                regimeLetivo.Programas = registro.Programas;
            }

            // Salva o regime letivo
            this.SaveEntity(regimeLetivo);

            // Retorna o sequencial do regime letivo salva
            return regimeLetivo.Seq;
        }
    }
}