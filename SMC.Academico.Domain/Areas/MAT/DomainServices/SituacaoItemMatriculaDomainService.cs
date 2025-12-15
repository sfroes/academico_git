using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SituacaoItemMatriculaDomainService : AcademicoContextDomain<SituacaoItemMatricula>
    {
        /// <summary>
        /// Busca a situação do item matricula de acordo com a etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial da solicitação de matrícula</param>
        /// <param name="situacaoInicial">Filtro que verifica se é uma situação inicial</param>
        /// <param name="situacaoFinal">Filtro que verifica se é uma situação final</param>
        /// <param name="classificacaoSituacaoFinal">Enum de classificação</param>
        /// <returns>Objeto com a situação matricula item</returns>
        public SituacaoItemMatriculaVO BuscarSituacaoItemMatriculaEtapa(long seqProcessoEtapa, bool? situacaoInicial, bool? situacaoFinal, ClassificacaoSituacaoFinal? classificacaoSituacaoFinal)
        {
            var filtro = new SituacaoItemMatriculaFilterSpecification()
            {
                SeqProcessoEtapa = seqProcessoEtapa,
                SituacaoInicial = situacaoInicial,
                SituacaoFinal = situacaoFinal,
                ClassificacaoSituacaoFinal = classificacaoSituacaoFinal
            };

            var registro = this.SearchBySpecification(filtro).FirstOrDefault();

            return registro.Transform<SituacaoItemMatriculaVO>();
        }
    }
}