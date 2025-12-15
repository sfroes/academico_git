using SMC.Academico.Common.Areas.CSO.Exceptions.TipoFormacaoEspecifica;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class TipoFormacaoEspecificaDomainService : AcademicoContextDomain<TipoFormacaoEspecifica>
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoFormacaoEspecificaDomainService InstituicaoNivelTipoFormacaoEspecificaDomainService
        {
            get { return this.Create<InstituicaoNivelTipoFormacaoEspecificaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca um tipo de formação específica
        /// </summary>
        /// <param name="seq">Sequencial do tipo de formação específica a ser recuperado</param>
        /// <returns>Dados do tipo de formação específica</returns>
        public TipoFormacaoEspecificaVO BuscarTipoFormacaoEspecifica(long seq)
        {
            var tipoFormacaoEspecifica = this.SearchProjectionByKey(seq, x => new TipoFormacaoEspecificaVO
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
                Token = x.Token,
                Ativo = x.Ativo,
                ClasseTipoFormacao = x.ClasseTipoFormacao,
                ExigeGrau = x.ExigeGrau,
                PermiteTitulacao = x.PermiteTitulacao,
                ExibeGrauDescricaoFormacao = x.ExibeGrauDescricaoFormacao,
                PermiteEmitirDocumentoConclusao = x.PermiteEmitirDocumentoConclusao,
                GeraCarimbo = x.GeraCarimbo,
                TiposCurso = x.TiposCurso.Select(s => new TipoFormacaoEspecificaTipoCursoVO
                {
                    Seq = s.Seq,
                    SeqTipoFormacaoEspecifica = s.SeqTipoFormacaoEspecifica,
                    TipoCurso = s.TipoCurso
                }).ToList(),
            });

            return tipoFormacaoEspecifica;
        }

        /// <summary>
        /// Busca a lista de formações específicas para popular um Select por nível de ensino e instituição
        /// </summary>
        /// <param name="spec">FilterSpecification da Instituição nível por tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(InstituicaoNivelTipoFormacaoEspecificaFilterSpecification spec)
        {
            var ret = InstituicaoNivelTipoFormacaoEspecificaDomainService.SearchProjectionBySpecification(spec, x => x.TipoFormacaoEspecifica, true).TransformList<SMCDatasourceItem>();
            return ret?.OrderBy(o => o.Descricao)?.ToList();
        }

        /// <summary>
        /// Busca a lista de formações específicas por nível de ensino e instituição
        /// </summary>
        /// <param name="spec">FilterSpecification da Instituição nível por tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        public List<TipoFormacaoEspecifica> BuscarTipoFormacaoEspecificaPorNivelEnsino(InstituicaoNivelTipoFormacaoEspecificaFilterSpecification spec)
        {
            return InstituicaoNivelTipoFormacaoEspecificaDomainService.SearchProjectionBySpecification(spec, x => x.TipoFormacaoEspecifica, true).ToList();
        }

        /// <summary>
        /// Recupera todos os tipos de formação numa hierarquia de formações
        /// </summary>
        /// <param name="hierarquia">Hierarquia de formações específicas</param>
        /// <returns>Tipos das formações na hierarquia</returns>
        public IEnumerable<long> RecuperarTiposFormacoesEspecificas(List<FormacaoEspecificaNodeVO> hierarquia)
        {
            if (hierarquia != null)
            {
                foreach (var item in hierarquia)
                {
                    if (item.FormacoesFilhas != null)
                    {
                        foreach (var tipo in RecuperarTiposFormacoesEspecificas(item.FormacoesFilhas))
                        {
                            yield return tipo;
                        }
                    }
                    yield return item.SeqTipoFormacaoEspecifica;
                }
            }
        }

        /// <summary>
        /// Grava um tipo de formação específica
        /// </summary>
        /// <param name="modelo">Tipo de formação específica a ser gravada</param>
        /// <returns>Sequencial do tipo de formação específica gravada</returns>
        public long SalvarTipoFormacaoEspecifica(TipoFormacaoEspecificaVO modelo)
        {
            var dominio = modelo.Transform<TipoFormacaoEspecifica>();

            if (dominio.TiposCurso.GroupBy(n => n.TipoCurso).Any(c => c.Count() > 1))
                throw new TipoFormacaoEspecificaTipoCursoDuplicadaException();

            dominio.GeraCarimbo = dominio.PermiteEmitirDocumentoConclusao ? modelo.GeraCarimbo : null;
            dominio.ExibeGrauDescricaoFormacao = dominio.ExigeGrau ? modelo.ExibeGrauDescricaoFormacao : false;
            dominio.PermiteTitulacao = dominio.ExigeGrau ? true : modelo.PermiteTitulacao;

            this.SaveEntity(dominio);

            return dominio.Seq;
        }
    }
}