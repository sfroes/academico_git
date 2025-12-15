using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class LogAtualizacaoColaboradorDomainService : AcademicoContextDomain<LogAtualizacaoColaborador>
    {
        public DateTime? Datetime { get; private set; }

        public List<RelatorioLogAtualizacaoColaboradorListaVO> BuscarLogsAtualizacoesColaboradoresRelatorio(RelatorioLogAtualizacaoColaboradorFiltroVO filtro)
        {
            var lista = new List<RelatorioLogAtualizacaoColaboradorListaVO>();

            var specLogAtualizacaoColaborador = filtro.Transform<RelatorioLogAtualizacaoColaboradorFilterSpecification>();

            specLogAtualizacaoColaborador.SetOrderBy(x => x.DataProcessamento);
            specLogAtualizacaoColaborador.SetOrderBy(x => x.Colaborador.DadosPessoais.Nome);

            var logs = this.SearchProjectionBySpecification(specLogAtualizacaoColaborador, l => new
            {
                l.DataProcessamento,
                l.SeqColaborador,
                l.Colaborador.DadosPessoais.Nome,
                l.MotivoAtualizacaoVinculo,
                l.DataInicioAfastamento,
                l.DataFimAfastamento,
                Entidades = l.Vinculos.Select(v => v.ColaboradorVinculo.EntidadeVinculo.Nome).Distinct().ToList(),
                AtividadesFinalizadasBanca = l.Itens.Where(i => i.SeqAplicacaoAvaliacao != null).Select(i => new
                {
                    i.SeqAplicacaoAvaliacao,
                    i.TipoMembroBanca,
                    AlunosBanca = i.AplicacaoAvaliacao.OrigemAvaliacao.DivisoesComponente.SelectMany(d => d.TrabalhoAcademico.Autores.Select(a => new
                    {
                        a.Aluno.NumeroRegistroAcademico,
                        a.Aluno.DadosPessoais.Nome,
                        a.Aluno.DadosPessoais.NomeSocial
                    })).ToList(),
                }).GroupBy(g => g.TipoMembroBanca).ToList(),
                AtividadesFinalizadasOrientacao = l.Itens.Where(i => i.SeqOrientacaoColaborador != null).Select(i => new
                {
                    l.SeqColaborador,
                    AlunosOrientacao = i.OrientacaoColaborador.Orientacao.OrientacoesPessoaAtuacao.Select(o => new
                    {
                        o.SeqPessoaAtuacao,
                        (o.PessoaAtuacao as Aluno).NumeroRegistroAcademico,
                        o.PessoaAtuacao.DadosPessoais.Nome,
                        o.PessoaAtuacao.DadosPessoais.NomeSocial
                    }).ToList()
                }).GroupBy(g => g.SeqColaborador).ToList(),
                AtividadesFinalizadasResponsavel = l.Itens.Where(i => i.SeqColaboradorResponsavelVinculo != null).Select(i => new
                {
                    i.ColaboradorResponsavelVinculo.ColaboradorVinculo.SeqColaborador,
                    i.ColaboradorResponsavelVinculo.ColaboradorVinculo.Colaborador.DadosPessoais.Nome,
                    i.ColaboradorResponsavelVinculo.ColaboradorVinculo.Colaborador.DadosPessoais.NomeSocial
                }).ToList()
            }).ToList();

            foreach (var log in logs)
            {
                var acao = string.Empty;

                switch (log.MotivoAtualizacaoVinculo)
                {
                    case Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.Afastamento:
                    case Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.Demissao:
                    case Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.EncerramentoContrato:
                        acao = "Perda de vínculo";
                        break;
                    case Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.NovoContrato:
                    case Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.RetornoAfastamento:
                        acao = "Ganho de vínculo";
                        break;
                }

                var atividadesFinalizada = string.Empty;
                var aluno = string.Empty;

                foreach (var atividade in log.AtividadesFinalizadasBanca)
                {
                    atividadesFinalizada += $"Banca examinadora - {SMCEnumHelper.GetDescription(atividade.Key)}";

                    foreach (var alunoBanca in atividade.SelectMany(a => a.AlunosBanca).OrderBy(o => o.NomeSocial).ThenBy(o => o.Nome))
                    {
                        aluno += $"{PessoaDadosPessoaisDomainService.FormatarNomeSocial(alunoBanca.Nome, alunoBanca.NomeSocial)} - {alunoBanca.NumeroRegistroAcademico}<br/>";
                        atividadesFinalizada += "<br/>";
                    }

                    atividadesFinalizada += "<br/>";
                    aluno += "<br/>";

                }

                foreach (var atividade in log.AtividadesFinalizadasOrientacao)
                {
                    atividadesFinalizada += $"Orientação";

                    foreach (var alunoOrientacao in atividade.SelectMany(a => a.AlunosOrientacao).OrderBy(o => o.NomeSocial).ThenBy(o => o.Nome).Distinct())
                    {
                        aluno += $"{PessoaDadosPessoaisDomainService.FormatarNomeSocial(alunoOrientacao.Nome, alunoOrientacao.NomeSocial)} - {alunoOrientacao.NumeroRegistroAcademico}<br/>";
                        atividadesFinalizada += "<br/>";
                    }

                    atividadesFinalizada += "<br/>";
                    aluno += "<br/>";
                }

                if (log.AtividadesFinalizadasResponsavel.SMCAny())
                {
                    atividadesFinalizada += $"Professor responsável";

                    var listaOrdenadaPosDoutorandos = log.AtividadesFinalizadasResponsavel
                        .Select(s => new
                        {
                            s.SeqColaborador,
                            Nome = PessoaDadosPessoaisDomainService.FormatarNomeSocial(s.Nome, s.NomeSocial)
                        })
                        .Distinct()
                        .OrderBy(o => o.Nome);
                    foreach (var posDoutorando in listaOrdenadaPosDoutorandos)
                    {
                        aluno += $"{posDoutorando.Nome} - {posDoutorando.SeqColaborador}<br/>";
                        atividadesFinalizada += "<br/>";
                    }

                    atividadesFinalizada += "<br/>";
                    aluno += "<br/>";
                }

                if (log.AtividadesFinalizadasBanca.Count == 0 && log.AtividadesFinalizadasOrientacao.Count == 0)
                    atividadesFinalizada = "Não possui atividade";

                string dataInicioAfastamento = null;
                string dataFimAfastamento = null;

                if(log.MotivoAtualizacaoVinculo != Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.Afastamento)
                {
                    dataInicioAfastamento = null;
                    dataFimAfastamento = null;
                }
                else
                {
                    dataInicioAfastamento = log.DataInicioAfastamento.HasValue ? log.DataInicioAfastamento.Value.Date.ToShortDateString() : null;
                    dataFimAfastamento = log.DataFimAfastamento.HasValue ? log.DataFimAfastamento.Value.Date.ToShortDateString() : null;
                }

                lista.Add(new RelatorioLogAtualizacaoColaboradorListaVO()
                {
                    DataProcessamento = log.DataProcessamento.ToString("dd/MM/yyyy"),
                    Professor = $"{log.SeqColaborador} / {log.Nome}",
                    Acao = acao,
                    Motivo = SMCEnumHelper.GetDescription(log.MotivoAtualizacaoVinculo),
                    Entidade = string.Join("<br/>", log.Entidades),
                    AtividadeFinalizada = atividadesFinalizada,
                    Aluno = aluno,
                    DataInicioAfastamento = dataInicioAfastamento,
                    DataFimAfastamento = dataFimAfastamento
                });
            }

            if(lista.SMCAny(c => c.Motivo == SMCEnumHelper.GetDescription(Common.Areas.DCT.Enums.MotivoAtualizacaoVinculo.Afastamento)))
            {
                lista.SMCForEach(d => d.ExibeColunasInicioFimAfastamento = true);
            }

            return lista;
        }
    }
}