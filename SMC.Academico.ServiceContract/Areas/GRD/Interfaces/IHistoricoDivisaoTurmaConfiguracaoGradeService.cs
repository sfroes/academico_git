using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Interfaces
{
    public interface IHistoricoDivisaoTurmaConfiguracaoGradeService : ISMCService
    {
        string BuscarCabecalhoConfiguracaoGrade(long seqDivisaoTurma);

        HistoricoDivisaoTurmaConfiguracaoGradeData BuscarNovaConfiguracaoGrade(long seqDivisaoTurma);

        HistoricoDivisaoTurmaConfiguracaoGradeData BuscarHistoricoDivisaoConfiguracaoGrade(long seq);

        SMCPagerData<HistoricoDivisaoTurmaConfiguracaoGradeListarData> BuscarHistoricosDivisaoConfiguracaoGrade(HistoricoDivisaoTurmaConfiguracaoGradeFiltroData filtro);

        List<SMCDatasourceItem> BuscarTiposPagamentoSelect(TipoDistribuicaoAula tipoDistribuicaoAula);

        List<SMCDatasourceItem> BuscarTiposPulaFeriadoSelect(TipoDistribuicaoAula tipoDistribuicaoAula);

        long Salvar(HistoricoDivisaoTurmaConfiguracaoGradeData modelo);

        void Excluir(long seq);
    }
}
