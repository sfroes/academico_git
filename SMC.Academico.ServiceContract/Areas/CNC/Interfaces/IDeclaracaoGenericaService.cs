using SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica;
using SMC.AssinaturaDigital.Common.Areas.DOC.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IDeclaracaoGenericaService : ISMCService
    {
        void AtualizarSituacaoDocumento(long seqDocumentoGAD, StatusDocumento status);
        SMCPagerData<DeclaracaoGenericaListarData> BuscarDeclaracoesGenericas(DeclaracaoGenericaFiltroData filtro);
        DeclaracaoGenericaDadosGeraisData BuscarDeclaracaoGenerica(long seqDocumento);
    }
}
