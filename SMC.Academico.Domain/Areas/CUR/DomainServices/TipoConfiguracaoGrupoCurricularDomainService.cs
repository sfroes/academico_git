using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class TipoConfiguracaoGrupoCurricularDomainService : AcademicoContextDomain<TipoConfiguracaoGrupoCurricular>
    {
        public long SalvarTipoConfiguracaoGrupoCurricular(TipoConfiguracaoGrupoCurricularVO tipoConfiguracaoGrupoCurricularVO)
        {
            //Indica se é um novo registro
            bool isNewSelfRelatedEntity = tipoConfiguracaoGrupoCurricularVO.Seq == 0 && tipoConfiguracaoGrupoCurricularVO.SelfSubgrupo;

            TipoConfiguracaoGrupoCurricular tipoConfiguracaoGrupoCurricular = tipoConfiguracaoGrupoCurricularVO.Transform<TipoConfiguracaoGrupoCurricular>();

            if (isNewSelfRelatedEntity)
            {
                CreateSelfRelationship(tipoConfiguracaoGrupoCurricular, isNewSelfRelatedEntity);
                //Salva o item em um contexto isolado pois é necessário recarregar o contexto de cache do entity framework
                //sem isto ele salva a entidade mas não consegue fazer o auto relacionamento.
                UpdateInIsolatedContext(tipoConfiguracaoGrupoCurricular);
            }
            else
            {
                SaveEntity(tipoConfiguracaoGrupoCurricular);
            }
            return tipoConfiguracaoGrupoCurricular.Seq;
        }

        /// <summary>
        /// Exclui um Tipo de Configuracao para um Grupo Curricular de modo que evite a recursividade da exclusão
        /// </summary>
        /// <param name="tipoConfiguracaoGrupoCurricularVO"></param>
        public void ExcluirTipoConfiguracaoGrupoCurricular(TipoConfiguracaoGrupoCurricular tipoConfiguracaoGrupoCurricular)
        {
            //Iniciando a transacao
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                //Obtendo o TipoConfiguracaoGrupoCurricular, associado a ele mesmo (quando o grupo é subgrupo dele mesmo)
                TipoConfiguracaoGrupoCurricular selfTipoConfiguracaoGrupoCurricular = tipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos.Where(w => w.Seq == tipoConfiguracaoGrupoCurricular.Seq).FirstOrDefault();

                if (selfTipoConfiguracaoGrupoCurricular != null)
                {
                    //Se o registro for subgrupo dele mesmo ele será removido e então será feito um update antes do delete
                    tipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos.Remove(selfTipoConfiguracaoGrupoCurricular);

                    //update para remover a associação do subgrupo
                    SaveEntity(tipoConfiguracaoGrupoCurricular);
                }

                //detele
                base.DeleteEntity(tipoConfiguracaoGrupoCurricular);

                //commit
                transacao.Commit();
            }
        }

        /// <summary>
        /// Lista todos os grupos curriculares filhos do grupo curricular informado
        /// </summary>
        /// <param name="seqTipoConfiguracaoGrupoCurricularSuperior">Sequencial do grupo curricular superior, caso não seja informado serão listados grupos raiz</param>
        /// <returns>Grupos cadastrados como subgrupo do grupo informado ou grupos marcados como raiz caso o grupo superior não seja informado</returns>
        public List<SMCDatasourceItem> BuscarTiposConfiguracaoGrupoCurricularSelect(long? seqTipoConfiguracaoGrupoCurricularSuperior)
        {
            var spec = new TipoConfiguracaoGrupoCurricularFilterSpecification();

            if (seqTipoConfiguracaoGrupoCurricularSuperior.HasValue)
                spec.SeqTipoConfiguracaoGrupoCurricularSuperior = seqTipoConfiguracaoGrupoCurricularSuperior;
            else
                spec.Raiz = true;

            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao })
                .ToList();
        }

        /// <summary>
        /// Verifica se o registro será subgrupo dele mesmo, se for, o mesmo então será adicionado como filho
        /// </summary>
        /// <param name="tipoConfiguracaoGrupoCurricular">Entidade após ser salva na base</param>
        /// <param name="tipoConfiguracaoGrupoCurricularVO">EntidadeData</param>
        /// <param name="isNew">Indica se a inserção é de um novo registro</param>
        private void CreateSelfRelationship(TipoConfiguracaoGrupoCurricular tipoConfiguracaoGrupoCurricular, bool isNew)
        {
            SaveEntity(tipoConfiguracaoGrupoCurricular);

            if (tipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos == null)
                tipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos = new List<TipoConfiguracaoGrupoCurricular>();

            tipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos.Add(new TipoConfiguracaoGrupoCurricular { Seq = tipoConfiguracaoGrupoCurricular.Seq });
        }
    }
}