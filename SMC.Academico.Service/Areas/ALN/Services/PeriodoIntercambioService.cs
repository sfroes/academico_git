using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class PeriodoIntercambioService : SMCServiceBase, IPeriodoIntercambioService
    {
        #region DomainService 

        private PeriodoIntercambioDomainService PeriodoIntercambioDomainService
        {
            get => Create<PeriodoIntercambioDomainService>();
        }

        #endregion

        /// <summary>
        /// Buscar dados do intercambio do aluno baseado no periodo de intercambio dele
        /// </summary>
        /// <param name="seqPeriodoIntercambio">Sequancial Periodo Intercambio</param>
        /// <returns>Dados do intercambio</returns>
        public PessoaAtuacaoTermoIntercambioData BuscarDadosIntercambioPorAluno(long seqPeriodoIntercambio)
        {
            return PeriodoIntercambioDomainService.BuscarDadosIntercambioPorAluno(seqPeriodoIntercambio).Transform<PessoaAtuacaoTermoIntercambioData>();
        }
    }
}
