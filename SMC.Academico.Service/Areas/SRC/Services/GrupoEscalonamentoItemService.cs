using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class GrupoEscalonamentoItemService : SMCServiceBase, IGrupoEscalonamentoItemService
    {
        #region [ DomainService ]

        private GrupoEscalonamentoItemDomainService GrupoEscalonamentoItemDomainService
        {
            get { return this.Create<GrupoEscalonamentoItemDomainService>(); }
        }

        #endregion [ DomainService ]

        public long SalvarGrupoEscalonamentoItem(GrupoEscalonamentoItemData modelo)
        {
            var result = this.GrupoEscalonamentoItemDomainService.SalvarGrupoEscalonamentoItem(modelo.Transform<GrupoEscalonamentoItemVO>());

            return result;
        }

        /// <summary>
        /// Buscar grupo escalonamento item
        /// </summary>
        /// <param name="seq">Sequencial do grupo escalonamento item</param>
        /// <returns>Retorna o grupo de escalonamento item</returns>
        public GrupoEscalonamentoItemData BuscarGrupoEscalonamentoItem(long seq)
        {
            return this.GrupoEscalonamentoItemDomainService.BuscarGrupoEscalonamentoItem(seq).Transform<GrupoEscalonamentoItemData>();
        }

        /// <summary>
        ///  Valida se o escalonmento esta vigente baseado no grupo de escalomento e na esta do SGF
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <param name="seqEtapaSGF">Sequenial da etapa no SGF</param>
        /// <param name="somenteDataFinmEtapa">Leva em consideração a data fim da etapa</param>
        /// <returns>Se o escalonamento esta vigente ou não</returns>
        public bool ValidarEscalonmentoDaEtapaVigentePorGrupoEscalonamento(long seqGrupoEscalonamento, long seqEtapaSGF, bool somenteDataFinmEtapa = false)
        {
            return GrupoEscalonamentoItemDomainService.ValidarEscalonmentoDaEtapaVigentePorGrupoEscalonamento(seqGrupoEscalonamento, seqEtapaSGF, somenteDataFinmEtapa);
        }

        public bool ConsistenciasValidadasSalvarGrupoEscalonamentoParcela(GrupoEscalonamentoItemData modelo)
        {
          return GrupoEscalonamentoItemDomainService.ConsistenciasValidadasSalvarGrupoEscalonamentoParcela(modelo.Transform<GrupoEscalonamentoItemVO>());
        }
}
}