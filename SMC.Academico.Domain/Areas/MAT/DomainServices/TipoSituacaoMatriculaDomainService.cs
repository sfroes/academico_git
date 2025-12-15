using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class TipoSituacaoMatriculaDomainService : AcademicoContextDomain<TipoSituacaoMatricula>
    {
        /// <summary>
        /// Busca todos os tipos de situações de matrícula que tenham o token matriculado
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas com o token matriculado</returns>
        public List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasTokenMatriculadoSelect()
        {
            List<SMCDatasourceItem> tipoSituacoes = new List<SMCDatasourceItem>();

            var allTipoSituacoes = this.SearchAll().OrderBy(o => o.Descricao);

            foreach (var item in allTipoSituacoes)
            {
                if (item.Token == TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO)
                {
                    tipoSituacoes.Add(new SMCDatasourceItem(item.Seq, item.Descricao, true));
                }
                else
                {
                    tipoSituacoes.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
                }
            }
            return tipoSituacoes;
        }

        /// <summary>
        /// Busca todos os tipos de situações de matrícula
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas</returns>
        public List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasSelect()
        {
            List<SMCDatasourceItem> tipoSituacoes = new List<SMCDatasourceItem>();

            var allTipoSituacoes = this.SearchAll().OrderBy(o => o.Descricao);

            foreach (var item in allTipoSituacoes)
            {
                tipoSituacoes.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }
            return tipoSituacoes;
        }

        /// <summary>
        /// Busca o token de uma tipo de situação de matrícula pelo seq
        /// </summary>
        /// <param name="seqTipoSituacaoMatricula">Sequencial do tipo da situacao de matrícula</param>
        /// <returns></returns>
        public string BuscarTokenTipoSituacaoMatricula(long seqTipoSituacaoMatricula)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<TipoSituacaoMatricula>(seqTipoSituacaoMatricula), t => t.Token);
        }
    }
}