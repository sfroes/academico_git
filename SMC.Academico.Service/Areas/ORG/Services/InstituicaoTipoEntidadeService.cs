using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Service.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoTipoEntidadeService : SMCServiceBase, IInstituicaoTipoEntidadeService
    {
        #region Servicos de Dominio

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeDomainService>(); }
        }

        private SituacaoEntidadeDomainService SituacaoEntidadeDomainService
        {
            get { return this.Create<SituacaoEntidadeDomainService>(); }
        }

        #endregion Servicos de Dominio

        /// <summary>
        /// Busca as configurações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Configurações do tipo de entidade na instituição</returns>
        public InstituicaoTipoEntidadeData BuscarTipoEntidadeDaInstituicao(long seqTipoEntidade)
        {
            return this.InstituicaoTipoEntidadeDomainService.BuscarTipoEntidadeDaInstituicao(seqTipoEntidade).Transform<InstituicaoTipoEntidadeData>();
        }

        /// <summary>
        /// Busca as situações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de situações de um tipo de entidade na insituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesTipoEntidadeDaInstituicaoSelect(long seqTipoEntidade)
        {
            // Busca as situações do tipo de entidade da instituição
            var spec = new InstituicaoTipoEntidadeFilterSpecification { SeqTipoEntidade = seqTipoEntidade };
            var lista = InstituicaoTipoEntidadeDomainService.SearchByKey(spec, IncludesInstituicaoTipoEntidade.Situacoes | IncludesInstituicaoTipoEntidade.Situacoes_SituacaoEntidade);

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            if (lista != null && lista.Situacoes != null)
            {
                foreach (var item in lista.Situacoes.Where(s => s.Ativo).Select(s => s.SituacaoEntidade).OrderBy(s => s.Descricao))
                {
                    retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
                }
            }
            return retorno;
        }

        /// <summary>
        /// Busca a lista de tipos de entidade da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarTipoEntidadesDaInstituicaoSelect()
        {
            // Busca a lista de tipos de entidade configuradas para a instituição de ensino logada
            // Filtro da instituição de ensino é feito automaticamente pelo filtro de dados global
            var listaTipos = this.InstituicaoTipoEntidadeDomainService.SearchAll(i => i.TipoEntidade.Descricao,
                IncludesInstituicaoTipoEntidade.TipoEntidade);

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in listaTipos)
            {
                retorno.Add(new SMCDatasourceItem(item.SeqTipoEntidade, item.TipoEntidade.Descricao));
            }
            return retorno;
        }

        /// <summary>
        /// Busca os tipos de entidade superiores cujo tipo de entidade seja CURSO_OFERTA_LOCALIDADE
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect()
        {
            return this.InstituicaoTipoEntidadeDomainService.BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect();
        }

        /// <summary>
        /// Busca a lista tipos de entidade da instituição para popular um Select
        /// O sequencial retornado é o da instituição_tipo_entidade
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoTiposEntidadeSelect()
        {
            // Busca a lista de tipos de entidade configuradas para a instituição de ensino logada
            // Filtro da instituição de ensino é feito automaticamente pelo filtro de dados global
            var listaTipos = this.InstituicaoTipoEntidadeDomainService.SearchAll(i => i.TipoEntidade.Descricao, IncludesInstituicaoTipoEntidade.TipoEntidade);

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in listaTipos)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.TipoEntidade.Descricao));
            }
            return retorno;
        }

        /// <summary>
        /// Busca a lista de tipos de entidade da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarTipoEntidadesNaoExternadaDaInstituicaoSelect()
        {
            // Busca a lista de tipos de entidade configuradas para a instituição de ensino logada
            // Filtro da instituição de ensino é feito automaticamente pelo filtro de dados global
            var listaTipos = this.InstituicaoTipoEntidadeDomainService.SearchBySpecification(new InstituicaoTipoEntidadeFilterSpecification { EntidadeExternada = false }, IncludesInstituicaoTipoEntidade.TipoEntidade);

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in listaTipos)
            {
                retorno.Add(new SMCDatasourceItem(item.SeqTipoEntidade, item.TipoEntidade.Descricao));
            }
            return retorno;
        }

        /// <summary>
        /// Grava os parâmetros de uma instinuição validando que suas situações não sejam de categorias duplicadas
        /// </summary>
        /// <param name="instituicaoTipoEntidade">Dados dos parâmetros da instituição de ensino</param>
        /// <returns>Sequencial do objeto gravado</returns>
        public long SalvarInstituicaoTipoEntidade(InstituicaoTipoEntidadeData instituicaoTipoEntidadeData)
        {
            var instituicaoTipoEntidade = instituicaoTipoEntidadeData.Transform<InstituicaoTipoEntidade>();
            return InstituicaoTipoEntidadeDomainService.SalvarInstituicaoEntidade(instituicaoTipoEntidade);
        }

        /// <summary>
        /// Busca os tipos de entidade baseados em um filtro
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na busca</param>
        /// <returns>Lista de tipos de entidade na instituição</returns>
        public SMCPagerData<InstituicaoTipoEntidadeListaData> BuscarInstituicaoTiposEntidade(InstituicaoTipoEntidadeFiltroData filtro)
        {
            var total = 0;

            var spec = filtro.Transform<InstituicaoTipoEntidadeFilterSpecification>();

            var result = this.InstituicaoTipoEntidadeDomainService.SearchProjectionBySpecification(spec, x => new InstituicaoTipoEntidadeListaData
            {
                Seq = x.Seq,
                DescricaoTipoEntidade = x.TipoEntidade.Descricao
            }, out total);

            return new SMCPagerData<InstituicaoTipoEntidadeListaData>(result, total);
        }

        public List<SMCDatasourceItem> BuscarInstituicaoTiposEntidadeTokenSelect(string[] tokens)
        {
            var spec = new InstituicaoTipoEntidadeTokenFilterSpecification() { Tokens = tokens };

            var result = this.InstituicaoTipoEntidadeDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem() { Seq = x.Seq, Descricao = x.TipoEntidade.Descricao });

            return result.ToList();
        }

        public List<SMCDatasourceItem> BuscarSituacoesEntidadeSelect()
        {
            var result = this.SituacaoEntidadeDomainService
                .SearchAll()
                .Select(x => new SMCDatasourceItem() { Seq = x.Seq, Descricao = $"{x.Descricao} - {x.CategoriaAtividade.SMCGetDescription()}" })
                .OrderBy(s => s.Descricao)
                .ToList();

            return result;
        }

        public List<SMCDatasourceItem> BuscarTelefonesComCategoria(long SeqTipoEntidade)
        {
            var instituicaoTipoEntidade = BuscarTipoEntidadeDaInstituicao(SeqTipoEntidade);

            if (instituicaoTipoEntidade != null && instituicaoTipoEntidade.TiposTelefone != null)
            {
                var listaTelefones = new List<SMCDatasourceItem>();

                foreach (var telefone in instituicaoTipoEntidade.TiposTelefone)
                {
                    if (telefone.CategoriaTelefone != null)
                    {
                        var tel = new SMCDatasourceItem()
                        {
                            Descricao = $"{telefone.TipoTelefone.SMCGetDescription()} - {telefone.CategoriaTelefone}",
                            Seq = telefone.Seq
                        };

                        listaTelefones.Add(tel);
                    }
                }

                return listaTelefones;
            }
            else
            {
                return null;
            }
        }

        public bool ExisteTipoEntidadePorVinculoTipoFuncionario(List<long> seqsTipoEntidade)
        {

            var spec = new InstituicaoTipoEntidadeFilterSpecification
            {
                SeqsTipoEntidade = seqsTipoEntidade
            };
            var existeTipoEntidade = InstituicaoTipoEntidadeDomainService.SearchBySpecification(spec).Any();

            return existeTipoEntidade;
        }
    }
}