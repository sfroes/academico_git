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
    public class DivisaoMatrizCurricularComponenteService : SMCServiceBase, IDivisaoMatrizCurricularComponenteService
    {
        #region [ DomainServices ]

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService
        {
            get { return Create<DivisaoMatrizCurricularComponenteDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Busca o cabeçalho de um componente da matriz curricular
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular</param>
        /// <returns>Dados da matriz e do componente para o cabeçalho</returns>
        public DivisaoMatrizCurricularComponenteCabecalhoData DivisaoMatrizCurricularComponenteCabecalho(long seqMatrizCurricular, long seqGrupoCurricularComponente)
        {
            return DivisaoMatrizCurricularComponenteDomainService
                .DivisaoMatrizCurricularComponenteCabecalho(seqMatrizCurricular, seqGrupoCurricularComponente)
                .Transform<DivisaoMatrizCurricularComponenteCabecalhoData>();
        }

        /// <summary>
        /// Busca componentes de uma matriz curricular com suas configurações
        /// </summary>
        /// <param name="filtros">Filtros da configuração de componentes na matriz</param>
        /// <returns>Dados paginados dos componentes curriculares da matriz agrupados por grupo curricular componente</returns>
        public SMCPagerData<ConfiguracaoComponeteMatrizListarData> BuscarDivisaoMatrizCurricularGruposComponentes(DivisaoMatrizCurricularComponenteFiltroData filtros)
        {
            // Desconsidera o grupo no filtro pois a busca de divisões nunca deveria ser filtrada pelo grupo.
            // O dynamic limpa da querystring do botão voltar apenas o campo Seq.
            // E na edição de uma DivisaoMatrizCurricularComponenteGrupo também é passado o SeqGrupoCurricularComponente
            // para caso o seq da divisão seja 0.
            filtros.SeqGrupoCurricularComponente = null;

            return DivisaoMatrizCurricularComponenteDomainService
                .BuscarDivisaoMatrizCurricularGruposComponentes(filtros.Transform<DivisaoMatrizCurricularComponenteFiltroVO>())
                .Transform<SMCPagerData<ConfiguracaoComponeteMatrizListarData>>();
        }

        /// <summary>
        /// Buscar configuração para nova configuração de componente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular, grupo componente curricular e currículo curso oferta</param>
        /// <returns>Dados da nova divisão matriz curricular componente</returns>
        public DivisaoMatrizCurricularComponenteData BuscarConfiguracaoNovaDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFiltroData filtro)
        {
            return DivisaoMatrizCurricularComponenteDomainService
                .BuscarConfiguracaoNovaDivisaoMatrizCurricularComponente(filtro.Transform<DivisaoMatrizCurricularComponenteFilterSpecification>())
                .Transform<DivisaoMatrizCurricularComponenteData>(filtro);
        }

        /// <summary>
        /// Busca uma divisão matriz curricular compoenete pelo seu grupo curricular compomente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular e grupo componente curricular</param>
        /// <returns>Dados da divisão matriz curricular componente</returns>
        public DivisaoMatrizCurricularComponenteData BuscarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFiltroData filtro)
        {
            return DivisaoMatrizCurricularComponenteDomainService
                .BuscarDivisaoMatrizCurricularComponente(filtro.Transform<DivisaoMatrizCurricularComponenteFilterSpecification>())
                .Transform<DivisaoMatrizCurricularComponenteData>(filtro); ;
        }

        /// <summary>
        /// Valida se vai exibir o assert ao salvar uma configuração
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser validada</param>
        /// <returns>Retorno se vai exibir o assert, e lista de grupos curriculares do componente da configuração</returns>
        public (bool ExibirAssert, List<GrupoCurricularComponenteData> ListaGruposCurricularesComponente) ValidarAssertSalvar(DivisaoMatrizCurricularComponenteData divisaoMatrizCurricularComponente)
        {
            var result = DivisaoMatrizCurricularComponenteDomainService.ValidarAssertSalvar(divisaoMatrizCurricularComponente.Transform<DivisaoMatrizCurricularComponenteVO>());
            return (result.ExibirAssert, result.ListaGruposCurricularesComponente.TransformList<GrupoCurricularComponenteData>());
        }

        /// <summary>
        /// Grava a divisão matriz curricular componente com suas divisões e componentes subistitutos
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser gravada</param>
        /// <returns>Sequencial da divisão matriz curricular gravada</returns>
        public long SalvarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteData divisaoMatrizCurricularComponente)
        {
            return DivisaoMatrizCurricularComponenteDomainService
                .SalvarDivisaoMatrizCurricularComponente(divisaoMatrizCurricularComponente.Transform<DivisaoMatrizCurricularComponenteVO>());
        }

        /// <summary>
        /// Busca a lista de componente curricular assunto de acordo com as ofertas de matriz e componentes selecionados
        /// </summary>
        /// <param name="seqsMatrizCurricular">Sequenciais das matrizes curriculares</param>
        /// <returns>Lista de componentes assunto</returns>
        public List<SMCDatasourceItem> BuscarDivisaoComponenteAssuntoSelect(List<long> seqsMatrizCurricularOferta)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoComponenteAssuntoSelect(seqsMatrizCurricularOferta);
        }

        public List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularSelect(DivisaoMatrizCurricularComponenteFiltroData filtro)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoComponenteCurricularSelect(filtro.Transform<DivisaoComponenteCurricularFiltroVO>());
        }

        /// <summary>
        /// Buscar os assuntos de componentes ativos que existem cadastrados em TODAS as ofertas de matizes associadas à turma. Ou seja, 
        /// se a turma for compartilhada, o assunto a ser escolhido deve estar cadastrado em todas as ofertas de matrizes.
        /// A descrição do assunto deverá ser conforme RN_CUR_040 - Exibição descrição componente curricular, em ordem alfabética.
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta">Sequenciais das matrizes curriculares oferta</param>
        /// <param name="seqsConfiguracoesComponente">Sequenciais das configurações componentes da turma (Principal + Compartilhadas)</param>
        /// <returns>Lista de componentes assunto</returns>
        public List<SMCDatasourceItem> BuscarAssuntosComponentesOfertasMatrizesTurma(List<long> seqsMatrizCurricularOferta, List<long> seqsConfiguracoesComponente)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarAssuntosComponentesOfertasMatrizesTurma(seqsMatrizCurricularOferta, seqsConfiguracoesComponente);
        }

        /// <summary>
        /// Busca o cabeçalho da associação assunto pela configuração de componente
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Dados matriz e configuração de compoente para o cabeçalho</returns>
        public AssuntoComponeteMatrizCabecalhoData BusacarAssuntoComponenteMatrizCabecalho(long seqDivisaoMatrizCurricularComponente)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BusacarAssuntoComponenteMatrizCabecalho(seqDivisaoMatrizCurricularComponente)
                                                                 .Transform<AssuntoComponeteMatrizCabecalhoData>();
        }

        /// <summary>
        /// Excluir configuracao de componnente
        /// </summary>
        /// <param name="seq">Sequencial da configuração de componente</param>
        public void ExcluirConfiguracaoComponente(long seq)
        {
            DivisaoMatrizCurricularComponenteDomainService.ExcluirConfiguracaoComponente(seq);
        }

        /// <summary>
        /// Listar grupos curriculares associados ao componente curricular da configuração de componente em questão, na
        /// matriz curricular em questão com seus devidos assuntos
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Lista de grupos com assunto</returns>
        public List<AssuntoComponeteMatrizListarData> BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(long seqDivisaoMatrizCurricularComponente)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(seqDivisaoMatrizCurricularComponente)
                                                                 .TransformList<AssuntoComponeteMatrizListarData>();
        }


        /// <summary>
        /// Salvar Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        public void SalvarAssunto(long seq, long seqDivisaoMatrizCurricularComponente)
        {
            DivisaoMatrizCurricularComponenteDomainService.SalvarAssunto(seq, seqDivisaoMatrizCurricularComponente);
        }

        /// <summary>
        /// Excluir Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        public void ExcluirAssunto(long seq, long seqDivisaoMatrizCurricularComponente)
        {
            DivisaoMatrizCurricularComponenteDomainService.ExcluirAssunto(seq, seqDivisaoMatrizCurricularComponente);
        }

        /// <summary>
        /// Buscar lista do Enum Comprovante Artigo em ordem conferme regra
        /// </summary>
        /// <returns>Lista Enum ordenada</returns>
        public List<SMCDatasourceItem> BuscarComprovacaoArtigoOrdenada()
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarComprovacaoArtigoOrdenada();
        }

        public List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularProjetoQualificacao(DivisaoMatrizCurricularComponenteFiltroData filtro)
        {
            return DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoComponenteCurricularProjetoQualificacao(filtro.Transform<DivisaoComponenteCurricularFiltroVO>());
        }
    }
}