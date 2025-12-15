using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IConvocacaoService : ISMCService
    {
        long SalvarConvocacao(ConvocacaoData convocacao);

        SMCPagerData<ConvocacaoListaData> ListarConvocacoes(ConvocacaoFiltroData filtro);

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera os ingressantes convocados pela chamada para realizar a matrícula
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição (necessario para utilizar RawQuery)</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        /// <returns>Retorna um objeto com os impedimentos validados</returns>
        ConvocacaoImpedimentosDeMatriculaData VerificarImpedimentosExecutarMatriculaPorChamada(long seqInstituicao, long seqChamada);

        ConvocacaoData AlterarConvocacao(long seq);

        bool VerificarExistenciaConvocados(long seqInstituicao, long seqChamada);

        List<SMCDatasourceItem> BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect(long seqCampanha, long? seqCicloLetivo = null, long? seqTipoProcessoSeletivo = null, long? seqProcessoSeletivo = null);
    }
}