using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IProcessoSeletivoOfertaService : ISMCService
    {
        SMCPagerData<ProcessoSeletivoOfertaListaData> BuscarProcessosSeletivoOferta(ProcessoSeletivoOfertaFiltroData filtro);

        /// <summary>
        /// Método que recupera uma lista de convocações, conforme o processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns>Lista de convocações do processo seletivo da oferta</returns>
        List<ConvocacaoData> BuscarConvocacoesProcessoSeletivo(long seqProcessoSeletivo);

        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivoOferta"></param>
        void Excluir(long seqProcessoSeletivoOferta);

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// </summary>
        /// <param name="data"></param>
        void AtualizarVagasOfertasProcessoSeletivo(VagasProcessoSeletivoOfertaData data);

        /// <summary>
        /// RN_CAM_068 - Copiar vagas da campanha
        /// </summary>
        /// <param name="data"></param>
        void CopiarVagasCampanha_RN_CAM_068(VagasProcessoSeletivoOfertaData data);

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// </summary>
        /// <param name="data"></param>
        void UsarVagasDisponiveis_RN_CAM_069(VagasProcessoSeletivoOfertaData data);
    }
}
