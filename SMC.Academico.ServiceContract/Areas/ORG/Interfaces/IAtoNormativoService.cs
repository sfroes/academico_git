using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IAtoNormativoService : ISMCService
    {
        SMCPagerData<AtoNormativoListarData> BuscarAtosNormativos(AtoNormativoFiltroData filtros);

        AtoNormativoData BuscarAtoNormativo(long seq);

        long SalvarAtoNormativo(AtoNormativoData modelo);

        SMCPagerData<AssociacaoEntidadeListarData> BuscarAssociacoesEntidades(AssociacaoEntidadeFiltroData filtros);

        AssociacaoEntidadeData BuscarAssociacaoEntidades(long seq);

        long SalvarAssociacaoEntidades(AssociacaoEntidadeData modelo);

        void ExcluirAssociacaoEntidade(long seq);

        /// <summary>
        /// Retornar informações do último ato normativo em vigor para uma entidade determinada
        /// </summary>
        /// <param name="seqEntidade">Detalhes do ato normativo</param>
        /// <returns></returns>
        [OperationContract]
        DadosAtoNormativoData BuscarUltimoAtoNormativoVigente(long seqEntidade);
    }
}
