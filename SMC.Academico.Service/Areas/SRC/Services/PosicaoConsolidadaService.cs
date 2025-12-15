using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class PosicaoConsolidadaService : SMCServiceBase, IPosicaoConsolidadaService
    {
        #region [ DomainService ]

        private ProcessoDomainService ProcessoDomainService
        {
            get { return this.Create<ProcessoDomainService>(); }
        }

        #endregion [ DomainService ]
        
        public SMCPagerData<PosicaoConsolidadaListarData> ListarPosicoesConsolidadas(PosicaoConsolidadaFiltroData filtro)
        {
            var result = this.ProcessoDomainService.BuscarPosicaoConsolidada(filtro.Transform<PosicaoConsolidadaFiltroVO>());

            return new SMCPagerData<PosicaoConsolidadaListarData>(result.TransformList<PosicaoConsolidadaListarData>());
        }
    }
}