using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ReportHost.Areas.ALN.Models;
using SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.AssinaturaDigital.Common.Areas.DOC.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class DeclaracaoGenericaService : SMCServiceBase, IDeclaracaoGenericaService
    {
        #region [ DomainService ]

        private DeclaracaoGenericaDomainService DeclaracaoGenericaDomainService => Create<DeclaracaoGenericaDomainService>();

        #endregion [ DomainService ]

        public void AtualizarSituacaoDocumento(long seqDocumentoGad, StatusDocumento status)
        {
            DeclaracaoGenericaDomainService.AtualizarSituacaoDocumento(seqDocumentoGad, status);
        }

        public DeclaracaoGenericaDadosGeraisData BuscarDeclaracaoGenerica(long seqDocumento)
        {
            var retorno = DeclaracaoGenericaDomainService.BuscarDeclaracaoGenerica(seqDocumento).Transform<DeclaracaoGenericaDadosGeraisData>();
            
            return retorno; 
        }

        public SMCPagerData<DeclaracaoGenericaListarData> BuscarDeclaracoesGenericas(DeclaracaoGenericaFiltroData filtro)
        {
            var declaracoes = DeclaracaoGenericaDomainService.BuscarDeclaracoesGenericas(filtro
                                                                    .Transform<DeclaracaoGenericaFiltroVO>())
                                                                    .Transform<SMCPagerData<DeclaracaoGenericaListarData>>();
            return declaracoes;
        }

    }
}
