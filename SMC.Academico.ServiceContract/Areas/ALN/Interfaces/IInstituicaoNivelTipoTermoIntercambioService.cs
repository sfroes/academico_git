using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IInstituicaoNivelTipoTermoIntercambioService : ISMCService
    {

        InstituicaoNivelTipoTermoIntercambioData BuscarInstituicoesNivelTipoVinculoAluno(InstituicaoNivelTipoTermoIntercambioFiltroData filtro);

        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoTermoIntercambioSelect(long seqInstituicaoNivelTipoVinculoAluno);

        /// <summary>
        /// UC_ALN_004_01_04 - Manter Termo de Intercâmbio
        /// NV08 O agrupamento de campos só deve ser exibido para preenchimento e obrigatório
        /// (ou habilitado para preenchimento) se o tipo do termo selecionado tiver sido parametrizado,
        /// por instituição-nível-vínculo, para exigir período de intercâmbio no ingresso.
        /// </summary>
        /// <param name="seqTermoIntercambio">Sequencial do Termo de Intercâmbio.</param>
        /// <returns></returns>
        bool ExigirVigenciaTermoIntercambio(long seqParceriaIntercambioTipoTermo, long seqInstituicaoEnsino, long seqNivelEnsino);

        /// <summary>
        /// Verificar se os tipos de termo associados a instituição-nível-vínculo em questão já foram associados anteriormente a alguma instituição-nível-vínculo. 
        /// Caso já estiverem sido associados, eles devem ter o mesmo valor no campo "Concede formação" do cadastro anterior.
        /// </summary>
        /// <param name="seqInstituicaoNivelTipoVinculoAluno">Sequencial do Tipo de Vinculo do Aluno</param>
        /// <param name="seqTipoTermoIntercambio">Sequencial do Tipo de Termo de Intercâmbio.</param>
        /// <param name="seqInstituicaoNivel">Sequencial da Instituição Nível</param>
        /// <param name="concedeFormacao">Parâmetro Concede Formação</param>
        /// <returns>Se retornar registro não esta válido para gravar, precisa do retorno para a mensagem de erro</returns>
        List<string> ValidarTermoIntercambioInstituicaoNivel(long seqInstituicaoNivelTipoVinculoAluno, long seqTipoTermoIntercambio, long seqInstituicaoNivel, bool concedeFormacao);
    }
}
