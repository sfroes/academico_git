using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ChamadaDomainService : AcademicoContextDomain<Chamada>
    {

        #region Jobs

        CargaIngressantesJob CargaIngressantesJob => Create<CargaIngressantesJob>();

        #endregion

        public void CargaIngressantes(CargaIngressanteSATVO data)
        {
            CargaIngressantesJob.Execute(data);
        }

        public void AtualizarSeqAgendamento(long seqChamada, long seqAgendamento)
        {
            var chamada = new Chamada()
            {
                Seq = seqChamada,
                SeqAgendamentoSat = seqAgendamento
            };
            UpdateFields(chamada, f => f.SeqAgendamentoSat);
        }

        public void EncerrarChamada(long seqChamada)
        {
            var chamada = new Chamada()
            {
                Seq = seqChamada,
                SituacaoChamada = SituacaoChamada.ChamadaEncerrada,
            };

            UpdateFields(chamada, f => f.SituacaoChamada);
        }

        public List<long> BuscarSeqsOfertasGPIPorSeqChamada(long seqChamada)
        {
            return SearchProjectionByKey(new SMCSeqSpecification<Chamada>(seqChamada), x => x.Convocacao.ProcessoSeletivo.Ofertas.Where(f => f.SeqHierarquiaOfertaGpi.HasValue).Select(f => f.SeqHierarquiaOfertaGpi.Value)).ToList();
        }

        public void AtualizaStatusProcessamento(long seqChamada, bool processando)
        {
            Chamada c = SearchByKey(new SMCSeqSpecification<Chamada>(seqChamada));
            c.Processando = processando;
            UpdateFields(c, a => a.Processando);
        }
    }
}