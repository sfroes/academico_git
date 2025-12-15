using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoServicoFilterSpecification : SMCSpecification<SolicitacaoServico>
    {
        public long? Seq { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqServico { get; set; }

        public long? SeqPessoa { get; set; }

        public string NumeroProtocolo { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqSituacaoEtapa { get; set; }

        public List<long> SeqsSituacoesEtapa { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public long? SeqBloqueio { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public string Solicitante { get; set; }

        public List<long> SeqsGruposEscalonamento { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsIngressantes { get; set; }

        public List<long> SeqsSolicitacoesServico { get; set; }

        public List<long> SeqsServicos { get; set; }

        public long? SeqCursoOferta { get; set; }

        public bool? DisponivelParaAtendimento { get; set; }

        public bool? PossuiBloqueio { get; set; }

        public bool? PossuiUsuarioResponsavel { get; set; }

        public long? SeqUsuarioResponsavel { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public TipoFiltroCentralSolicitacao TipoFiltroCentralSolicitacao { get; set; }

        public DateTime? DataPrevistaSolucaoInicio { get; set; }

        public DateTime? DataPrevistaSolucaoFim { get; set; }

        public DateTime? DataSolucaoInicio { get; set; }

        public DateTime? DataSolucaoFim { get; set; }

        public DateTime? DataInclusaoInicio { get; set; }

        public DateTime? DataInclusaoFim { get; set; }

        public long? SeqTipoServico { get; set; }

        public CategoriaSituacao? CategoriaSituacao { get; set; }

        public List<CategoriaSituacao> CategoriasSituacoes { get; set; }

        public DateTime? DataSolicitacao { get; set; }

        public long? SeqTipoDocumento { get; set; }

        public List<OrigemSolicitacaoServico> OrigensSolicitacaoServico { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public string DescricaoLookupSolicitacao { get; set; }

        public List<string> TokensServico { get; set; }
        public string TokenServico { get; set; }
        public List<string> TokenTipoServico { get; set; }

        public long? SeqConfiguracaoProcesso { get; set; }

        public AcaoLiberacaoTrabalho? AcaoLiberacaoTrabalho { get; set; }



        public override Expression<Func<SolicitacaoServico, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => Seq.Value == x.Seq);

            AddExpression(SeqPessoa, x => SeqPessoa.Value == x.PessoaAtuacao.Pessoa.Seq);

            AddExpression(DisponivelParaAtendimento, x => x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.CentralAtendimento == DisponivelParaAtendimento);

            AddExpression(() => PossuiUsuarioResponsavel.HasValue && PossuiUsuarioResponsavel.Value, x => x.UsuariosResponsaveis.Any());

            AddExpression(SeqUsuarioResponsavel, x => x.UsuariosResponsaveis.Any(w => w.SeqUsuarioResponsavel == SeqUsuarioResponsavel));

            AddExpression(SeqGrupoEscalonamento, x => x.SeqGrupoEscalonamento == SeqGrupoEscalonamento.Value);

            AddExpression(OrigensSolicitacaoServico, x => OrigensSolicitacaoServico.Contains(x.OrigemSolicitacaoServico));

            AddExpression(TokensServico, x => TokensServico.Contains(x.ConfiguracaoProcesso.Processo.Servico.Token));
            AddExpression(TokenServico, x => x.ConfiguracaoProcesso.Processo.Servico.Token == TokenServico);
            AddExpression(TokenTipoServico, x => TokenTipoServico.Contains(x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token));

            AddExpression(SeqsEntidadesResponsaveis, x => SeqsEntidadesResponsaveis.Contains(x.SeqEntidadeResponsavel));

            switch (TipoFiltroCentralSolicitacao)
            {
                case TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao:

                    AddExpression(SeqSituacaoEtapa, x => x.SituacaoAtual.SeqSituacaoEtapaSgf == SeqSituacaoEtapa);
                    AddExpression(SeqProcessoEtapa, x => x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa == SeqProcessoEtapa);
                    AddExpression(SeqsSituacoesEtapa, x => SeqsSituacoesEtapa.Contains(x.SituacaoAtual.SeqSituacaoEtapaSgf));

                    break;

                case TipoFiltroCentralSolicitacao.SituacaoEtapaSelecionada:

                    AddExpression(SeqSituacaoEtapa, x => x.Etapas.Any(e => e.HistoricosSituacao.Any(h => h.SeqSituacaoEtapaSgf == SeqSituacaoEtapa)));
                    AddExpression(SeqsSituacoesEtapa, x => x.Etapas.Any(e => e.HistoricosSituacao.Any(h => SeqsSituacoesEtapa.Contains(h.SeqSituacaoEtapaSgf))));

                    AddExpression(SeqProcessoEtapa, x => x.Etapas.Any(e => e.ConfiguracaoEtapa.SeqProcessoEtapa == SeqProcessoEtapa));

                    break;
            }

            //Vc pode determinar a condição para aplicar o filtro
            AddExpression(() => SituacaoDocumentacao != SituacaoDocumentacao.Nenhum, x => x.SituacaoDocumentacao == SituacaoDocumentacao);

            AddExpression(SeqBloqueio, x => x.PessoaAtuacao.Bloqueios.Where(b => x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf).Escalonamento.ProcessoEtapa.ConfiguracoesEtapa.SelectMany(c => c.ConfiguracoesBloqueio.Select(cb => cb.SeqMotivoBloqueio)).Any(bb => bb == b.SeqMotivoBloqueio)).Any(b => b.SeqMotivoBloqueio == SeqBloqueio.Value));
            AddExpression(NumeroProtocolo, x => x.NumeroProtocolo.Contains(NumeroProtocolo));
            AddExpression(Solicitante, x => x.PessoaAtuacao.DadosPessoais.Nome.Contains(Solicitante) || x.PessoaAtuacao.DadosPessoais.NomeSocial.Contains(Solicitante));
            AddExpression(SeqsGruposEscalonamento, x => SeqsGruposEscalonamento.Contains(x.SeqGrupoEscalonamento.Value));
            AddExpression(SeqCicloLetivo, x => x.ConfiguracaoProcesso.Processo.SeqCicloLetivo == SeqCicloLetivo.Value);
            AddExpression(SeqCursoOferta, x => (x.PessoaAtuacao as Ingressante).Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(SeqsSolicitacoesServico, x => SeqsSolicitacoesServico.Contains(x.Seq));

            if (SeqPessoaAtuacao.HasValue && SeqsIngressantes.SMCAny())
            {
                AddExpression(x => SeqsIngressantes.Contains(x.SeqPessoaAtuacao) || x.SeqPessoaAtuacao == SeqPessoaAtuacao);
            }
            else
            {
                AddExpression(SeqPessoaAtuacao, x => x.SeqPessoaAtuacao == SeqPessoaAtuacao);
                AddExpression(SeqsIngressantes, x => SeqsIngressantes.Contains(x.SeqPessoaAtuacao));
            }

            if (DataSolucaoInicio.HasValue)
            {
                AddExpression(DataSolucaoInicio, x => x.DataInclusao >= DataInclusaoInicio.Value);
            }

            if (DataSolucaoFim.HasValue)
            {
                var dataSolucaoFim = DataSolucaoFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                AddExpression(DataSolucaoFim, x => x.DataSolucao <= dataSolucaoFim);
            }

            if (DataInclusaoInicio.HasValue)
            {
                AddExpression(DataInclusaoInicio, x => x.DataInclusao >= DataInclusaoInicio.Value);
            }

            if (DataInclusaoFim.HasValue)
            {
                var dataInclusaoFim = DataInclusaoFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                AddExpression(DataInclusaoFim, x => x.DataInclusao <= dataInclusaoFim);
            }

            if (DataPrevistaSolucaoInicio.HasValue)
            {
                AddExpression(DataPrevistaSolucaoInicio, x => x.DataPrevistaSolucao >= DataPrevistaSolucaoInicio.Value);
            }

            if (DataPrevistaSolucaoFim.HasValue)
            {
                var dataPrevistaSolucaoFim = DataPrevistaSolucaoFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                AddExpression(DataPrevistaSolucaoFim, x => x.DataPrevistaSolucao <= dataPrevistaSolucaoFim);
            }

            AddExpression(SeqTipoServico, x => x.ConfiguracaoProcesso.Processo.Servico.SeqTipoServico == SeqTipoServico);

            AddExpression(SeqServico, x => SeqServico == x.ConfiguracaoProcesso.Processo.SeqServico);
            AddExpression(SeqsServicos, x => SeqsServicos.Contains(x.ConfiguracaoProcesso.Processo.SeqServico));

            AddExpression(SeqProcesso, x => SeqProcesso == x.ConfiguracaoProcesso.SeqProcesso);
            AddExpression(SeqsProcessos, x => SeqsProcessos.Contains(x.ConfiguracaoProcesso.SeqProcesso));
            AddExpression(SeqConfiguracaoProcesso, x => SeqConfiguracaoProcesso == x.SeqConfiguracaoProcesso);

            if (DataSolicitacao.HasValue)
            {
                var dataSolicitacaoFim = DataSolicitacao.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                AddExpression(DataSolicitacao, x => x.DataSolicitacao >= DataSolicitacao && x.DataSolicitacao <= dataSolicitacaoFim);
            }
            AddExpression(CategoriaSituacao, x => x.SituacaoAtual.CategoriaSituacao == CategoriaSituacao);
            AddExpression(CategoriasSituacoes, x => CategoriasSituacoes.Contains(x.SituacaoAtual.CategoriaSituacao));
            AddExpression(SeqTipoDocumento, x => x.DocumentosRequeridos.Any(d => d.DocumentoRequerido.SeqTipoDocumento == SeqTipoDocumento));
            AddExpression(this.CPF, x => x.PessoaAtuacao.Pessoa.Cpf == this.CPF.Replace(".", "").Replace("-", ""));
            AddExpression(this.Passaporte, x => x.PessoaAtuacao.Pessoa.NumeroPassaporte == this.Passaporte);
            AddExpression(this.DescricaoLookupSolicitacao, a => a.NumeroProtocolo.Contains(this.DescricaoLookupSolicitacao)
                                                             || a.PessoaAtuacao.DadosPessoais.Nome.Contains(this.DescricaoLookupSolicitacao)
                                                             || a.PessoaAtuacao.DadosPessoais.NomeSocial.Contains(this.DescricaoLookupSolicitacao)
                                                             || a.GrupoEscalonamento.Descricao.Contains(this.DescricaoLookupSolicitacao));

            AddExpression(AcaoLiberacaoTrabalho, x => x.ConfiguracaoProcesso.Processo.Servico.AcaoLiberacaoTrabalho == AcaoLiberacaoTrabalho);

            return GetExpression();
        }
    }
}