using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IInstituicaoNivelTipoVinculoAlunoService : ISMCService
    {
        long SalvarInstituicaoNivelTipoVinculoAluno(InstituicaoNivelTipoVinculoAlunoData modelo);

        InstituicaoNivelTipoVinculoAlunoData AlterarInstituicaoNivelTipoVinculoAluno(long seq);

        SMCPagerData<InstituicaoNivelTipoVinculoAlunoListarData> ListarInstituicaoNivelTipoVinculoAluno(InstituicaoNivelTipoVinculoAlunoFiltroData filtros);

        /// <summary>
        /// Busca instituição nível tipo vínculo aluno de acordo com o sequencial do ingressante para validar parâmetros na solicitação de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do ingressante</param>
        /// <returns>Parâmetros de instituição nível tipo vínculo aluno</returns>
        InstituicaoNivelTipoVinculoAlunoData BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(long seqPessoaAtuacao);

        /// <summary>
        /// Busca o vínculo de aluno pelo tipo e nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Nível de ensino do vínculo</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vínculo</param>
        /// <returns>Configuração do vínculo par o tipo e nível informada na instituição</returns>
        InstituicaoNivelTipoVinculoAlunoData BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(long seqNivelEnsino, long seqTipoVinculoAluno);

        /// <summary>
        /// Busca os tipos de vínculos das forma de ingresso associadas ao tipo do processo seletivo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo</param>
        /// <returns>Dados dos vínculos</returns>
        List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorProcesso(long seqProcessoSeletivo);

        /// <summary>
        /// Buscar os tipos de vinculos por nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsino(long seqNivelEnsino);

        /// <summary>
        /// Buscar os tipos de vinculos no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino);

        /// <summary>
        /// Buscar os tipos de intercambio no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo);

        /// <summary>
        /// Buscar os tipos de operação no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <param name="seqTipoIntercambio">Sequencial tipo de intercambio</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTipoOperacaoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo, long? seqTipoIntercambio);

        /// <summary>
        /// Buscar os tipos de orientação no qual exista tipo de orientação que esteja configurado para permitir inclusão manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <param name="SeqTipoTermoIntercambio">Sequencial tipo de intercambio</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual(long seqNivelEnsino, long seqTipoVinculo, long? SeqTipoTermoIntercambio);
    }
}