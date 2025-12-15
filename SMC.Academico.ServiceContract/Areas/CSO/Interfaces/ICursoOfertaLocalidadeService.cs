using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoOfertaLocalidadeService : ISMCService

    {
        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades
        /// </summary>
        /// <param name="filtros">Filtros para a pesquisa</param>
        /// <returns>Dados das ofertas de curso paginados</returns>
        SMCPagerData<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidade(CursoOfertaLocalidadeFiltroData filtros);

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais selecionados</param>
        /// <returns>Dados das ofertas de curso para o grid do lookup</returns>
        List<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidadeGridLookup(long[] seqs);

        /// <summary>
        /// Busca o curso oferta localidade para o cabeçalho de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Dados do cabeçalho de curso oferta localidade</returns>
        CursoOfertaLocalidadeCabecalhoData BurcarCursoOfertaLocalidadeCabecalhoPorCursoUnidade(long seqCursoUnidade, bool desativarfiltrosHierarquia = false);

        /// <summary>
        /// Busca o curso oferta localidade de acordo com o sequencial da entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade responsável</param>
        /// <returns>Dados da configuração do curso oferta localidade e sequencial do curso e instituição nível</returns>
        CursoOfertaLocalidadeData BuscarConfiguracoesCursoOfertaLocalidade(long seqEntidade);

        /// <summary>
        /// Busca o curso oferta localidade com suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        /// <param name="desativarFiltroHierarquia">Flag para desativar filtro de dados</param>
        /// <returns>Dados do curso oferta localidade</returns>
        CursoOfertaLocalidadeData BuscarCursoOfertaLocalidade(long seq, bool desativarFiltroHierarquia = false);

        /// <summary>
        /// Busca o curso oferta localidade com suas dependências com o filtro de dados desativado
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        /// <returns>Dados do curso oferta localidade</returns>
        CursoOfertaLocalidadeData BuscarCursoOfertaLocalidadeFiltroDesativado(long seq);
       
        /// <summary>
        /// Busca o curso oferta localidade para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de curso oferta localidade</returns>
        List<SMCDatasourceItem> BuscarCursoOfertaLocalidadePorCursoUnidadeSelect(long seqCursoUnidade);

        /// <summary>
        /// Busca as localidades de uma unidade para um curso oferta localidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de localidades</returns>
        List<SMCDatasourceItem> BuscarLocalidadesTipoCursoOfertaLocalidadeSelect(long seqCursoUnidade);

        /// <summary>
        /// Busca todas as localidades ativas na visão de localidades vigente
        /// </summary>
        /// <returns>Dados das localidades</returns>
        List<SMCDatasourceItem> BuscarLocalidadesAtivasSelect(bool apenasAtivos = true);

        /// <summary>
        /// Busca todas as entidades superiores a curso oferta localidade
        /// </summary>
        /// <returns>Dados das localidades</returns>
        List<SMCDatasourceItem> BuscarEntidadesSuperioresSelect(bool apenasAtivos = true);

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de modalidades definida para o curso oferta localidade</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorCursoUnidadeSelect(long seqCursoUnidade);

        /// <summary>
        /// Busca localidades para listagem de acordo com a modadalidade
        /// </summary>
        /// <param name="seqModalidade">Sequencial da modalidade</param>
        /// <returns>Lista de modalidades, definida para a modalidade</returns>
        List<SMCDatasourceItem> BuscarLocalidadesPorModalidadeSelect(long? seqModalidade, long? SeqInstituicaoNivel, long? seqCursoUnidade, bool desativarfiltrosHierarquia = false);

        /// <summary>
        /// Busca uma lista de unidades/localidades de acordo com a tabela curso oferta localidade
        /// definida pelo sequencial do curriculo curso oferta e pelo sequencial de modalidade
        /// </summary>
        /// <param name='seqCurriculoCursooferta'>Sequencial do curriculo curso oferta</param>
        /// <param name='seqModalidade'>Sequencial da modalidade</param>
        /// <returns>Lista de unidades/localidades</returns>
        List<SMCDatasourceItem> BuscarUnidadesLocalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long seqModalidade);

        /// <summary>
        /// BUsca as ofertas curso localidade para a tela de replicar formação específica curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica</param>
        /// <returns>Lista de oferta curso localidade</returns>
        List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeReplicarCursoFormacaoEspecificaSelect(long seqCurso, long seqFormacaoEspecifica);

        /// <summary>
        /// Recupera a mascara de curso oferta localidade segundo a regra RN_CSO_027
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <param name="seqLocalidade">Sequencial do item de hierarquia da localidade</param>
        /// <returns></returns>
        string RecuperarMascaraCursoOfertaLocalidade(long? seqCursoOferta, long? seqLocalidade);

        /// <summary>
        /// Grava o curso oferta localidade e suas dependências
        /// </summary>
        /// <param name="cursoOfertaLocalidade">Dados do curso oferta localidade a ser gravado</param>
        /// <returns>Sequencial do curso oferta localidade gravado</returns>
        long SalvarCursoOfertaLocalidade(CursoOfertaLocalidadeData cursoOfertaLocalidade, bool desativarFiltroHierarquia = false);

        /// <summary>
        /// Grava o curso oferta localidade e suas dependências com o filtro de dados desativado
        /// </summary>
        /// <param name="cursoOfertaLocalidade">Dados do curso oferta localidade a ser gravado</param>
        /// <returns>Sequencial do curso oferta localidade gravado</returns>
        long SalvarCursoOfertaLocalidadeFiltroDesativado(CursoOfertaLocalidadeData cursoOfertaLocalidade);

        /// <summary>
        /// Exclui o curso oferta localidade
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        void ExcluirCursoOfertaLocalidade(long seq);

        /// <summary>
        /// Busca origens finaiceiras do sistena GRA para a listagem
        /// </summary>
        /// <returns>Lista de origens finaiceiras</returns>
        List<SMCDatasourceItem> BuscarOrigensFinanceirasGRASelect();

        /// <summary>
        /// Recupera as ofertas de curso por localidade filhas das entidades responsáveis informadas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade responsavel</param>
        /// <returns>Ofertas de curso por localidade que atendam aos filtros</returns>
        List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect(long seqEntidadeVinculo);

        /// <summary>
        /// Buscar as localidades definidas na(s) matriz(es) associada(s) à turma.
        ///    - Se houver mais de uma matriz, exibir a união de todas as localidades de todas as matrizes.
        ///    - As localidades de exceção definidas nas ofertas de matrizes para as configurações que foram associadas à turma.    
        /// Se a turma que estiver sendo cadastrada não possuir oferta de matriz  associada, listar todas as localidades para a instituição de ensino em questão.
        /// </summary>
        /// <param name="ofertasMatriz"></param>
        /// <returns>Lista de Localidades</returns>
        List<SMCDatasourceItem> BuscarLocalidadesMatrizTurma(List<MatrizCurricularOfertaData> ofertasMatriz);

        /// <summary>
        /// Buscar os cursos ofertas localidades para replicar formação específica de programa
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de cursos ofertas localidades para replicar formação específica de programa</returns>
        List<SMCDatasourceItem> BuscarCursosOfertasLocalidadesReplicarFormacaoEspecificaProgramaSelect(CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroData filtros);

        /// <summary>
        /// Verifica se a Entidade é do tipo entidade CURSO_OFERTA_LOCALIDADE e pelo menos uma formação específica exigindo grau
        /// </summary>
        /// <param name="seq">Sequencial entidade</param>
        /// <returns></returns>
        bool CursoOfertaLocalidadeExigeGrau(long seq);
    }
}