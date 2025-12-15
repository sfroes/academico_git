using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ServicoService : SMCServiceBase, IServicoService
    {
        #region [ DomainService ]

        private ServicoDomainService ServicoDomainService
        {
            get { return this.Create<ServicoDomainService>(); }
        }

        private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService
        {
            get { return this.Create<InstituicaoNivelServicoDomainService>(); }
        }

        private ContratoDomainService ContratoDomainService
        {
            get { return this.Create<ContratoDomainService>(); }
        }

        #endregion [ DomainService ]

        public ServicoData BuscarServico(long seqServico)
        {
            return ServicoDomainService.BuscarServico(seqServico).Transform<ServicoData>();
        }

        public SMCPagerData<ServicoListarData> BuscarServicos(ServicoFiltroData filtro)
        {
            return this.ServicoDomainService.BuscarServicos(filtro.Transform<ServicoFiltroVO>()).Transform<SMCPagerData<ServicoListarData>>();
        }

        public List<SMCDatasourceItem> BuscarTiposTransacao()
        {
            return this.ServicoDomainService.BuscarTiposTransacao();
        }

        public List<SMCDatasourceItem> BuscarTaxasAcademicas()
        {
            return this.ServicoDomainService.BuscarTaxasAcademicas();
        }

        public List<SMCDatasourceItem> BuscarBancosAgencias()
        {
            return this.ServicoDomainService.BuscarBancosAgencias();
        }

        public List<ValoresTaxaData> ConsultarValoresTaxas(List<int> seqsTaxas)
        {
            return this.ServicoDomainService.ConsultarValoresTaxas(seqsTaxas);
        }

        public List<ConsultarTaxasPorNucleoListarData> ConsultarTaxasPorNucleo(long seqServico)
        {
            return this.ServicoDomainService.ConsultarTaxasPorNucleo(seqServico).TransformList<ConsultarTaxasPorNucleoListarData>();
        }

        public List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelEnsinoSelect()
        {
            //Busca todos os serviços respeitando o filtro global
            var seqsServicos = this.InstituicaoNivelServicoDomainService
                                   .SearchProjectionAll(x => x.Servico.Seq)
                                   .ToArray();

            var spec = new SMCContainsSpecification<Servico, long>(t => t.Seq, seqsServicos);

            spec.SetOrderBy(s => s.Descricao);

            var result = this.ServicoDomainService.SearchProjectionBySpecification(spec, t => new SMCDatasourceItem()
            {
                Seq = t.Seq,
                Descricao = t.Descricao
            });

            return result.ToList();
        }

        public List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelEnsinoTipoServicoSelect(long seqTipoServico)
        {
            return this.ServicoDomainService.BuscarServicosPorInstituicaoNivelEnsinoTipoServicoSelect(seqTipoServico);
        }

        public List<SMCDatasourceItem> BuscarServicosPorTipoServicoSelect(long seqTipoServico)
        {
            var result = this.ServicoDomainService.BuscarServicosSelect(new ServicoFiltroVO() { SeqTipoServico = seqTipoServico });

            return result;
        }

        public List<SMCDatasourceItem> BuscarEtapasDoServicoSelect(long seqServico)
        {
            return this.ServicoDomainService.BuscarEtapasDoServicoSelect(seqServico);
        }

        public List<SMCDatasourceItem> BuscarServicosSelect()
        {
            var lista = ServicoDomainService.SearchAll(i => i.Descricao).OrderBy(w => w.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            return retorno;
        }

        public List<SMCDatasourceItem> BuscarServicosPorAlunoSelect(ServicoPorAlunoFiltroData filtro)
        {
            // Faz a busca dos serviços do aluno
            return ServicoDomainService.BuscarServicosPorAlunoSelect(filtro.Transform<ServicoPorAlunoFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelDoContratoSelect(long seqContrato)
        {
            return ServicoDomainService.BuscarServicosPorInstituicaoNivelDoContratoSelect(seqContrato);
        }

        public List<SMCDatasourceItem> BuscarServicosSelect(ServicoFiltroData filtros)
        {
            return ServicoDomainService.BuscarServicosSelect(filtros.Transform<ServicoFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarServicosGeraSolicitacaoTipoDocumentoSelect(long seqInstituicaoEnsino)
        {
            return ServicoDomainService.BuscarServicosGeraSolicitacaoTipoDocumentoSelect(seqInstituicaoEnsino);
        }

        /// <summary>
        /// Busca os serviços conforme o Ciclo Letivo
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Lista de serviços</returns>
        public List<SMCDatasourceItem> BuscarServicosPorCicloLetivoSelect(long seqCicloLetivo)
        {
            return ServicoDomainService.BuscarServicosPorCicloLetivoSelect(seqCicloLetivo);
        }

        public List<DadosRelatorioServicoCicloLetivoData> BuscarDadosRelatorioServicoCicloLetivo(RelatorioServicoCicloLetivoFiltroData filtro)
        {
            return ServicoDomainService.BuscarDadosRelatorioServicoCicloLetivo(filtro.Transform<RelatorioServicoCicloLetivoFiltroVO>())
                                       .TransformList<DadosRelatorioServicoCicloLetivoData>();
        }

        public List<SMCDatasourceItem> BuscarTemplatesSGFPorTipoServicoSelect(long seqTipoServico)
        {
            return ServicoDomainService.BuscarTemplatesSGFPorTipoServicoSelect(seqTipoServico);
        }

        public List<SMCDatasourceItem> BuscarTiposEmissaoTaxa(OrigemSolicitacaoServico origemSolicitacaoServico)
        {
            return ServicoDomainService.BuscarTiposEmissaoTaxa(origemSolicitacaoServico);
        }

        public List<SMCDatasourceItem> BuscarTiposEmissaoCobrancaTaxa()
        {
            return ServicoDomainService.BuscarTiposEmissaoCobrancaTaxa();
        }

        public long Salvar(ServicoData modelo)
        {
            return ServicoDomainService.SalvarServico(modelo.Transform<ServicoVO>());
        }

        public void ValidarModelo(ServicoData modelo)
        {
            ServicoDomainService.ValidarModelo(modelo.Transform<ServicoVO>());
        }

        public void ValidarCampoLiberarTrabalhoAcademico(ServicoData modelo)
        {
            ServicoDomainService.ValidarCampoLiberarTrabalhoAcademico(modelo.Transform<ServicoVO>());
        }

        public (bool ExibirAssert, string MensagemAssertTaxasNaoParametrizadas) ValidarAssertProximo(ServicoData modelo)
        {
           return ServicoDomainService.ValidarAssertProximo(modelo.Transform<ServicoVO>());
        }

        public void Excluir(long seq)
        {
            ServicoDomainService.Excluir(seq);
        }
    }
}