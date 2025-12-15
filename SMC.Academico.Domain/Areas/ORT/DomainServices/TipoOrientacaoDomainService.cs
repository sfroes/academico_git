using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Domain;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class TipoOrientacaoDomainService : AcademicoContextDomain<TipoOrientacao>
    {
        /// <summary>
        /// Buscar tipo de orientação
        /// </summary>
        /// <param name="seq">Sequencial tipo de orientação</param>
        /// <returns>Tipo de orientação</returns>
        public TipoOrientacao BuscarTipoOrientacao(long seq)
        {
            return this.SearchByKey(new SMCSeqSpecification<TipoOrientacao>(seq));
        }

        /// <summary>
        /// Valida se o tipo de orientação e de conclusão de curso - TCC
        /// </summary>
        /// <param name="seq">Sequencial do tipo de orientação</param>
        /// <returns>Verdaeiro caso seja TCC</returns>
        public bool ValidarTipoOrientacaoConclucaoCurso(long seq)
        {
            return this.SearchProjectionByKey(seq, p => p.TrabalhoConclusaoCurso);
        }
    }
}