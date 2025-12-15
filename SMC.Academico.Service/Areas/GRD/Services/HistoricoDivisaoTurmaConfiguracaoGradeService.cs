using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.GRD.Services
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeService : SMCServiceBase, IHistoricoDivisaoTurmaConfiguracaoGradeService
    {
        #region [ DomainService ]

        private HistoricoDivisaoTurmaConfiguracaoGradeDomainService HistoricoDivisaoTurmaConfiguracaoGradeDomainService => Create<HistoricoDivisaoTurmaConfiguracaoGradeDomainService>();

        #endregion [ DomainService ]

        public string BuscarCabecalhoConfiguracaoGrade(long seqDivisaoTurma)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarCabecalhoConfiguracaoGrade(seqDivisaoTurma);
        }

        public HistoricoDivisaoTurmaConfiguracaoGradeData BuscarNovaConfiguracaoGrade(long seqDivisaoTurma)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarNovaConfiguracaoGrade(seqDivisaoTurma).Transform<HistoricoDivisaoTurmaConfiguracaoGradeData>();
        }


        public HistoricoDivisaoTurmaConfiguracaoGradeData BuscarHistoricoDivisaoConfiguracaoGrade(long seq)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarHistoricoDivisaoConfiguracaoGrade(seq).Transform<HistoricoDivisaoTurmaConfiguracaoGradeData>();
        }

        public SMCPagerData<HistoricoDivisaoTurmaConfiguracaoGradeListarData> BuscarHistoricosDivisaoConfiguracaoGrade(HistoricoDivisaoTurmaConfiguracaoGradeFiltroData filtro)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarHistoricosDivisaoConfiguracaoGrade(filtro.Transform<HistoricoDivisaoTurmaConfiguracaoGradeFiltroVO>()).Transform<SMCPagerData<HistoricoDivisaoTurmaConfiguracaoGradeListarData>>();
        }

        public List<SMCDatasourceItem> BuscarTiposPagamentoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarTiposPagamentoSelect(tipoDistribuicaoAula);
        }

        public List<SMCDatasourceItem> BuscarTiposPulaFeriadoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.BuscarTiposPulaFeriadoSelect(tipoDistribuicaoAula);
        }

        public long Salvar(HistoricoDivisaoTurmaConfiguracaoGradeData modelo)
        {
            return this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.SalvarHistoricoDivisaoConfiguracaoGrade(modelo.Transform<HistoricoDivisaoTurmaConfiguracaoGradeVO>());
        }

        public void Excluir(long seq)
        {
            this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.ExcluirHistoricoDivisaoConfiguracaoGrade(seq);
        }
    }
}
