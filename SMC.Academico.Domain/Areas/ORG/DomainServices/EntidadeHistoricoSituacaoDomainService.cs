using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Validation;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class EntidadeHistoricoSituacaoDomainService : AcademicoContextDomain<EntidadeHistoricoSituacao>
    {
        #region [ DataSources ]

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        #endregion [ DataSources ]

        /// <summary>
        /// Grava um registro de entidade histórico situação e caso tenha um registro anterior,
        /// atualiza a data final deste como a data inicial do novo registro -1
        /// </summary>
        /// <param name="entidadeHistoricoSituacao">Dados do histórico de situação</param>
        /// <returns>Sequencial do registro gravado</returns>
        public long SalvarEntidadeHistoricoSituacao(EntidadeHistoricoSituacao entidadeHistoricoSituacao)
        {
            MaterializarEntidade(entidadeHistoricoSituacao);
            var ultimoHistorico = RecuperarUltimoHistorico(entidadeHistoricoSituacao);
            if (ultimoHistorico != null)
                ultimoHistorico.DataFim = entidadeHistoricoSituacao.DataInicio.AddDays(-1);
            ValidarHisotricoEntidade(entidadeHistoricoSituacao);
            if (ultimoHistorico != null)
                this.SaveEntity(ultimoHistorico);
            this.SaveEntity(entidadeHistoricoSituacao);
            return entidadeHistoricoSituacao.Seq;
        }

        /// <summary>
        /// Exclui um registro de histórico de entidade situação e caso tenha um registro anterior,
        /// atualiza a data final deste para nulo
        /// </summary>
        /// <param name="seqEntidadeHistoricoSituacao">Sequencial do registro a ser excluído</param>
        public void ExcluirEntidadeHistoricoSituacao(long seqEntidadeHistoricoSituacao)
        {
            var entidadeHistoricoSituacao = this.SearchByKey(new EntidadeHistoricoSituacaoFilterSpecification() { Seq = seqEntidadeHistoricoSituacao });
            MaterializarEntidade(entidadeHistoricoSituacao);
            var ultimoHistorico = RecuperarUltimoHistorico(entidadeHistoricoSituacao);
            if (ultimoHistorico == null)
                throw new EntidadeHistoricoSituacaoExclusaoException();
            ultimoHistorico.DataFim = null;
            SaveEntity(ultimoHistorico);
            this.DeleteEntity(entidadeHistoricoSituacao);
        }

        private void MaterializarEntidade(EntidadeHistoricoSituacao entidadeHistoricoSituacao)
        {
            var entidade = this.EntidadeDomainService.SearchByKey(new EntidadeFilterSpecification() { Seq = entidadeHistoricoSituacao.SeqEntidade }, IncludesEntidade.HistoricoSituacoes);
            entidadeHistoricoSituacao.Entidade = entidade;
            entidade.HistoricoSituacoes = entidade.HistoricoSituacoes.Where(w => w.Seq != entidadeHistoricoSituacao.Seq).ToList();
            entidadeHistoricoSituacao.Entidade.HistoricoSituacoes.Add(entidadeHistoricoSituacao);
        }

        private static EntidadeHistoricoSituacao RecuperarUltimoHistorico(EntidadeHistoricoSituacao entidadeHistoricoSituacao)
        {
            return entidadeHistoricoSituacao.Entidade.HistoricoSituacoes
                .Where(w => w.Seq != entidadeHistoricoSituacao.Seq)
                .OrderBy(o => o.DataInicio)
                .LastOrDefault();
        }

        private static void ValidarHisotricoEntidade(EntidadeHistoricoSituacao entidadeHistoricoSituacao)
        {
            var validator = new EntidadeHistoricoSituacaoValidator();
            var results = validator.Validate(entidadeHistoricoSituacao);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }
        }
    }
}