using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data.ConfiguracaoAvaliacaoPpa;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class ConfiguracaoAvaliacaoPpaService : SMCServiceBase, IConfiguracaoAvaliacaoPpaService
    {
        #region [ DomainServices ]
        ConfiguracaoAvaliacaoPpaDomainService ConfiguracaoAvaliacaoPpaDomainService => this.Create<ConfiguracaoAvaliacaoPpaDomainService>();

        #endregion [ DomainServices ]

        #region [Services]

        private IAmostraService AmostraService => Create<IAmostraService>();

        #endregion

        //utilizado na listagem de pesquisa
        public SMCPagerData<ConfiguracaoAvaliacaoPpaListaData> BuscarAvaliacoes(ConfiguracaoAvaliacaoPpaFiltroData filtro)
        {
            var spec = filtro.Transform<ConfiguracaoAvaliacaoPpaFilterSpecification>();
            var lista = ConfiguracaoAvaliacaoPpaDomainService.BuscarAvaliacoes(spec);
            return lista.Transform<SMCPagerData<ConfiguracaoAvaliacaoPpaListaData>>();
        }

        #region [Cadastro Avaliacao PPA]

        /// <summary>
        /// Metodo acionado pelo dynamic model de avaliação para carregar o select de avaliacoes ativas
        /// </summary>
        /// <returns>Retorna lista de datasource com descricao concatenada com codigo</returns>
        public List<SMCDatasourceItem> BuscarAvaliacoesIntitucionaisSelect()
        {
            var lista = AmostraService.BuscarDescricoesAvaliacoesAtivas();
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.Seq} - {item.Descricao}" });
            }
            return retorno;
        }

        /// <summary>
        /// Buscar Origens de amostras ativas
        /// </summary>
        /// <param name="AmostraAtiva">Origem amostra ativa</param>
        /// <returns>Retorna data source com a lista de amostras ativas</returns>
        public List<SMCDatasourceItem> BuscarOrigemAmostraPpaSelect(bool AmostraAtiva)
        {
            var lista = AmostraService.BuscarOrigensAmostras(null, AmostraAtiva);
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.Seq} - {item.Descricao}" });
            }
            return retorno;
        }

        /// <summary>
        /// Popular Select de Tipos de instrumentos no cadastro de avaliação.
        /// </summary>
        /// <returns>Retona codigo e descrição instrumentos.</returns>
        public List<SMCDatasourceItem> BuscarTiposInstrumentosSelect()
        {
            var lista = AmostraService.BuscarTiposInstrumentos(null);
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.Seq} - {item.Descricao}" });
            }
            return retorno;
        }

        /// <summary>
        /// Popular Select de Tipos de instrumentos no cadastro de avaliação.
        /// </summary>
        /// <returns>Retona codigo e descrição instrumentos.</returns>
        public List<SMCDatasourceItem> BuscarInstrumentosSelect(int? codigoAvaliacao)
        {
            if (codigoAvaliacao == null)
            {
                return new List<SMCDatasourceItem>();
            }
            var lista = AmostraService.BuscarInstrumentos(codigoAvaliacao);
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.Seq} - {item.Descricao}" });
            }
            return retorno;
        }
        public long SalvarConfiguracaoAvaliacao(ConfiguracaoAvaliacaoPpaData configuracao)
        {
            return ConfiguracaoAvaliacaoPpaDomainService.SalvarConfiguracaoAvaliacao(configuracao.Transform<ConfiguracaoAvaliacaoPpaVO>());
        }

        public ConfiguracaoAvaliacaoPpaCabecalhoData BuscarCabecalhoConfiguracaoAvaliacaoPpa(long seq)
        {
            var result = this.ConfiguracaoAvaliacaoPpaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoAvaliacaoPpa>(seq));

            var data = result.Transform<ConfiguracaoAvaliacaoPpaCabecalhoData>();
            
            return data;
        }

        public ConfiguracaoAvaliacaoPpaData BuscarConfiguracaoAvaliacao(long seq)
        {
            var spec = new ConfiguracaoAvaliacaoPpaFilterSpecification() { Seq = seq };

            var configuracao = ConfiguracaoAvaliacaoPpaDomainService.SearchProjectionByKey(spec, x =>
            new ConfiguracaoAvaliacaoPpaData
            {
                Seq = x.Seq,
                NomeEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                SeqNivelEnsino = x.SeqNivelEnsino,
                Descricao = x.Descricao,
                TipoAvaliacaoPpa = x.TipoAvaliacaoPpa,
                DataInicioVigencia = x.DataInicioVigencia,
                DataFimVigencia = x.DataFimVigencia,
                DataLimiteRespostas = x.DataLimiteRespostas,
                CodigoAvaliacaoPpa = x.CodigoAvaliacaoPpa,
                CodigoOrigemPpa = x.CodigoOrigemPpa,
                SeqTipoInstrumentoPpa = x.SeqTipoInstrumentoPpa,
                CodigoInstrumentoPpa = x.CodigoInstrumentoPpa,
                CodigoAplicacaoQuestionarioSgq = x.CodigoAplicacaoQuestionarioSgq,
                SeqEspecieAvaliadorPpa = x.SeqEspecieAvaliadorPpa,
                CargaRealizada = x.CargaRealizada
            });

            return configuracao;
        }

        public void SalvarAlteracaoDataLimiteResposta(long seq, DateTime novaData)
        {
            this.ConfiguracaoAvaliacaoPpaDomainService.AlterarDataLimiteResposta(seq, novaData);
        }

        public void ExcluirConfiguracaoAvaliacao(long seq)
        {
            this.ConfiguracaoAvaliacaoPpaDomainService.ExcluirConfiguracaoAvaliacaoPpa(seq);
        }
        #endregion
    }
}
