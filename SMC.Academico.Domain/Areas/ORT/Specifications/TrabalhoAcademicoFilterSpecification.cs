using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoFilterSpecification : SMCSpecification<TrabalhoAcademico>
    {
        public long? Seq { get; set; }
        public long? SeqInstituicaoLogada { get; set; }

        public string Letra { get; set; }

        public List<TipoPesquisaTrabalhoAcademico> TipoPesquisa { get; set; }

        public string Pesquisa { get; set; }

        public long? SeqAluno { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoSituacao { get; set; }

        public long? SeqTipoTrabalho { get; set; }
        public long? SeqEntidadeInstituicaoEnsino { get; set; }

        public List<long?> SeqsTipoTrabalho { get; set; }

        public string Titulo { get; set; }

        public bool? PossuiDataDeposito { get; set; }
        public string DescricaoTipoTrabalho { get; set; }

        public List<FiltroSituacaoTrabalhoAcademico> Situacao { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(PossuiDataDeposito, x => x.DataDepositoSecretaria.HasValue == PossuiDataDeposito);
            AddExpression(SeqInstituicaoLogada, x => x.SeqInstituicaoEnsino == SeqInstituicaoLogada);
            AddExpression(SeqEntidadeInstituicaoEnsino, x => x.SeqInstituicaoEnsino == SeqEntidadeInstituicaoEnsino);
            AddExpression(DescricaoTipoTrabalho, x => x.TipoTrabalho.Descricao.Equals(DescricaoTipoTrabalho));
            if (Pesquisa == null && TipoPesquisa != null)
            {
                FiltroPorLetraAlfabeto();
            }
            else if (!string.IsNullOrEmpty(Pesquisa))
            {
                // Pesquisa por palavras chave
                var pesquisa = Pesquisa.ToLower();
                AddExpression(x => x.PublicacaoBdp.Any(f => f.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico == SituacaoTrabalhoAcademico.LiberadaConsulta));
                AddExpression(x => x.PublicacaoBdp.Any(f => f.InformacoesIdioma.Any(g => g.Titulo.ToLower().Contains(pesquisa)
                                                                                        || g.PalavrasChave.Any(h => h.PalavraChave == pesquisa)
                                                                                        || g.Resumo.ToLower().Contains(pesquisa))));
            }
            else
            {
                FiltroTelasAcademico();
            }

            return GetExpression();
        }

        private void FiltroTelasAcademico()
        {
            AddExpression(SeqTipoTrabalho, x => x.SeqTipoTrabalho == this.SeqTipoTrabalho);
            AddExpression(SeqsTipoTrabalho, x => SeqsTipoTrabalho.Contains(x.SeqTipoTrabalho));
            AddExpression(SeqTurno, p => p.Autores.Any(a => a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Seq == this.SeqTurno));

            if (SeqCicloLetivo.HasValue)
            {
                AddExpression(SeqCicloLetivo, p => p.Autores.Any(a => a.Aluno.Historicos.Any(w => w.Atual && w.HistoricosCicloLetivo.Any(h => h.SeqCicloLetivo == SeqCicloLetivo && !h.DataExclusao.HasValue))));
                AddExpression(SeqTipoSituacao, p => p.Autores.Any(a => a.Aluno.Historicos.FirstOrDefault(w => w.Atual)
                                                                        .HistoricosCicloLetivo.FirstOrDefault(o => o.SeqCicloLetivo == SeqCicloLetivo && !o.DataExclusao.HasValue)
                                                                        .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
                                                                        .FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue)
                                                                        .SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacao));
            }
            else
            {
                AddExpression(SeqTipoSituacao, p => p.Autores.Any(a => a.Aluno.Historicos.FirstOrDefault(w => w.Atual)
                                                        .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(o => !o.DataExclusao.HasValue)
                                                        .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
                                                        .FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue)
                                                        .SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacao));
            }

            //
            AddExpression(Titulo, p => p.Autores.Any(a => a.TrabalhoAcademico.Titulo.Contains(Titulo)));
            AddExpression(SeqCursoOfertaLocalidade, p => p.Autores.Any(a => a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq == this.SeqCursoOfertaLocalidade));
            AddExpression(SeqsEntidadesResponsaveis, p => p.Autores.Any(a => a.Aluno.Historicos.Any(z => SeqsEntidadesResponsaveis.Contains(z.SeqEntidadeVinculo))));
            AddExpression(SeqAluno, p => p.Autores.Any(a => a.Aluno.Seq == this.SeqAluno));

            if (Situacao != null && Situacao.Count > 0)
            {
                // Converte a lista de situações do filtro para as situações do trabalho.
                var situacoes = new List<SituacaoTrabalhoAcademico>();
                foreach (var situacao in Situacao)
                {
                    if (situacao == FiltroSituacaoTrabalhoAcademico.AguardandoAutorizacaoAluno)
                    {
                        situacoes.Add(SituacaoTrabalhoAcademico.AguardandoCadastroAluno);
                        situacoes.Add(SituacaoTrabalhoAcademico.CadastradaAluno);
                    }
                    else if (situacao == FiltroSituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria)
                    {
                        situacoes.Add(SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria);
                    }
                    else if (situacao == FiltroSituacaoTrabalhoAcademico.LiberadaBiblioteca)
                    {
                        situacoes.Add(SituacaoTrabalhoAcademico.LiberadaBiblioteca);
                    }
                    else if (situacao == FiltroSituacaoTrabalhoAcademico.LiberadaConsulta)
                    {
                        situacoes.Add(SituacaoTrabalhoAcademico.LiberadaConsulta);
                    }
                }

                AddExpression(p => situacoes.Contains(p.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
                                                                    .FirstOrDefault().SituacaoTrabalhoAcademico));
            }
        }

        private void FiltroPorLetraAlfabeto()
        {
            AddExpression(x => x.PublicacaoBdp.Any(f => f.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico == SituacaoTrabalhoAcademico.LiberadaConsulta));
            // Pesquisa por letra do alfabeto
            AddExpression(x => (TipoPesquisa.Contains(TipoPesquisaTrabalhoAcademico.Autor) && x.Autores.Any(f => f.NomeAutor.StartsWith(Letra)))
                             || x.DivisoesComponente.Any(f =>
                                    f.OrigemAvaliacao.AplicacoesAvaliacao.Any(g =>
                                        g.MembrosBancaExaminadora.Any(
                                            h =>
                                            (
                                                h.Colaborador.DadosPessoais.Nome.StartsWith(Letra) ||
                                                h.NomeColaborador.StartsWith(Letra)
                                            )
                                                &&
                                            (
                                                (TipoPesquisa.Contains(TipoPesquisaTrabalhoAcademico.Orientador) && h.TipoMembroBanca == Common.Areas.APR.Enums.TipoMembroBanca.Orientador) ||
                                                (TipoPesquisa.Contains(TipoPesquisaTrabalhoAcademico.Coorientador) && h.TipoMembroBanca == Common.Areas.APR.Enums.TipoMembroBanca.Coorientador && h.Participou == true)
                                            )
                                        )
                                    )
                                )
                            );
        }
    }
}