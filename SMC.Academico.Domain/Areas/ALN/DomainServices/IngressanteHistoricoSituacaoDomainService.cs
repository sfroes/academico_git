using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class IngressanteHistoricoSituacaoDomainService : AcademicoContextDomain<IngressanteHistoricoSituacao>
    {
        /// <summary>
        /// Atualiza a situação de uma lista de ingressante para ficar apto para matrícula
        /// </summary>
        /// <param name="seqsIngressante">Listagem de sequenciais dos igressantes para ficar apto para matrícula</param>
        public void AtualizarSituacaoNaLiberacaoMatriculaIngressante(List<long> seqsIngressante)
        {
            var registros = seqsIngressante.Select(s => new IngressanteHistoricoSituacao() {
                SeqIngressante = s,
                SituacaoIngressante = SituacaoIngressante.AptoMatricula
            });

            this.BulkSaveEntity(registros.ToList());
        }
    }
} 