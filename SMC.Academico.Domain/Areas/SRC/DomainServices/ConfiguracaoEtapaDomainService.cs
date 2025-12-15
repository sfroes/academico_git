using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ConfiguracaoEtapaDomainService : AcademicoContextDomain<ConfiguracaoEtapa>
    {
        #region DomainServices

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService
        {
            get { return this.Create<SolicitacaoServicoEtapaDomainService>(); }
        }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return this.Create<ProcessoEtapaDomainService>(); }
        }

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaPaginaDomainService>(); }
        }

        private ProcessoDomainService ProcessoDomainService
        {
            get { return this.Create<ProcessoDomainService>(); }
        }

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService
        {
            get { return this.Create<DocumentoRequeridoDomainService>(); }
        }

        #endregion DomainServices

        #region Services

        private IPaginaService PaginaService
        {
            get { return this.Create<IPaginaService>(); }
        }

        #endregion

        public CabecalhoConfiguracaoEtapaVO BuscarCabecalhoConfiguracaoEtapa(long seqConfiguracaoEtapa)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), p => new CabecalhoConfiguracaoEtapaVO
            {
                SeqProcesso = p.ProcessoEtapa.SeqProcesso,
                DescricaoProcesso = p.ProcessoEtapa.Processo.Descricao,
                SeqEtapaSgf = p.ProcessoEtapa.SeqEtapaSgf,
                DescricaoEtapaSgf = p.ProcessoEtapa.DescricaoEtapa,
                SituacaoEtapa = p.ProcessoEtapa.SituacaoEtapa,
                DescricaoConfiguracaoEtapa = p.Descricao
            });

            return registro;
        }

        public SMCPagerData<ConfiguracaoEtapaListarVO> BuscarConfiguracoesEtapa(ConfiguracaoEtapaFiltroVO filtro)
        {
            var spec = filtro.Transform<ProcessoEtapaFilterSpecification>();

            var lista = this.ProcessoEtapaDomainService.SearchProjectionBySpecification(spec, a => new ConfiguracaoEtapaListarVO()
            {
                SeqProcesso = a.SeqProcesso,
                SeqProcessoEtapa = a.Seq,
                DescricaoEtapa = a.DescricaoEtapa,
                SituacaoEtapa = a.SituacaoEtapa,
                ConfiguracoesEtapa = a.ConfiguracoesEtapa.Select(b => new ConfiguracaoEtapaListarItemVO()
                {
                    Seq = b.Seq,
                    SeqConfiguracaoProcesso = b.SeqConfiguracaoProcesso,
                    DescricaoConfiguracaoProcesso = b.ConfiguracaoProcesso.Descricao,
                    SeqConfiguracaoEtapa = b.Seq,
                    DescricaoConfiguracaoEtapa = b.Descricao,
                    SeqProcessoEtapa = a.Seq,
                    SeqProcesso = a.SeqProcesso,
                    SituacaoEtapa = a.SituacaoEtapa,
                    PossuiDocumentosRequeridos = b.DocumentosRequeridos.Any()
                }).OrderBy(c => c.DescricaoConfiguracaoProcesso).ThenBy(o => o.DescricaoConfiguracaoEtapa).ToList()

            }, out int total).ToList();

            lista.SelectMany(a => a.ConfiguracoesEtapa).ToList().ForEach(a => a.SolicitacaoAssociada = this.SolicitacaoServicoEtapaDomainService.Count(new SolicitacaoServicoEtapaFilterSpecification() { SeqConfiguracaoEtapa = a.Seq }) > 0);

            return new SMCPagerData<ConfiguracaoEtapaListarVO>(lista, 1);
        }

        public List<SMCDatasourceItem> BuscarPaginasNaoCriadas(long seqConfiguracaoEtapa)
        {
            //Recuperar páginas existentes para a configuração
            var dados = this.SearchProjectionByKey(
                new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa),
                x => new
                {
                    Paginas = x.ConfiguracoesPagina.Select(p => p.SeqPaginaEtapaSgf).Distinct(),
                    SeqEtapaSgf = x.ProcessoEtapa.SeqEtapaSgf,
                });

            //Buscar no SGF as páginas que não estão criadas
            return this.PaginaService.BuscarPaginasEtapaForaIntervaloKeyValue(dados.SeqEtapaSgf, dados.Paginas.ToArray());
        }

        public long Salvar(ConfiguracaoEtapaVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<ConfiguracaoEtapa>();

            if (modelo.Seq == 0)
            {
                var listaPaginasEtapa = new List<ConfiguracaoEtapaPagina>();
                var processoEtapa = this.ProcessoEtapaDomainService.BuscarProcessoEtapa(modelo.SeqProcessoEtapa);
                var etapasPagina = this.PaginaService.BuscarPaginasCompletasPorEtapa(processoEtapa.SeqEtapaSgf);

                foreach (var etapaPagina in etapasPagina)
                {
                    ConfiguracaoEtapaPagina configuracaoEtapaPagina = new ConfiguracaoEtapaPagina()
                    {
                        SeqPaginaEtapaSgf = etapaPagina.Seq,
                        TokenPagina = etapaPagina.Pagina.Token,
                        Ordem = (short)(etapaPagina.Ordem * 10),
                        TituloPagina = etapaPagina.Pagina.Titulo,
                        SeqFormulario = etapaPagina.SeqFormulario,
                        SeqVisaoFormulario = null
                    };

                    var listaTextosSecaoPagina = new List<TextoSecaoPagina>();

                    foreach (var secaoPagina in etapaPagina.Pagina.Secoes.Where(x => x.TipoSecaoPagina == TipoSecaoPagina.Texto))
                    {
                        TextoSecaoPagina textoSecaoPagina = new TextoSecaoPagina
                        {
                            SeqSecaoPaginaSgf = secaoPagina.Seq,
                            TokenSecao = secaoPagina.Token,
                            Texto = secaoPagina.TextoPadrao
                        };

                        listaTextosSecaoPagina.Add(textoSecaoPagina);
                    }

                    configuracaoEtapaPagina.TextosSecao = listaTextosSecaoPagina;
                    listaPaginasEtapa.Add(configuracaoEtapaPagina);
                }

                dominio.ConfiguracoesPagina = listaPaginasEtapa;
            }
            else
            {
                //GARANTINDO QUE NA EDIÇÃO NÃO IRÁ REMOVER AS CONFIGURAÇÕES DE PÁGINA E SEÇÃO
                dominio.ConfiguracoesPagina = null;
            }

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(ConfiguracaoEtapaVO modelo)
        {
            var configuracaoEtapaPorConfiguracaoProcesso = this.SearchBySpecification(new ConfiguracaoEtapaFilterSpecification() { SeqProcessoEtapa = modelo.SeqProcessoEtapa, SeqConfiguracaoProcesso = modelo.SeqConfiguracaoProcesso }).ToList();

            if (configuracaoEtapaPorConfiguracaoProcesso.Any(a => a.Seq != modelo.Seq))
                throw new ConfiguracaoEtapaConfiguracaoProcessoJaAssociadaException();
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seq));
                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }
    }
}