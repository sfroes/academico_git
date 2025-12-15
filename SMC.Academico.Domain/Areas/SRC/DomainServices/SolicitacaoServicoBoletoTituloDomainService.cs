using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Financeiro.BLT.Common;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Financeiro.ServiceContract.BLT.Data;
using SMC.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Util;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoServicoBoletoTituloDomainService : AcademicoContextDomain<SolicitacaoServicoBoletoTitulo>
    {
        #region Serviços

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return Create<IIntegracaoFinanceiroService>(); }
        }

        #endregion Serviços

        #region [ DomainServices ]

        private SolicitacaoServicoBoletoTaxaDomainService SolicitacaoServicoBoletoTaxaDomainService => Create<SolicitacaoServicoBoletoTaxaDomainService>();

        #endregion

        public List<TaxasSolicitacaoVO> BuscarTaxasTitulosPorSolicitacao(long seqSolicitacaoServico)
        {
            List<TaxasSolicitacaoVO> listaRetorno = new List<TaxasSolicitacaoVO>();
            var specTitulo = new SolicitacaoServicoBoletoTituloFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var titulosSolicitacaoServico = this.SearchBySpecification(specTitulo).Where(a => !a.DataCancelamento.HasValue).OrderBy(a => a.DataInclusao).ToList();
            var specTaxa = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(specTaxa).OrderBy(a => a.DataInclusao).ToList();

            for (int i = 0; i < titulosSolicitacaoServico.Count(); i++)
            {
                TaxasSolicitacaoVO taxaSolicitacao = new TaxasSolicitacaoVO();
                taxaSolicitacao.SeqTituloGra = titulosSolicitacaoServico[i].SeqTituloGra;

                /*TRECHO COMENTADO PORQUE A PROCEDURE CHAMADA NO MÉTODO BUSCARBOLETOCREI RETORNA ERRO SE O BOLETO ESTIVER 
                DIFERENTE DE ABERTO, ENTÃO NÃO É POSSÍVEL REALIZAR A CONSULTA*/

                #region CONSULTAR BOLETO NO FINANCEIRO 

                //var boletoCrei = this.IntegracaoFinanceiroService.BuscarBoletoCrei(new BoletoFiltroData()
                //{
                //    SeqTitulo = titulo.SeqTituloGra,
                //    Crei = true,
                //    Sistema = SistemaBoleto.SGA
                //});

                //var taxasBoleto = boletoCrei.Taxas.TransformList<TaxaCreiData>();
                //taxaSolicitacao.DescricaoTaxa = taxasBoleto.FirstOrDefault()?.Descricao;

                #endregion

                /*SOLUÇÃO TEMPORÁRIA POIS POR ENQUANTO SERÁ APENAS UMA TAXA PARA CADA SOLICITAÇÃO, E HOJE NÃO TEM COMO 
                 RELACIONAR A TAXA AO BOLETO, FUTURAMENTE NA TABELA SOLICITACAO SERVICO BOLETO TITULO PRECISARÁ DE UM CAMPO
                 PARA SALVAR O SEQTAXA DO TITULO GERADO, ISSO ERA CONSULTADO NO MÉTODO BUSCARBOLETOCREI, MAS DÁ ERRO 
                 SE O BOLETO NÃO ESTIVER ABERTO, ENTÃO NÃO CONSEGUIMOS CONSULTAR*/

                var taxaBoleto = taxasSolicitacaoServico[i] != null ? taxasSolicitacaoServico[i] : taxasSolicitacaoServico.FirstOrDefault();
                var taxa = this.IntegracaoFinanceiroService.BuscarTaxa(taxaBoleto.SeqTaxaGra);
                taxaSolicitacao.SeqTaxaGra = taxaBoleto.SeqTaxaGra;
                taxaSolicitacao.DescricaoTaxa = taxa.DescricaoTaxa;

                taxaSolicitacao.HabilitarBotaoEmitirBoleto = true;
                taxaSolicitacao.MensagemBotaoEmitirBoleto = string.Empty;

                var dadosTitulo = this.IntegracaoFinanceiroService.BuscarDadosTitulo(titulosSolicitacaoServico[i].SeqTituloGra);

                //SE O TIPO DA SITUAÇÃO ATUAL DO TÍTULO FOR DIFERENTE DE 'EM ABERTO', DESABILITAR O COMANDO 'EMITIR BOLETO'
                if (dadosTitulo != null && dadosTitulo.SituacaoTitulo != SituacaoTitulo.EmAberto)
                {
                    taxaSolicitacao.HabilitarBotaoEmitirBoleto = false;
                    taxaSolicitacao.MensagemBotaoEmitirBoleto = string.Format(MessagesResource.MSG_InstructionEmitirBoletoDiferenteEmAberto, SMCEnumHelper.GetDescription(dadosTitulo.SituacaoTitulo));
                }

                listaRetorno.Add(taxaSolicitacao);
            }

            /*PODE TER INSERIDO TAXAS NA SOLICITAÇÃO MAS NÃO TER CONFIRMADO E GERADO OS TÍTULOS*/
            if (!titulosSolicitacaoServico.Any() && taxasSolicitacaoServico.Any())
            {
                foreach (var taxaSolicitacaoServico in taxasSolicitacaoServico)
                {
                    TaxasSolicitacaoVO taxaSolicitacao = new TaxasSolicitacaoVO();
                    taxaSolicitacao.SeqTituloGra = 0;

                    var taxa = this.IntegracaoFinanceiroService.BuscarTaxa(taxaSolicitacaoServico.SeqTaxaGra);
                    taxaSolicitacao.SeqTaxaGra = taxaSolicitacaoServico.SeqTaxaGra;
                    taxaSolicitacao.DescricaoTaxa = taxa.DescricaoTaxa;

                    taxaSolicitacao.HabilitarBotaoEmitirBoleto = false;
                    taxaSolicitacao.MensagemBotaoEmitirBoleto = MessagesResource.MSG_InstructionEmitirBoletoSemTitulo;

                    /*SOLUÇÃO TEMPORÁRIA POIS POR ENQUANTO SERÁ APENAS UMA TAXA PARA CADA SOLICITAÇÃO, E HOJE NÃO TEM COMO 
                    RELACIONAR A TAXA AO BOLETO, FUTURAMENTE NA TABELA SOLICITACAO SERVICO BOLETO TITULO PRECISARÁ DE UM CAMPO
                    PARA SALVAR O SEQTAXA DO TITULO GERADO, ISSO ERA CONSULTADO NO MÉTODO BUSCARBOLETOCREI, MAS DÁ ERRO 
                    SE O BOLETO NÃO ESTIVER ABERTO, ENTÃO NÃO CONSEGUIMOS CONSULTAR*/

                    /*SOLUÇÃO PARA QUANDO A SOLICITAÇÃO ESTIVER CANCELADA, NÃO TIVER BOLETO OU TODOS OS BOLETOS CANCELADOS, PARA NÃO 
                    PERMITIR SUA EMISSÃO*/
                    var tituloSolicitacaoServico = this.SearchBySpecification(specTitulo).OrderByDescending(a => a.DataGeracaoTitulo).FirstOrDefault();

                    if (tituloSolicitacaoServico != null)
                    {
                        var dadosTitulo = this.IntegracaoFinanceiroService.BuscarDadosTitulo(tituloSolicitacaoServico.SeqTituloGra);

                        //SE O TIPO DA SITUAÇÃO ATUAL DO TÍTULO FOR DIFERENTE DE 'EM ABERTO', DESABILITAR O COMANDO 'EMITIR BOLETO'
                        if (dadosTitulo != null && dadosTitulo.SituacaoTitulo != SituacaoTitulo.EmAberto)
                            taxaSolicitacao.MensagemBotaoEmitirBoleto = string.Format(MessagesResource.MSG_InstructionEmitirBoletoDiferenteEmAberto, SMCEnumHelper.GetDescription(dadosTitulo.SituacaoTitulo));
                    }

                    listaRetorno.Add(taxaSolicitacao);
                }
            }

            return listaRetorno.OrderBy(o => o.DescricaoTaxa).ToList();
        }

        public List<TitulosSolicitacaoVO> BuscarTitulosPorSolicitacao(long seqSolicitacaoServico)
        {
            List<TitulosSolicitacaoVO> listaRetorno = new List<TitulosSolicitacaoVO>();
            var spec = new SolicitacaoServicoBoletoTituloFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var titulosSolicitacaoServico = this.SearchBySpecification(spec).ToList();

            foreach (var titulo in titulosSolicitacaoServico)
            {
                var dadosTitulo = this.IntegracaoFinanceiroService.BuscarDadosTitulo(titulo.SeqTituloGra);

                TitulosSolicitacaoVO tituloSolicitacao = titulo.Transform<TitulosSolicitacaoVO>();
                tituloSolicitacao.SituacaoTitulo = dadosTitulo.SituacaoTitulo;

                listaRetorno.Add(tituloSolicitacao);
            }

            return listaRetorno.OrderByDescending(o => o.DataGeracaoTitulo).ToList();
        }
    }
}
