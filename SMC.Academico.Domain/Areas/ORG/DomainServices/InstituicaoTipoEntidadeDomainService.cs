using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.ORG.Exceptions;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoTipoEntidadeDomainService : AcademicoContextDomain<InstituicaoTipoEntidade>
    {
        #region [ DomainServices ]

        private SituacaoEntidadeDomainService SituacaoEntidadeDomainService
        {
            get { return this.Create<SituacaoEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Buscar as situações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de situações de um tipo de entidade na insituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesTipoEntidadeDaInstituicaoSelect(long seqTipoEntidade, bool listarInativos)
        {
            return BuscarSituacoesTipoEntidadeDaInstituicaoSelect(new InstituicaoTipoEntidadeFilterSpecification { SeqTipoEntidade = seqTipoEntidade }, listarInativos);
        }

        /// <summary>
        /// Buscar as situações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="tokenTipoEntidade">Token do tipo da entidade</param>
        /// <returns>Lista de situações de um tipo de entidade na insituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesTipoEntidadeDaInstituicaoSelect(string tokenTipoEntidade, bool listarInativos)
        {
            return BuscarSituacoesTipoEntidadeDaInstituicaoSelect(new InstituicaoTipoEntidadeFilterSpecification() { Token = tokenTipoEntidade }, listarInativos);
        }

        /// <summary>
        /// Busca as configurações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Configurações do tipo de entidade na instituição</returns>
        public InstituicaoTipoEntidade BuscarTipoEntidadeDaInstituicao(long seqTipoEntidade)
        {
            var spec = new InstituicaoTipoEntidadeFilterSpecification { SeqTipoEntidade = seqTipoEntidade };
            // Recupera o tipo de entidade da instituição (com referências à instituição para ativar o filtro global)
            var tipoEntidadeDaInstituicao = this.SearchByKey(spec, IncludesInstituicaoTipoEntidade.InstituicaoEnsino);
            if (tipoEntidadeDaInstituicao == null) { return new InstituicaoTipoEntidade(); }
            //Recupera as dependências da instituição (executar direto gera conflito com o filtro global)
            var includes = IncludesInstituicaoTipoEntidade.TiposEndereco |
                            IncludesInstituicaoTipoEntidade.TiposTelefone |
                            IncludesInstituicaoTipoEntidade.TiposEnderecoEletronico |
                            IncludesInstituicaoTipoEntidade.Situacoes;
            
            var instituicaoTipoEntidade = this.SearchByKey(new SMCSeqSpecification<InstituicaoTipoEntidade>(tipoEntidadeDaInstituicao.Seq), includes);

            return instituicaoTipoEntidade;
        }

        /// <summary>
        /// Salva uma instituição x tipo de entidade
        /// </summary>
        /// <param name="instituicaoTipoEntidade">Instituição x tipo de entidade a ser salva</param>
        /// <returns>Sequencial da intituição x tipo de entidade salva</returns>
        public long SalvarInstituicaoEntidade(InstituicaoTipoEntidade instituicaoTipoEntidade)
        {
            // Valida a instituição e seus relacionamentos
            ValidaInstituicaoTipoEntidade(instituicaoTipoEntidade);

            // Salva a instituição
            this.SaveEntity(instituicaoTipoEntidade);

            // Retorna o sequencial salvo
            return instituicaoTipoEntidade.Seq;
        }

        /// <summary>
        /// Busca os itens que possam ser pais de pai do tipo de entidade Curso Oferta Localidade (CURSO_OFERTA_LOCALIDADE)
        /// na hierarquia atual com a visão de Localidades.
        /// </summary>
        /// <returns>Lista contendo os tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect()
        {
            const TipoVisao visaoLocalidades = TipoVisao.VisaoLocalidades;

            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visaoLocalidades);

            // Se não encontrou a hierarquia, erro
            if (hierarquia == null)
            {
                throw new HierarquiaEntidadeNaoConfiguradaException(visaoLocalidades.SMCGetDescription());
            }

            // Recupera o seq do tipo entidade na instituição atual
            var seqTipoEntidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE).Seq;

            // Verifica no tipo da hierarquia, os tipos de itens que podem ser pai do tipo de entidade
            var specTipo = new TipoHierarquiaEntidadeItemFilterSpecification()
            {
                SeqTipoHierarquiaEntidade = hierarquia.SeqTipoHierarquiaEntidade,
                SeqPaiTipoEntidade = seqTipoEntidade
            };
            var tipos = TipoHierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specTipo, x => x.TipoEntidade, isDistinct: true);

            if (tipos.SMCCount() <= 0)
            {
                throw new TipoHierarquiaEntidadeNaoConfigurouTipoException(visaoLocalidades.SMCGetDescription());
            }

            // Monta a lista de retorno
            return tipos.TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Buscar as situações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="filtro">Filtro das entidades a serem consideradas</param>
        /// <returns>Lista de situações de um tipo de entidade na insituição</returns>
        private List<SMCDatasourceItem> BuscarSituacoesTipoEntidadeDaInstituicaoSelect(InstituicaoTipoEntidadeFilterSpecification filtro, bool listarInativos)
        {
            // Busca as situações do tipo de entidade da instituição
            var lista = this.SearchByKey(filtro, IncludesInstituicaoTipoEntidade.Situacoes | IncludesInstituicaoTipoEntidade.Situacoes_SituacaoEntidade);

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            if (lista != null && lista.Situacoes != null)
            {
                foreach (var item in lista.Situacoes.Where(s => listarInativos || s.Ativo).Select(s => s.SituacaoEntidade).OrderBy(s => s.Descricao))
                {
                    retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
                }
            }
            return retorno;
        }

        private void ValidaInstituicaoTipoEntidade(InstituicaoTipoEntidade instituicaoTipoEntidade)
        {
            //Validação de situações da mesma categoria
            if (instituicaoTipoEntidade.Situacoes != null && instituicaoTipoEntidade.Situacoes.Count > 1)
            {
                List<CategoriaAtividade> listaCategoria = new List<CategoriaAtividade>();
                instituicaoTipoEntidade.Situacoes.SMCForEach(x =>
                {
                    listaCategoria.Add(this.SituacaoEntidadeDomainService.SearchByKey(new SMCSeqSpecification<SituacaoEntidade>(x.SeqSituacaoEntidade)).CategoriaAtividade);
                });

                // Não permite cadastrar situações que sejam da mesma categoria.
                if (listaCategoria.Count > 1 && listaCategoria.GroupBy(s => s).Any(g => g.Count() > 1))
                {
                    throw new InstituicaoTipoEntidadeSituacaoCategoriaDuplicadaException();
                }
            }
        }
    }
}