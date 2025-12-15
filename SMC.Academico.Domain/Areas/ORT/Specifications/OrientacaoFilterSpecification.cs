using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ORG.Models;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class OrientacaoFilterSpecification : SMCSpecification<Orientacao>
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqColaborador { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public List<long?> SeqsEntidadesResponsaveisHierarquiaItem { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqTipoSituacaoMatricula { get; set; }

        public List<long?> SeqsTiposSituacoesMatriculas { get; set; }

        public bool? PermiteManutencaoManual { get; set; }

        public bool SomenteSemTipoTermoIntercambio { get; set; }

        public string TokenTipoOrientacao { get; set; }

        public bool? ExibirOrientacoesFinalizadas { get; set; }

        public override Expression<Func<Orientacao, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq.Value);
            AddExpression(this.SeqColaborador, a => a.OrientacoesColaborador.Any(o => o.SeqColaborador == this.SeqColaborador));
            AddExpression(this.SeqPessoaAtuacao, a => a.OrientacoesPessoaAtuacao.Any(o => o.SeqPessoaAtuacao == this.SeqPessoaAtuacao));
            AddExpression(this.SeqTurma, a => a.ItensPlanoEstudo.Any(i => i.DivisaoTurma.SeqTurma == this.SeqTurma));
            AddExpression(this.SeqTipoTermoIntercambio, a => a.SeqTipoTermoIntercambio == this.SeqTipoTermoIntercambio.Value);
            AddExpression(this.SeqTipoOrientacao, a => a.SeqTipoOrientacao == this.SeqTipoOrientacao.Value);
            AddExpression(this.PermiteManutencaoManual, a => a.TipoOrientacao.PermiteManutencaoManual == this.PermiteManutencaoManual);
            AddExpression(this.TipoAtuacao, a => a.OrientacoesPessoaAtuacao.Any(w => w.PessoaAtuacao.TipoAtuacao == this.TipoAtuacao));
            AddExpression(this.SeqTurno, a => a.OrientacoesPessoaAtuacao.Select(s => (s.PessoaAtuacao as Aluno).Historicos.Where(h => h.Atual == true).FirstOrDefault()
                                                                                                               .CursoOfertaLocalidadeTurno.SeqTurno).FirstOrDefault() == this.SeqTurno);
            AddExpression(this.SeqCursoOfertaLocalidade, a => a.OrientacoesPessoaAtuacao.Select(s => (s.PessoaAtuacao as Aluno).Historicos.Where(h => h.Atual == true).FirstOrDefault()
                                                                                                     .CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq).FirstOrDefault() == this.SeqCursoOfertaLocalidade);
            //AddExpression(this.SeqsEntidadesResponsaveis, a => this.SeqsEntidadesResponsaveis.Any(se => a.OrientacoesPessoaAtuacao.Select(s => 
            //                                                                                         (s.PessoaAtuacao as Aluno).Historicos.Where(w => w.Atual == true).FirstOrDefault()
            //                                                                                         .EntidadeVinculo.Seq).Any(al => al == se)));
            AddExpression(this.SeqsEntidadesResponsaveisHierarquiaItem, a => this.SeqsEntidadesResponsaveisHierarquiaItem.Any(se => a.OrientacoesPessoaAtuacao.Select(s =>
                                                                                         (s.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual)
                                                                                         .CursoOfertaLocalidadeTurno
                                                                                         .CursoOfertaLocalidade
                                                                                         .CursoOferta
                                                                                         .Curso
                                                                                         .HierarquiasEntidades.FirstOrDefault()
                                                                                         .SeqItemSuperior).Any(al => al == se)));
            AddExpression(TokenTipoOrientacao, x => x.TipoOrientacao.Token == TokenTipoOrientacao);

            ///Regra para pesquisa de ciclo e situação
            if (this.SeqCicloLetivo.HasValue && this.SeqTipoSituacaoMatricula.HasValue)
            {
                //AddExpression(a => a.OrientacoesPessoaAtuacao.Any(ao => (ao.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual)
                //                .HistoricosCicloLetivo.FirstOrDefault(f => f.DataExclusao == null && f.SeqCicloLetivo == this.SeqCicloLetivo
                //                                        && f.AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).Any(ahs => ahs.DataInicioSituacao <= DateTime.Today
                //                                                                            && !ahs.DataExclusao.HasValue
                //                                                                            && ahs.SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacaoMatricula)) != null));
                AddExpression(a => a.OrientacoesPessoaAtuacao.Any(ao => (ao.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual)
                .HistoricosCicloLetivo.FirstOrDefault(f => f.DataExclusao == null && f.SeqCicloLetivo == this.SeqCicloLetivo)
                                                             .AlunoHistoricoSituacao.OrderByDescending(h => h.DataInicioSituacao)
                                                             .FirstOrDefault(h => h.DataInicioSituacao <= DateTime.Today && !h.DataExclusao.HasValue)
                                                             .SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacaoMatricula));
            }
            else if (this.SeqCicloLetivo.HasValue)
            {
                AddExpression(this.SeqCicloLetivo, a => a.OrientacoesPessoaAtuacao.Any(s => (s.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(ah => ah.Atual)
                                                                                           .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(t => t.CicloLetivo.Numero)
                                                                                           .FirstOrDefault(f => f.AlunoHistoricoSituacao.Any(ahs => ahs.DataInicioSituacao <= DateTime.Today && !ahs.DataExclusao.HasValue))
                                                                                           .SeqCicloLetivo == this.SeqCicloLetivo));
            }
            else
            {
                AddExpression(this.SeqTipoSituacaoMatricula, a => a.OrientacoesPessoaAtuacao.Any(s => (s.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(w => w.Atual)
                                                                                            .HistoricosCicloLetivo.OrderByDescending(c => c.CicloLetivo.Ano)
                                                                                            .ThenByDescending(c => c.CicloLetivo.Numero).FirstOrDefault(c => !c.DataExclusao.HasValue)
                                                                                            .AlunoHistoricoSituacao.OrderByDescending(h => h.DataInicioSituacao).FirstOrDefault(h => h.DataInicioSituacao <= DateTime.Today && !h.DataExclusao.HasValue).SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacaoMatricula));
            }

            if (SomenteSemTipoTermoIntercambio)
            {
                AddExpression(x => !x.SeqTipoTermoIntercambio.HasValue);
            }

            return GetExpression();
        }
    }
}
