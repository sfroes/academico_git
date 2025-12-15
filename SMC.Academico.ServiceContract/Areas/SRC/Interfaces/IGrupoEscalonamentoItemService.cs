using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IGrupoEscalonamentoItemService : ISMCService
    {
        long SalvarGrupoEscalonamentoItem(GrupoEscalonamentoItemData modelo);

        /// <summary>
        /// Buscar grupo escalonamento item
        /// </summary>
        /// <param name="seq">Sequencial do grupo escalonamento item</param>
        /// <returns>Retorna o grupo de escalonamento item</returns>
        GrupoEscalonamentoItemData BuscarGrupoEscalonamentoItem(long seq);

        /// <summary>
        ///  Valida se o escalonmento esta vigente baseado no grupo de escalomento e na esta do SGF
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <param name="seqEtapaSGF">Sequenial da etapa no SGF</param>
        /// <param name="somenteDataFinmEtapa">Leva em consideração a data fim da etapa</param>
        /// <returns>Se o escalonamento esta vigente ou não</returns>
        bool ValidarEscalonmentoDaEtapaVigentePorGrupoEscalonamento(long seqGrupoEscalonamento, long seqEtapaSGF, bool somenteDataFinmEtapa = false);
        bool ConsistenciasValidadasSalvarGrupoEscalonamentoParcela(GrupoEscalonamentoItemData model);
    }
}