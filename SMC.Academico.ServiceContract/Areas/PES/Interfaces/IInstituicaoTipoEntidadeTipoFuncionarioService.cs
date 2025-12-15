using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IInstituicaoTipoEntidadeTipoFuncionarioService : ISMCService
    {
        long SalvarInstituicaoTipoEntidadeTipoFuncionario(InstituicaoTipoEntidadeTipoFuncionarioData instituicaoTipoEntidadeTipoFuncionario);

        List<SMCDatasourceItem> BuscarTipoEntidadePorTipoFuncionario(long seqTipoFuncionario);

        bool ListaTiposEntidadePorTipoFuncionario(long seqTipoFuncionario);
    }
}
