using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class ConfiguracaoComponenteService : SMCServiceBase, IConfiguracaoComponenteService
    {
        #region [ DomainService ]

        private ComponenteCurricularDomainService ComponenteCurricularDomainService
        {
            get { return this.Create<ComponenteCurricularDomainService>(); }
        }

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService
        {
            get { return this.Create<ConfiguracaoComponenteDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar os dados de configuração do componente
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Registro da configuração do componente</returns>
        public ConfiguracaoComponenteData BuscarConfiguracaoComponente(long seq)
        {
            var configuracaoVO = ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponente(seq);
            return configuracaoVO.Transform<ConfiguracaoComponenteData>();
        }

        /// <summary>
        /// Buscar os dados de configuração inicial de acordo com o componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Configuração inicial do componente curricular</returns>
        public ConfiguracaoComponenteData BuscarConfiguracaoComponenteCurricular(long seqComponenteCurricular)
        {
            var configuracaoVO = ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteCurricular(seqComponenteCurricular);
            return configuracaoVO.Transform<ConfiguracaoComponenteData>();
        }

        /// <summary>
        /// Buscar as organizações cadastradas para o componente que podem ser selecionadas no cadastro de configuração - divisão
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com as organizações</returns>
        public List<SMCDatasourceItem> BuscarComponenteOrganizacoesSelect(long seqComponenteCurricular)
        {
            return ComponenteCurricularDomainService.BuscarComponenteOrganizacoesSelect(seqComponenteCurricular);
        }

        /// <summary>
        /// Buscar as configurações do componente curricular de um grupo curricular componente
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular compoente</param>
        /// <returns>Lista com as organizações</returns>
        public List<SMCDatasourceItem> BuscarConfiguracaoComponentePorGrupoCurricularComponenteSelect(long seqGrupoCurricularComponente)
        {
            return this.ConfiguracaoComponenteDomainService.
                BuscarConfiguracaoComponentePorGrupoCurricularComponenteSelect(seqGrupoCurricularComponente);
        }

        /// <summary>
        /// Buscar as configurações de componente, necessario para preencher o combo de entidades descrição na listagem
        /// </summary>
        /// <param name="filtros">Filtros definidos para a configuração de componente</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        public SMCPagerData<ConfiguracaoComponenteListaData> BuscarConfiguracoesComponentes(ConfiguracaoComponenteFiltroData filtros)
        {
            var lista = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtros.Transform<ConfiguracaoComponenteFilterSpecification>(), filtros.IgnorarFiltroDados);
            return lista.Transform<SMCPagerData<ConfiguracaoComponenteListaData>>();
        }

        /// <summary>
        /// Buscar as configurações de componente para o lookup
        /// </summary>
        /// <param name="filtros">Filtros definidos para a configuração de componente</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        public SMCPagerData<ConfiguracaoComponenteLookupListaData> BuscarConfiguracoesComponentesLookup(ConfiguracaoComponenteFiltroData filtros)
        {
            var lista = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentesLookup(filtros.Transform<ConfiguracaoComponenteFiltroVO>());
            return lista.Transform<SMCPagerData<ConfiguracaoComponenteLookupListaData>>();
        }

        /// <summary>
        /// Salvar a configuração do componente
        /// </summary>
        /// <param name="configuracaoComponenteData"></param>
        /// <returns>Sequencial da configuração do componente</returns>
        /// <exception cref="ConfiguracaoComponenteCargaHorariaDivergenteException">RN_CUR_079 validação de carga horária dos itens de organização</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoAlteracaoProibidaException">Caso seja alterado algum campo não permitido para uma configuração já associada</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException">Validação da carga horária das divisões</exception>
        /// <exception cref="ConfiguracaoComponenteInativoException">RN_CUR_083 - Consistência configuração componente</exception>
        /// <exception cref="ConfiguracaoComponenteOrganizacaoInvalidaException">Caso seja informado um tipo de organização inválido</exception>
        /// <exception cref="ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException">Caso sera informado um tipo de divisão inválido</exception>
        public long SalvarConfiguracaoComponente(ConfiguracaoComponenteData configuracaoComponenteData)
        {
            return ConfiguracaoComponenteDomainService.SalvarConfiguracaoComponente(configuracaoComponenteData.Transform<ConfiguracaoComponenteVO>());
        }

        /// <summary>
        /// Busca as configurações de componente de acordo com a matriz curricular oferta da pesso atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matricula</param>
        /// <returns>Lista configuração de componentes</returns>
        public SMCPagerData<ConfiguracaoComponenteListaData> BuscarConfiguracaoComponentePessoaAtuacaoEntidade(long seqPessoaAtuacao, long seqSolicitacaoMatricula)
        {
            var registro = this.ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponentePessoaAtuacaoEntidade(seqPessoaAtuacao, seqSolicitacaoMatricula);
            return registro.Transform<SMCPagerData<ConfiguracaoComponenteListaData>>();
        }

        /// <summary>
        /// Verifica se exige a configuração de componente pertence a um curriculo
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Valor Sim caso a configuração de componente esteja associado um grupo de componente com matriz curricular</returns>
        public bool VerificaConfiguracaoComponentePertenceCurriculo(long seq)
        {
            return ConfiguracaoComponenteDomainService.VerificaConfiguracaoComponentePertenceCurriculo(seq);
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            return ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(seqCicloLetivo, seqCursoOfertaLocalidade, seqTurno);
        }

        public string BuscarDescricaoConfiguracaoComponente(long seqConfiguracaoComponente)
        {
            return ConfiguracaoComponenteDomainService.BuscarDescricaoConfiguracaoComponente(seqConfiguracaoComponente);
        }

        /// <summary>
        /// Buscar dados do cabeçalho configuração do componente por matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial matriz curricular</param>
        public ConfiguracaoComponeteMatrizCabecalhoData BuscarCabecalhoConfiguracaoComponentePorMatriz(long seqMatrizCurricular)
        {
            return ConfiguracaoComponenteDomainService.BuscarCabecalhoConfiguracaoComponentePorMatriz(seqMatrizCurricular)
                                                      .Transform<ConfiguracaoComponeteMatrizCabecalhoData>();
        }
    }
}