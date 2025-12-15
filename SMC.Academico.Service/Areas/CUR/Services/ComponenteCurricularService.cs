using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class ComponenteCurricularService : SMCServiceBase, IComponenteCurricularService
    {
        #region [ DomainService ]

        private ComponenteCurricularDomainService ComponenteCurricularDomainService
        {
            get { return this.Create<ComponenteCurricularDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar os dados de um componente curricular
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Informações do componente curricular</returns>
        public ComponenteCurricularData BuscarComponenteCurricular(long seq)
        {
            var componenteCurricularVO = ComponenteCurricularDomainService.BuscarComponenteCurricular(seq);
            return componenteCurricularVO.Transform<ComponenteCurricularData>();
        }

        /// <summary>
        /// Busca a descrição completa de um componente com seu tipo e tipo de organização
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Descrição completa, tipo do componente e tipo da sua organização</returns>
        public ComponenteCurricularCabecalhoData BuscarComponenteCurricularCabecalho(long seq)
        {
            return this.ComponenteCurricularDomainService.BuscarComponenteCurricularCabecalho(seq).Transform<ComponenteCurricularCabecalhoData>();
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados com alguns filtros obrigatórios
        /// </summary>
        /// <param name="filtros">Filtros da listagem de componentes curricular</param>
        /// <returns>SMCPagerData de componentes curricular</returns>
        public SMCPagerData<ComponenteCurricularListaData> BuscarComponentesCurriculares(ComponenteCurricularFiltroData filtros)
        {
            var lista = ComponenteCurricularDomainService.BuscarComponentesCurriculares(filtros.Transform<ComponenteCurricularFiltroVO>(), false);
            return lista.Transform<SMCPagerData<ComponenteCurricularListaData>>();
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados sem filtros obrigatórios
        /// </summary>
        /// <param name="filtros">Filtros da listagem de componentes curricular</param>
        /// <returns>SMCPagerData de componentes curricular</returns>
        public SMCPagerData<ComponenteCurricularListaData> BuscarComponentesCurricularesLookup(ComponenteCurricularFiltroData filtros)
        {
            var lista = ComponenteCurricularDomainService.BuscarComponentesCurriculares(filtros.Transform<ComponenteCurricularFiltroVO>(), false);
            return lista.Transform<SMCPagerData<ComponenteCurricularListaData>>();
        }

        /// <summary>
        /// Recupera um componente curricular por um grupo curricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular</param>
        /// <returns>Dados do componente curricular</returns>
        public ComponenteCurricularData BuscarComponenteCurricularPorGrupoCurricularComponente(long seqGrupoCurricularComponente)
        {
            return ComponenteCurricularDomainService
                .BuscarComponenteCurricularPorGrupoCurricularComponente(seqGrupoCurricularComponente)
                .Transform<ComponenteCurricularData>();
        }

        /// <summary>
        /// Busca os dados de um componente curricular com os detalhes para tela de visualização
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Informações do componente curricular</returns>
        public ComponenteCurricularDetalheData BuscarComponenteCurricularDetalhe(long seq)
        {
            return ComponenteCurricularDomainService.BuscarComponenteCurricularDetalhe(seq).Transform<ComponenteCurricularDetalheData>();
        }

        /// <summary>
        /// Buscar a lista de componentes curriculares que pertencem ao grupo curricular da matriz e no parâmetro permitem requisito
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="seqDivisaoCurricularItem">Sequencial da divisão da matriz curricular</param>
        /// <returns>Lista com os omponentes curriculares</returns>
        public List<SMCDatasourceItem> BuscarComponenteCurricularPorMatrizRequisitoSelect(long seqMatrizCurricular, long? SeqDivisaoCurricularItem)
        {
            return this.ComponenteCurricularDomainService.BuscarComponenteCurricularPorMatrizRequisitoSelect(seqMatrizCurricular, SeqDivisaoCurricularItem);
        }

        /// <summary>
        /// Buscar a lista de componentes curriculares que pertencem ao grupo curricular da matriz e no parâmetro permitem requisito
        /// </summary>
        /// <param name="tipoRequisito">Tipo do requisito selecionado</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular selecionada</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com os omponentes curriculares</returns>
        public List<SMCDatasourceItem> BuscarComponenteCurricularPorComponenteRequisitoSelect(TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqComponenteCurricular)
        {
            return this.ComponenteCurricularDomainService.BuscarComponenteCurricularPorComponenteRequisitoSelect(tipoRequisito, seqMatrizCurricular, seqComponenteCurricular);
        }

        /// <summary>
        /// Salvar o componente curricular com seus filhos
        /// </summary>
        /// <param name="componenteCurricularData"></param>
        /// <returns>Sequencial do Componente Curricular</returns>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException">RN_CUR_033.2.2 validação de associação com componente substituto</exception>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException">RN_CUR_033.2.1 validação de associação com grupo curricular</exception>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException">RN_CUR033.2.3 validação de associação com turma</exception>
        /// <exception cref="ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException">RN_CUR_036 Alteração componente curricular</exception>
        /// <exception cref="ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException">RN_CUR_033.1 validação de total de entidades resposáveis</exception>
        /// <exception cref="ComponenteCurricularSemOrganizacaoException">UC_CUR_002_01_02.NV18 Caso seja selecionado um tipo de organização e não seja informada nenhuma organização</exception>
        public long SalvarComponenteCurricular(ComponenteCurricularData componenteCurricularData)
        {
            return ComponenteCurricularDomainService.SalvarComponenteCurricular(componenteCurricularData.Transform<ComponenteCurricularVO>());
        }

        public List<SMCDatasourceItem> BuscarComponenteCurricularSelect(ComponenteCurricularFiltroData filtro)
        {
            return ComponenteCurricularDomainService.BuscarComponenteCurricularSelect(filtro.Transform<ComponenteCurricularFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarQuantidadesSemanasSelect() 
        {
            return ComponenteCurricularDomainService.BuscarQuantidadesSemanasSelect();
        }

        /// <summary>
        /// Busca um componente curricular sem suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Dados do componente curricular</returns>
        public ComponenteCurricularData BuscarComponenteCurricularSemDependencias(long seq)
        {
            return ComponenteCurricularDomainService.BuscarComponenteCurricularSemDependencias(seq).Transform<ComponenteCurricularData>();
        }

        /// <summary>
        /// Buscar os Assuntos de Componente Curricular, a busca pode ser feita pelo GrupoComponenteCurricular ou direto pelo ComponenteCurricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo componente curricular</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <param name="ignorarFormadosDispensados">Ignora os itens já formados ou dispensados pelo aluno</param>
        /// <returns>Lista com os Assuntos de Componentes Curriculares</returns>
        public List<SMCDatasourceItem> BuscarAssuntosComponenteCurricularSelect(long? seqGrupoCurricularComponente, long? seqComponenteCurricular, long seqCicloLetivo, long seqPessoaAtuacao, bool ignorarFormadosDispensados)
        {
            return this.ComponenteCurricularDomainService.BuscarAssuntosComponenteCurricularSelect(seqGrupoCurricularComponente, seqComponenteCurricular, seqCicloLetivo, seqPessoaAtuacao, ignorarFormadosDispensados);
        }

        /// <summary>
        /// Valida se o componente curricular foi parâmetrizado para exigir assunto, a validação pode ser feita pelo GrupoComponenteCurricular ou direto pelo ComponenteCurricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo componente curricular</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>     
        /// <returns>True para exigir assunto</returns>
        public bool ValidarComponenteCurricularExigeAssunto(long? seqGrupoCurricularComponente, long? seqComponenteCurricular)
        {
            return this.ComponenteCurricularDomainService.ValidarComponenteCurricularExigeAssunto(seqGrupoCurricularComponente, seqComponenteCurricular);
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados no lookup de dispensa baseado no histórico escolar do aluno
        /// </summary>
        /// <param name="filtros">Filtros da listagem de componentes curricular dispensa</param>
        /// <returns>SMCPagerData de componentes curricular do historico escolar</returns>
        public SMCPagerData<ComponenteCurricularListaData> BuscarComponentesCurricularesDispensaLookup(ComponenteCurricularDispensaFiltroData filtros)
        {
            var lista = ComponenteCurricularDomainService.BuscarComponentesCurricularesDispensaLookup(filtros.Transform<ComponenteCurricularDispensaFiltroVO>());
            return lista.Transform<SMCPagerData<ComponenteCurricularListaData>>();
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os seqs informados
        /// </summary>
        /// <param name="seqs">Seqs dos componentes curriculares</param>
        /// <returns>SMCPagerData de componentes curriculares</returns>
        public List<ComponenteCurricularListaData> BuscarComponentesCurricularesDispensa(long[] seqs)
        {
            var lista = ComponenteCurricularDomainService.BuscarComponentesCurricularesDispensa(seqs);
            return lista.TransformList<ComponenteCurricularListaData>();
        }

        /// <summary>
        /// Buscar as organizações do componente curricular que atenda o seq informado
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista de organizações encontradas</returns>
        public List<SMCDatasourceItem> BuscarOrganizacoesComponenteCurricularSelect(long seqComponenteCurricular)
        {
            return ComponenteCurricularDomainService.BuscarOrganizacoesComponenteCurricularSelect(seqComponenteCurricular);
        }

        /// <summary>
        /// Verifica se a quantidade de semanas de um componente curricular possui algum valor
        /// </summary>
        /// <param name="seqTipoDivisaoComponente">Sequencial do tipo de divisão do componente</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Retorna verdadeito caso a quantidade de semanas de um componente curricular possui algum valor</returns>
        public bool VerificarQuantidadeSemanasComponentePreenchida(long seqComponenteCurricular)
        {
            return ComponenteCurricularDomainService.VerificarQuantidadeSemanasComponentePreenchida(seqComponenteCurricular);
        }

        /// <summary>
        /// Busca os componentes curriculares de uma matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Retorna a lista de componentes curriculares daquela matriz</returns>
        public List<SMCDatasourceItem> BuscarComponenteCurricularPorMatrizSelect(long seqMatrizCurricular)
        {
            return ComponenteCurricularDomainService.BuscarComponenteCurricularPorMatrizSelect(seqMatrizCurricular);
        }
    }
}