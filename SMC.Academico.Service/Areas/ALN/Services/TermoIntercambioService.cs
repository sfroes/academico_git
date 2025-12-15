using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class TermoIntercambioService : SMCServiceBase, ITermoIntercambioService
    {
        #region DomainService 

        private TermoIntercambioDomainService TermoIntercambioDomainService
        {
            get { return this.Create<TermoIntercambioDomainService>(); }
        }

        #endregion

        public TermoIntercambioCabecalhoData BuscarCabecalhoTermoIntercambio(long seqParceriaIntercambio)
        {
            return TermoIntercambioDomainService.BuscarCabecalhoTermoIntercambio(seqParceriaIntercambio).Transform<TermoIntercambioCabecalhoData>();
        }      

        public SMCPagerData<TermoIntercambioListarData> ListarTermoIntercambio(TermoIntercambioFiltroData filtro)
        {
            var pager = TermoIntercambioDomainService.ListarTermoIntercambio(filtro.Transform<TermoIntercambioFiltroVO>());

            return new SMCPagerData<TermoIntercambioListarData>(pager.TransformList<TermoIntercambioListarData>(), pager.Total); 
        }        

        public TermoIntercambioData PreencherModeloTermoIntercambio(long seq)
        {
            return TermoIntercambioDomainService.PreencherModeloTermoIntercambio(seq).Transform<TermoIntercambioData>();
        }

        public long SalvarTermoIntercambio(TermoIntercambioData modelo)
        {
            return TermoIntercambioDomainService.SalvarTermoIntercambio(SMCMapperHelper.Create<TermoIntercambioVO>(modelo));
        }

        public bool TermoIntercambioPossuiPessoaAtuacao(long seqTermoIntercambio)
        {
            return TermoIntercambioDomainService.TermoIntercambioPossuiPessoaAtuacao(seqTermoIntercambio);
        }

        public SMCPagerData<TermoIntercambioListarData> BuscarTermosIntercambiosLookup(TermoIntercambioLookupFiltroData filtro)
        {
            var pager = TermoIntercambioDomainService.BuscarTermosIntercambiosLookup(filtro.Transform<TermoIntercambioLookupFiltroVO>());

            return new SMCPagerData<TermoIntercambioListarData>(pager.TransformList<TermoIntercambioListarData>(), pager.Total);
        }

        public void ExcluirTermoIntercambio(long seq)
        {
            TermoIntercambioDomainService.ExcluirTermoIntercambio(seq);
        }

        public DadosSimplificadoTermoIntercambioData BuscarDadosTermoIntercambio(long seqTermoIntercambio)
        {
            return TermoIntercambioDomainService.BuscarDadosTermoIntercambio(seqTermoIntercambio).Transform<DadosSimplificadoTermoIntercambioData>();
        }
    }
}
