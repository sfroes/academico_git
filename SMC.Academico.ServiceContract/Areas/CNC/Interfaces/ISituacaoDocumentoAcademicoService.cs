using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ISituacaoDocumentoAcademicoService : ISMCService
    {
        SituacaoDocumentoAcademicoData BuscarSituacaoDocumentoAcademicoPorSituacaoAtual(long seqDocumentoAcademicoHistoricoSituacaoAtual);

        List<SMCDatasourceItem> BuscarSituacoesDocumentoAcademicoSelect();

        SituacaoDocumentoAcademicoData BuscarSituacaoDocumentoAcademico(long seq);

        long Salvar(SituacaoDocumentoAcademicoData modelo);

        List<SMCDatasourceItem> BuscarSituacoesDocumentoAcademicoPorGrupoSelect(List<GrupoDocumentoAcademico> listaGrupoDocumentoAcademico);

        SMCPagerData<SituacaoDocumentoAcademicoListarData> BuscarSituacaoDocumentoAcademicoFiltro(SituacaoDocumentoAcademicoFiltroData filtro);
    }
}
