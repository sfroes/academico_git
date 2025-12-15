using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPeriodoIntercambioService : ISMCService
    {
        /// <summary>
        /// Buscar dados do intercambio do aluno baseado no periodo de intercambio dele
        /// </summary>
        /// <param name="seqPeriodoIntercambio">Sequancial Periodo Intercambio</param>
        /// <returns>Dados do intercambio</returns>
        PessoaAtuacaoTermoIntercambioData BuscarDadosIntercambioPorAluno(long seqPeriodoIntercambio);
    }
}

 