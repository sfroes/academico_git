using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class CursoOfertaLocalidadeService : SMCServiceBase, ICursoOfertaLocalidadeService
    {
        #region [ Service ]

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeDomainService>(); }
        }

        private InstituicaoNivelModalidadeDomainService InstituicaoNivelModalidadeDomainService
        {
            get { return this.Create<InstituicaoNivelModalidadeDomainService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService { get => Create<HierarquiaEntidadeItemDomainService>(); }

        #endregion [ Service ]

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades
        /// </summary>
        /// <param name="filtros">Filtros para a pesquisa</param>
        /// <returns>Dados das ofertas de curso paginados</returns>
        public SMCPagerData<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidade(CursoOfertaLocalidadeFiltroData filtros)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidade(filtros.Transform<CursoOfertaLocalidadeFiltroVO>()).Transform<SMCPagerData<CursoOfertaLocalidadeListaData>>();
        }

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades
        /// </summary>
        /// <param name="filtros">Filtros para a pesquisa</param>
        /// <returns>Dados das ofertas de curso</returns>
        public List<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidadePorToken(CursoOfertaLocalidadeFiltroData filtros)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadePorToken(filtros.Transform<CursoOfertaLocalidadeFiltroVO>()).TransformList<CursoOfertaLocalidadeListaData>();
        }

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais selecionados</param>
        /// <returns>Dados das ofertas de curso para o grid do lookup</returns>
        public List<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidadeGridLookup(long[] seqs)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeGridLookup(seqs).TransformList<CursoOfertaLocalidadeListaData>();
        }

        /// <summary>
        /// Busca o curso oferta localidade para o cabeçalho de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Dados do cabeçalho de curso oferta localidade</returns>
        public CursoOfertaLocalidadeCabecalhoData BurcarCursoOfertaLocalidadeCabecalhoPorCursoUnidade(long seqCursoUnidade, bool desativarfiltrosHierarquia = false)
        {
            var cursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.BurcarCursoOfertaLocalidadeCabecalhoPorCursoUnidade(seqCursoUnidade, desativarfiltrosHierarquia);

            return cursoOfertaLocalidade.Transform<CursoOfertaLocalidadeCabecalhoData>();
        }

        /// <summary>
        /// Busca o curso oferta localidade de acordo com o sequencial da entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade responsável</param>
        /// <returns>Dados da configuração do curso oferta localidade e sequencial do curso e instituição nível</returns>
        public CursoOfertaLocalidadeData BuscarConfiguracoesCursoOfertaLocalidade(long seqEntidade)
        {
            var cursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.BuscarConfiguracoesCursoOfertaLocalidade(seqEntidade);
            var configuracao = this.EntidadeService.BuscarConfiguracoesEntidadeComClassificacao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE);

            var retorno = cursoOfertaLocalidade.Transform<CursoOfertaLocalidadeData>(configuracao);

            // Se o usuario não tiver acesso ao token o retorno será false e os campos restringidos
            retorno.RestricaoCursoOferta = SMCSecurityHelper.Authorize(UC_CSO_001_02_03.PERMITIR_ALTERACAO_CURSO_OFERTA_LOCALIDADE);

            return retorno;
        }

        /// <summary>
        /// Busca o curso oferta localidade com suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        /// <param name="desativarFiltroHierarquia">Flag para desativar filtro de dados</param>
        /// <returns>Dados do curso oferta localidade</returns>
        public CursoOfertaLocalidadeData BuscarCursoOfertaLocalidade(long seq, bool desativarFiltroHierarquia = false)
        {
            var cursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertaLocalidade(seq, desativarFiltroHierarquia);
            var configuracao = this.EntidadeService
                .BuscarConfiguracoesEntidadeComClassificacao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE);

            return cursoOfertaLocalidade.Transform<CursoOfertaLocalidadeData>(configuracao);
        }

        /// <summary>
        /// Busca o curso oferta localidade com suas dependências com o filtro de dados desativado
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        /// <returns>Dados do curso oferta localidade</returns>
        public CursoOfertaLocalidadeData BuscarCursoOfertaLocalidadeFiltroDesativado(long seq)
        {
            var retorno = BuscarCursoOfertaLocalidade(seq, true);

            // Se o usuario não tiver acesso ao token o retorno será false e os campos restringidos
            retorno.RestricaoCursoOferta = SMCSecurityHelper.Authorize(UC_CSO_001_02_03.PERMITIR_ALTERACAO_CURSO_OFERTA_LOCALIDADE);

            return retorno;
        }

        /// <summary>
        /// Busca o curso oferta localidade para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de curso oferta localidade</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertaLocalidadePorCursoUnidadeSelect(long seqCursoUnidade)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertaLocalidadePorCursoUnidadeSelect(seqCursoUnidade);
        }

        /// <summary>
        /// Busca as localidades de uma unidade para um curso oferta localidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de localidades</returns>

        public List<SMCDatasourceItem> BuscarLocalidadesTipoCursoOfertaLocalidadeSelect(long seqCursoUnidade)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarLocalidadesTipoCursoOfertaLocalidadeSelect(seqCursoUnidade);
        }

        /// <summary>
        /// Busca todas as localidades ativas na visão de localidades vigente
        /// </summary>
        /// <returns>Dados das localidades</returns>
        public List<SMCDatasourceItem> BuscarLocalidadesAtivasSelect(bool apenasAtivos = true)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarLocalidadesAtivasSelect(apenasAtivos);
        }

        /// <summary>
        /// Busca todas as entidades superiores a curso oferta localidade
        /// </summary>
        /// <returns>Dados das localidades</returns>
        public List<SMCDatasourceItem> BuscarEntidadesSuperioresSelect(bool apenasAtivos = true)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarEntidadesPai(TipoVisao.VisaoLocalidades, TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE, apenasAtivos, true);
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de modalidades definida para o curso oferta localidade</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorCursoUnidadeSelect(long seqCursoUnidade)
        {
            return this.InstituicaoNivelModalidadeDomainService.BuscarModalidadesPorCursoUnidadeSelect(seqCursoUnidade);
        }

        /// <summary>
        /// Busca uma lista de unidades/localidades de acordo com a tabela curso oferta localidade
        /// definida pelo sequencial do curriculo curso oferta e pelo sequencial de modalidade
        /// </summary>
        /// <param name='seqCurriculoCursoOferta'>Sequencial do curriculo curso oferta</param>
        /// <param name='seqModalidade'>Sequencial da modalidade</param>
        /// <returns>Lista de unidades/localidades</returns>
        public List<SMCDatasourceItem> BuscarUnidadesLocalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long seqModalidade)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarUnidadesLocalidadesPorCurriculoCursoOfertaSelect(seqCurriculoCursoOferta, seqModalidade);
        }

        /// <summary>
        /// BUsca as ofertas curso localidade para a tela de replicar formação específica curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica</param>
        /// <returns>Lista de oferta curso localidade</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeReplicarCursoFormacaoEspecificaSelect(long seqCurso, long seqFormacaoEspecifica)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeReplicarCursoFormacaoEspecificaSelect(seqCurso, seqFormacaoEspecifica);
        }

        /// <summary>
        /// Recupera a mascara de curso oferta localidade segundo a regra RN_CSO_027
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <param name="seqLocalidade">Sequencial do item de hierarquia da localidade</param>
        /// <returns></returns>
        public string RecuperarMascaraCursoOfertaLocalidade(long? seqCursoOferta, long? seqLocalidade)
        {
            return this.CursoOfertaLocalidadeDomainService.RecuperarMascaraCursoOfertaLocalidade(seqCursoOferta, seqLocalidade);
        }

        /// <summary>
        /// Grava o curso oferta localidade e suas dependências
        /// </summary>
        /// <param name="cursoOfertaLocalidade">Dados do curso oferta localidade a ser gravado</param>
        /// <returns>Sequencial do curso oferta localidade gravado</returns>
        public long SalvarCursoOfertaLocalidade(CursoOfertaLocalidadeData cursoOfertaLocalidade, bool desativarFiltroHierarquia = false)
        {
            return this.CursoOfertaLocalidadeDomainService.SalvarCursoOfertaLocalidade(cursoOfertaLocalidade.Transform<CursoOfertaLocalidadeVO>(), desativarFiltroHierarquia);
        }

        /// <summary>
        /// Grava o curso oferta localidade e suas dependências com o filtro de dados desativado
        /// </summary>
        /// <param name="cursoOfertaLocalidade">Dados do curso oferta localidade a ser gravado</param>
        /// <returns>Sequencial do curso oferta localidade gravado</returns>
        public long SalvarCursoOfertaLocalidadeFiltroDesativado(CursoOfertaLocalidadeData cursoOfertaLocalidade)
        {
            return SalvarCursoOfertaLocalidade(cursoOfertaLocalidade, true);
        }

        /// <summary>
        /// Exclui o curso oferta localidade
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        public void ExcluirCursoOfertaLocalidade(long seq)
        {
            this.CursoOfertaLocalidadeDomainService.DeleteEntity(seq);
        }

        public List<SMCDatasourceItem> BuscarLocalidadesPorModalidadeSelect(long? seqModalidade, long? seqInstituicaoNivel, long? seqCursoUnidade, bool desativarfiltrosHierarquia = false)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarLocalidadesPorModalidadeSelect(seqModalidade, seqInstituicaoNivel, seqCursoUnidade, desativarfiltrosHierarquia);
        }

        /// <summary>
        /// Busca todas as origens finaiceiras do sistema GRA para listagem
        /// </summary>
        /// <returns>Lista de origens financeiras</returns>
        public List<SMCDatasourceItem> BuscarOrigensFinanceirasGRASelect()
        {
            return new List<SMCDatasourceItem>();
        }

        /// <summary>
        /// Recupera as ofertas de curso por localidade filhas das entidades responsáveis informadas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade responsavel</param>
        /// <returns>Ofertas de curso por localidade que atendam aos filtros</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect(long seqEntidadeVinculo)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavelSelect(seqEntidadeVinculo);
        }

        public List<CursoOfertaLocalidadeListaData> BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(long seqEntidadeVinculo)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(seqEntidadeVinculo).TransformList<CursoOfertaLocalidadeListaData>();
        }

        /// <summary>
        /// Buscar as localidades definidas na(s) matriz(es) associada(s) à turma.
        ///    - Se houver mais de uma matriz, exibir a união de todas as localidades de todas as matrizes.
        ///    - As localidades de exceção definidas nas ofertas de matrizes para as configurações que foram associadas à turma.    
        /// Se a turma que estiver sendo cadastrada não possuir oferta de matriz  associada, listar todas as localidades para a instituição de ensino em questão.
        /// </summary>
        /// <param name="ofertasMatriz"></param>
        /// <returns>Lista de Localidades</returns>
        public List<SMCDatasourceItem> BuscarLocalidadesMatrizTurma(List<MatrizCurricularOfertaData> ofertasMatriz)
        {
            return CursoOfertaLocalidadeDomainService.BuscarLocalidadesMatrizTurma(ofertasMatriz.TransformList<MatrizCurricularOfertaVO>());
        }

        /// <summary>
        /// Buscar os cursos ofertas localidades para replicar formação específica de programa
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de cursos ofertas localidades para replicar formação específica de programa</returns>
        public List<SMCDatasourceItem> BuscarCursosOfertasLocalidadesReplicarFormacaoEspecificaProgramaSelect(CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroData filtros)
        {
            return this.CursoOfertaLocalidadeDomainService.BuscarCursosOfertasLocalidadesReplicarFormacaoEspecificaProgramaSelect(filtros.Transform<CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroVO>());
        }

        /// <summary>
        /// Verifica se a Entidade é do tipo entidade CURSO_OFERTA_LOCALIDADE e pelo menos uma formação específica exigindo grau
        /// </summary>
        /// <param name="seq">Sequencial entidade</param>
        /// <returns></returns>
        public bool CursoOfertaLocalidadeExigeGrau(long seq)
        {
            return this.CursoOfertaLocalidadeDomainService.CursoOfertaLocalidadeExigeGrau(seq);
        }
    }
}