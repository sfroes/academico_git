using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IColaboradorService : ISMCService
    {
        /// <summary>
        /// Busca colaboradores com seus dados pessoais
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos colaboradores</returns>
        SMCPagerData<ColaboradorListaData> BuscarColaboradores(ColaboradorFiltroData filtros);

        /// <summary>
        /// Busca colaboradores com seus dados pessoais para o lookup de colaboradores
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        ColaboradorLookupData BuscarColaboradorLookup(long seq);

        /// <summary>
        /// Verifica se as quantidades de filiação do tipo de atuação colaborador estão configuradas na instituição
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        ColaboradorData BuscarConfiguracaoColaborador();

        /// <summary>
        /// Recupera um colaborador com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do colaborado</param>
        /// <returns>Dados do colaborador com suas dependências</returns>
        ColaboradorData BuscarColaborador(long seq);

        /// <summary>
        /// Grava um colaborador com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="colaborador">Dados do colaborador a ser gravado</param>
        /// <returns>Sequencial do colaborador</returns>
        long SalvarColaborador(ColaboradorData colaborador);

        /// <summary>
        /// Recupera os colaboradores por um tipo de atividade
        /// </summary>
        /// <param name="tipoAtividadeColaborador">Tipo de atividade desejada</param>
        /// <returns>Colaboradores do tipo de atividade desejada</returns>
        List<SMCDatasourceItem> BuscarColaboradoresPorTipoAtividadeSelect(TipoAtividadeColaborador tipoAtividadeColaborador);

        /// <summary>
        /// Busca os colaboradores com vínculo ativo na entidade do ingressante e com tipo de atividade “Orientação”
        /// no curso-oferta-localidade do curso-oferta-localidade-turno do item da oferta de campanha do ingressante.
        /// </summary>
        /// <param name="seqIngressante">Seq do ingressante</param>
        /// <returns>Lista de colaboradores</returns>
        List<SMCDatasourceItem> BuscarColaboradoresPorIngressanteSelect(long seqIngressante);

        /// <summary>
        /// Buscar colaboradores seguindo a RN_ORT_013 - Filtro Orientador do caso de uso Orientação
        /// </summary>
        /// <param name="colaboradorFiltroData">Dados de filtro</param>
        /// <returns>Sequencial e nome dos orientadores</returns>
        List<SMCDatasourceItem> BuscarColaboradoresOrientacaoSelect(ColaboradorFiltroData colaboradorFiltroData);

        /// <summary>
        /// Busca os colaboradoes que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Lista de colaboradores ordenados por nome</returns>
        List<SMCDatasourceItem> BuscarColaboradoresSelect(ColaboradorFiltroData filtros);

        /// <summary>
        /// Recupera um professor com a instituição selecionada no portal
        /// </summary>
        /// <param name="seqUsuarioSAS">Sequencial do usuario SAS</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Dados do colaborador</returns>
        ColaboradorData BuscarProfessorLogin(long seqUsuarioSAS, long seqInstituicaoEnsino);

        /// <summary>
        /// Busca os Colaboradores que tenham a atividade do tipo Orientação
        /// </summary>
        /// <param name="filtros">Filtro realizado na tela</param>
        /// <returns>Lista de colaboradores Orientadores ordenados por nome</returns>
        List<SMCDatasourceItem> BuscarColaboradoresOrientadores(ColaboradorOrientadorFiltroData filtros);

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado na turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma para filtrar apenas os vinculos relacionados a ela</param>
        /// <returns>Dados dos vínculos relacionados a Turma</returns>
        List<SMCDatasourceItem> BuscarEntidadeVinculoColaboradorPorTurmaSelect(long seqTurma);

        /// <summary>
        /// Buscar colaboradores para turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="tipoAtividadeColaborador">Atividade do colaborador</param>
        /// <returns>Lista de colaboradores em formato select</returns>
        List<SMCDatasourceItem> BuscarColaboradoresPorTurmaSelect(long seqTurma, TipoAtividadeColaborador tipoAtividadeColaborador);

        /// <summary>
        /// Buscar professores aptos a lecionar na grade com seu vinculo ativo mais longo
        /// </summary>
        /// <param name="colaboradorFiltroVO">Filtro do colaborador</param>
        /// <returns>Dados dos professores com seus vinculos</returns>
        List<ColaboradorGradeData> BuscarColaboradoresAptoLecionarGrade(ColaboradorFiltroData colaboradorFiltroData);

    }
}