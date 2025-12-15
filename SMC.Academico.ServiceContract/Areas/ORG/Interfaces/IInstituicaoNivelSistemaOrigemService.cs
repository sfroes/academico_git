using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoNivelSistemaOrigemService : ISMCService
    {
        List<SMCDatasourceItem> BuscarConfiguracaoDiplomaGADSelect();

        List<SMCDatasourceItem> BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(long seqInstituicaoNivel, UsoSistemaOrigem? usoSistemaOrigem = null);

        List<SMCDatasourceItem<string>> BuscarSistemaOrigemGADSelect();

        SMCPagerData<InstituicaoNivelSistemaOrigemData> BuscarInstituicaoNivelSistemaOrigemGAD(InstituicaoNivelSistemaOrigemFiltroData filtros);

        long SalvarInstituicaoNivelSIstemaOrigemGAD(InstituicaoNivelSistemaOrigemData instituicaoNivelSistemaOrigemData);

        List<SMCDatasourceItem> BuscarTipoUsoNivelEnsino(long seqNivelEnsino);

        List<SMCDatasourceItem> BuscarTiposUsoNiveisEnsino();

    }
}