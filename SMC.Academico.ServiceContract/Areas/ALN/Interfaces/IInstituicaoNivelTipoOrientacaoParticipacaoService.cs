using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IInstituicaoNivelTipoOrientacaoParticipacaoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoComObrigatoriedadeSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros);

        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros);

        InstituicaoNivelTipoOrientacaoParticipacaoData BuscarInstituicaoNivelTipoOrientacaoParticipacao(long seq);

        List<InstituicaoNivelTipoOrientacaoParticipacaoData> BuscarInstituicaoNivelTipoOrientacaoParticipacoes(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros);

        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoOrigemSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros);
    }
}