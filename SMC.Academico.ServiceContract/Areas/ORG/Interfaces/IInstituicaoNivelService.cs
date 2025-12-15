using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoNivelService : ISMCService
    {
        List<SMCDatasourceItem> BuscarNiveisEnsinoPorEntidadeSelect(long seqEntidade);

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoDaInstituicaoSelect();

        /// <summary>
        /// Buscar seq da unidade responsável
        /// </summary>
        /// <returns></returns>
        long BuscarSeqUnidadeResponsavelAgd(long seqInstituicaoNivel);

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoSelect();

        /// <summary>
        /// Busca a lista de instituições níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial da Instituicao Nivel</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoComSequencialInstituicaoNivelSelect();

        /// <summary>
        /// Busca a instituição nível de um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Dados da instituição nível</returns>
        InstituicaoNivelData BuscarInstituicaoNivelPorCurso(long seqCurso);

        /// <summary>
        /// Busca a instituição nível de um nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados da nível de ensino na instituição</returns>
        InstituicaoNivelData BuscarInstituicaoNivelPorNivelEnsino(long seqNivelEnsino);

        /// <summary>
        /// Busca a lista de níveis de ensino com reconhecimento LDB da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoReconhecidoLDBSelect();

        /// <summary>
        /// Busca a instituição nível de um nível ensino
        /// </summary>
        /// <param name="seq">Sequencial do instituição nível de ensino</param>
        /// <returns>Dados da nível de ensino na instituição</returns>
        InstituicaoNivelData BuscarInstituicaoNivel(long seq);

        /// <summary>
        /// Busca os níveis de ensino associados ao ciculo letivo do processo seletivo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo</param>
        /// <returns>Dados dos níveis de ensino encontrados</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoPorProcessoSeletivoSelect(long seqProcessoSeletivo);

        /// <summary>
        /// Busca Órgão regulador, de acordo com Instituição x Nível de Ensino.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial Nivel Ensino</param>
        /// <param name="seqInstituicaoEnsino">Sequencial Instituicao Ensino</param>
        /// <returns>Retorno do órgão regulador</returns>
        List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(long seqNivelEnsino, long seqInstituicaoEnsino);

        /// <summary>
        /// Busca Órgão regulador, todos parametrizados no Nível de Ensino com a Instituição
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial Instituicao Ensino</param>
        /// <returns>Retorno do órgão regulador</returns>
        List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicao(long seqInstituicaoEnsino);
    }
}