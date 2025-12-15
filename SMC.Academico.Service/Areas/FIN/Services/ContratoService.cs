using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.FIN;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class ContratoService : SMCServiceBase, IContratoService
    {
        #region [ Services ]

        private ContratoDomainService ContratoDomainService
        {
            get { return this.Create<ContratoDomainService>(); }
        }

        #endregion [ Services ]

        public ContratoData BuscarContrato(long Seq)
        {
            var data = this.ContratoDomainService.BuscarContrato(Seq).Transform<ContratoData>();

            return data;
        }

        public long SalvarContrato(ContratoData contrato)
        {
            var contratoDomain = contrato.Transform<Contrato>();

            return this.ContratoDomainService.SalvarContrato(contratoDomain);
        }

        public SMCPagerData<ContratoListarData> ListarContrato(ContratoFiltroData filtro)
        {
            var filtroVO = filtro.Transform<ContratoFiltroVO>();

            var list = ContratoDomainService.ListarContrato(filtroVO);

            var result = list.TransformList<ContratoListarData>();

            return new SMCPagerData<ContratoListarData>(result, list.Total);
        }

        public AdesaoContratoData BuscarAdesaoContrato(long seqSolicitacaoMatricula)
        {
            return ContratoDomainService.BuscarAdesaoContrato(seqSolicitacaoMatricula).Transform<AdesaoContratoData>();
        }
          public AdesaoContratoData BuscarAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula)
        {
            return ContratoDomainService.BuscarAdesaoContratoResidenciaMedica(seqSolicitacaoMatricula).Transform<AdesaoContratoData>();
        }

        public AdesaoContratoData AderirContrato(AdesaoContratoDadosData dados)
        {
            return ContratoDomainService.AderirContrato(dados.Transform<AdesaoContratoDadosVO>()).Transform<AdesaoContratoData>();
        }

        public AdesaoContratoData AderirContratoRenovacao(AdesaoContratoDadosData dados)
        {
            return ContratoDomainService.AderirContratoRenovacao(dados.Transform<AdesaoContratoDadosVO>()).Transform<AdesaoContratoData>();
        }

        public AdesaoContratoData AderirContratoResidenciaMedica(AdesaoContratoDadosData dados)
        {
            return ContratoDomainService.AderirContratoResidenciaMedica(dados.Transform<AdesaoContratoDadosVO>()).Transform<AdesaoContratoData>();
        }

        public SMCUploadFile GerarTermoAdesaoContrato(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            return ContratoDomainService.GerarTermoAdesaoContrato(seqSolicitacaoMatricula, gerarTermo);
        }

        public SMCUploadFile GerarTermoAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            return ContratoDomainService.GerarTermoAdesaoContratoResidenciaMedica(seqSolicitacaoMatricula, gerarTermo);
        }
    }
}