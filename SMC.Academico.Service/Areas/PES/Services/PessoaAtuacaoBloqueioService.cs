using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaAtuacaoBloqueioService : SMCServiceBase, IPessoaAtuacaoBloqueioService
    {
        #region [ DataSources ]

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService
        {
            get { return this.Create<PessoaAtuacaoBloqueioDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ DataSources ]

        public List<PessoaAtuacaoBloqueioData> BuscarPessoaAtuacaoBloqueios(long seqPessoaAtuacao, long seqConfiguracaoEtapa, bool bloqueioFimEtapa)
        {
            return PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(seqPessoaAtuacao, seqConfiguracaoEtapa, bloqueioFimEtapa).TransformList<PessoaAtuacaoBloqueioData>();
        }

        public SMCPagerData<PessoaAtuacaoBloqueioListaData> BuscarPessoasAtuacoesBloqueios(PessoaAtuacaoBloqueioFiltroData filtros)
        {
            int total = 0;

            var spec = filtros.Transform<PessoaAtuacaoBloqueioFilterSpecification>();

            if (spec?.SeqMotivoBloqueio?.Count == 0)
                spec.SeqMotivoBloqueio = null;

            if (spec?.SeqTipoBloqueio?.Count == 0)
                spec.SeqTipoBloqueio = null;

            var pessoaAtuacaoBloqueio = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, out total,
                                                 IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_Pessoa |
                                                 IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_DadosPessoais |
                                                 IncludesPessoaAtuacaoBloqueio.MotivoBloqueio |
                                                 IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio |
                                                 IncludesPessoaAtuacaoBloqueio.Comprovantes_ArquivoAnexado)
                                            .ToList();

            var result = pessoaAtuacaoBloqueio.GroupBy(x => x.SeqPessoaAtuacao).Select(y =>
            {
                var pessoa = y.First().Transform<PessoaAtuacaoBloqueioListaData>();
                pessoa.Bloqueios = y.TransformList<PessoaAtuacaoBloqueioDetalheData>();

                return pessoa;
            }).ToList();

            result.SMCForEach(f => f.Bloqueios
                  .SMCForEach(sf => sf.HabilitaBotaoDesbloqueio = !string.IsNullOrEmpty(sf.TokenPermissaoDesbloqueio) && SMCSecurityHelper.Authorize(sf.TokenPermissaoDesbloqueio)));

            return new SMCPagerData<PessoaAtuacaoBloqueioListaData>(result, total);
        }

        public PessoaAtuacaoBloqueioData PreencherModeloInserirPessoaAtuacaoBloqueio()
        {
            return new PessoaAtuacaoBloqueioData()
            {
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                TipoDesbloqueio = TipoDesbloqueio.Nenhum,
                ResponsavelBloqueio = SMCContext.User.SMCGetNome(),
                DataCriacao = DateTime.Now.Date,
                ExibirItensBloqueio = false
            };
        }

        public PessoaAtuacaoBloqueioData PreencherModeloAlterarPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio)
        {
            var result = this.PessoaAtuacaoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueio>(seqPessoaAtuacaoBloqueio),
                                IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio | IncludesPessoaAtuacaoBloqueio.Comprovantes_ArquivoAnexado | IncludesPessoaAtuacaoBloqueio.Itens);

            var data = result.Transform<PessoaAtuacaoBloqueioData>();

            foreach (var itemData in data.Comprovantes)
            {
                if (itemData.ArquivoAnexado != null)
                {
                    var arquivoAnexadoOrigem = result.Comprovantes.FirstOrDefault(a => a.SeqArquivoAnexado == itemData.SeqArquivoAnexado).ArquivoAnexado;
                    itemData.ArquivoAnexado.GuidFile = arquivoAnexadoOrigem.UidArquivo.ToString();
                }
            }

            ////Caso motivo bloqueio flag obrigatório anexo desbloqueio os comprovantes seram opcionais
            //data.MotivoComprovanteOpcional = result.MotivoBloqueio.ObrigatorioAnexoDesbloqueio;
            //if (!result.MotivoBloqueio.ObrigatorioAnexoDesbloqueio)
            //{
            //    data.ComprovantesOpcional = data.Comprovantes;
            //}

            data.ResponsavelBloqueio = result.UsuarioInclusao;
            data.DataCriacao = result.DataInclusao;

            return data;
        }

        public PessoaAtuacaoBloqueioDesbloqueioData PreencherModeloDesbloquearPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio)
        {
            var result = this.PessoaAtuacaoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueio>(seqPessoaAtuacaoBloqueio),
                                IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio | IncludesPessoaAtuacaoBloqueio.Comprovantes_ArquivoAnexado | IncludesPessoaAtuacaoBloqueio.Itens);

            var data = result.Transform<PessoaAtuacaoBloqueioDesbloqueioData>();

            foreach (var itemData in data.Comprovantes)
            {
                if (itemData.ArquivoAnexado != null)
                {
                    var arquivoAnexadoOrigem = result.Comprovantes.FirstOrDefault(a => a.SeqArquivoAnexado == itemData.SeqArquivoAnexado).ArquivoAnexado;
                    itemData.ArquivoAnexado.GuidFile = arquivoAnexadoOrigem.UidArquivo.ToString();
                }
            }

            //Caso motivo bloqueio flag obrigatório anexo desbloqueio os comprovantes seram opcionais
            data.MotivoObrigatorioAnexoDesbloqueio = result.MotivoBloqueio.ObrigatorioAnexoDesbloqueio;
            if (!result.MotivoBloqueio.ObrigatorioAnexoDesbloqueio)
            {
                data.ComprovantesOpcional = data.Comprovantes;
            }

            data.ResponsavelDesbloqueio = SMCContext.User.SMCGetNome();
            data.DataDesbloqueio = DateTime.Now.Date;

            if (!data.PermiteDesbloqueioTemporario)
                data.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
            else
                data.TipoDesbloqueio = TipoDesbloqueio.Temporario;

            foreach (var item in data.Itens)
            {
                item.BloquearSituacao = item.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado;
                item.TipoDesbloqueioDetalhe = data.TipoDesbloqueio;
            }

            return data;
        }

        public PessoaAtuacaoBloqueioCabecalhoData BuscarCabecalhoPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio)
        {
            var result = this.PessoaAtuacaoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueio>(seqPessoaAtuacaoBloqueio),
                                IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_DadosPessoais);

            var data = result.Transform<PessoaAtuacaoBloqueioCabecalhoData>();

            return data;
        }

        public long SalvarPessoaAtuacaoBloqueioDesbloqueio(PessoaAtuacaoBloqueioDesbloqueioData modelo)
        {
            //Caso motivo bloqueio flag obrigatório anexo desbloqueio os comprovantes seram opcionais
            //Desta forma faremos o mapeamento dos comprovantes opcionais para os comproventes para manter as regras.
            if (!modelo.MotivoObrigatorioAnexoDesbloqueio)
            {
                modelo.Comprovantes = modelo.ComprovantesOpcional;
            }

            var dominio = this.PessoaAtuacaoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueio>(modelo.Seq), IncludesPessoaAtuacaoBloqueio.Comprovantes | IncludesPessoaAtuacaoBloqueio.Itens);

            if (dominio.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado && dominio.TipoDesbloqueio == TipoDesbloqueio.Efetivo)
                throw new PessoaAtuacaoBloqueioDesbloqueioJaDesbloqueadoException();

            switch (modelo.TipoDesbloqueio)
            {
                case TipoDesbloqueio.Temporario:

                    dominio.DataDesbloqueioTemporario = modelo.DataDesbloqueio;
                    dominio.UsuarioDesbloqueioTemporario = modelo.ResponsavelDesbloqueio;
                    dominio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;

                    break;

                case TipoDesbloqueio.Efetivo:

                    dominio.DataDesbloqueioEfetivo = modelo.DataDesbloqueio;
                    dominio.UsuarioDesbloqueioEfetivo = modelo.ResponsavelDesbloqueio;

                    if (dominio.Itens != null && dominio.Itens.Count > 0)
                    {
                        dominio.Itens.ToList().ForEach(x =>
                        {
                            x.SituacaoBloqueio = modelo.Itens.FirstOrDefault(i => i.Seq == x.Seq)?.SituacaoBloqueio ?? SituacaoBloqueio.Bloqueado;
                        });

                        dominio.SituacaoBloqueio = dominio.Itens.Any(i => i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado) ? SituacaoBloqueio.Bloqueado : SituacaoBloqueio.Desbloqueado;
                    }
                    else
                    {
                        dominio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                    }
                    break;
            }

            dominio.JustificativaDesbloqueio = modelo.JustificativaDesbloqueio;
            dominio.TipoDesbloqueio = modelo.TipoDesbloqueio;
            dominio.Comprovantes = modelo.Comprovantes?.TransformList<PessoaAtuacaoBloqueioComprovante>();

            this.PessoaAtuacaoBloqueioDomainService.SaveEntity(dominio);

            return dominio.Seq;
        }

        public long SalvarPessoaAtuacaoBloqueio(PessoaAtuacaoBloqueioData modelo)
        {
            return this.PessoaAtuacaoBloqueioDomainService.SalvarPessoaAtuacaoBloqueio(modelo.Transform<PessoaAtuacaoBloqueioVO>());
        }

        /// <summary>
        /// Realiza a verificação de pendências de material da biblioteca de forma automática
        /// </summary>
        /// <param name="filtro">Filtros para verificação</param>
        public void VerificarBloqueioBibliotecaAutomatica(VerificarBloqueioBibliotecaSATData filtro)
        {
            PessoaAtuacaoBloqueioDomainService.VerificaBloqueioPendenciaBibliotecaAutomatica(filtro.Transform<VerificarPendenciaBibliotecaSATVO>());
        }

    }
}