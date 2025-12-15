using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IServicoService : ISMCService
    {
        SMCPagerData<ServicoListarData> BuscarServicos(ServicoFiltroData filtro);

        List<SMCDatasourceItem> BuscarTiposTransacao();

        List<SMCDatasourceItem> BuscarTaxasAcademicas();

        List<SMCDatasourceItem> BuscarBancosAgencias();

        List<ValoresTaxaData> ConsultarValoresTaxas(List<int> seqsTaxas);

        List<ConsultarTaxasPorNucleoListarData> ConsultarTaxasPorNucleo(long seqServico);

        List<SMCDatasourceItem> BuscarServicosSelect();

        List<SMCDatasourceItem> BuscarServicosPorAlunoSelect(ServicoPorAlunoFiltroData filtro);

        List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelEnsinoTipoServicoSelect(long seqTipoServico);

        List<SMCDatasourceItem> BuscarServicosPorTipoServicoSelect(long seqTipoServico);

        List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelEnsinoSelect();

        List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelDoContratoSelect(long seqContrato);

        ServicoData BuscarServico(long seqServico);

        List<SMCDatasourceItem> BuscarServicosSelect(ServicoFiltroData filtros);

        List<SMCDatasourceItem> BuscarServicosGeraSolicitacaoTipoDocumentoSelect(long seqInstituicaoEnsino);

        List<SMCDatasourceItem> BuscarEtapasDoServicoSelect(long seqServico);

        /// <summary>
        /// Busca os serviços conforme o Ciclo Letivo
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Lista de serviços</returns>
        List<SMCDatasourceItem> BuscarServicosPorCicloLetivoSelect(long seqCicloLetivo);

        List<DadosRelatorioServicoCicloLetivoData> BuscarDadosRelatorioServicoCicloLetivo(RelatorioServicoCicloLetivoFiltroData filtro);

        List<SMCDatasourceItem> BuscarTemplatesSGFPorTipoServicoSelect(long seqTipoServico);

        List<SMCDatasourceItem> BuscarTiposEmissaoTaxa(OrigemSolicitacaoServico origemSolicitacaoServico);

        List<SMCDatasourceItem> BuscarTiposEmissaoCobrancaTaxa();

        long Salvar(ServicoData modelo);

        void ValidarModelo(ServicoData modelo);

        void ValidarCampoLiberarTrabalhoAcademico(ServicoData modelo);

        (bool ExibirAssert, string MensagemAssertTaxasNaoParametrizadas) ValidarAssertProximo(ServicoData modelo);

        void Excluir(long seq);
    }
}