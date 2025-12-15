using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeResponsavelVisaoSpecification : SMCSpecification<Entidade>
    {
        public TipoVisao Tipovisao { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public List<long> SeqsInstituicoesEnsino { get; set; }

        public long? Seq { get; set; }

        public List<long> SeqsTipoEntidade { get; set; }

        public string Nome { get; set; }

        public bool ListarApenasEntidadesAtivas { get; set; }

        public List<long> SeqsEntidadesSelecionadas { get; set; }

        public override Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            // Filtra as entidades que são responsável na visão (filtro obrigatório)
            DateTime dataAtual = DateTime.Today;
            AddExpression(e => e.HierarquiasEntidades.Any(i => i.TipoHierarquiaEntidadeItem.Responsavel &&
                                                               i.HierarquiaEntidade.TipoHierarquiaEntidade.TipoVisao == this.Tipovisao &&
                                                               i.HierarquiaEntidade.DataInicioVigencia <= dataAtual &&
                                                               (i.HierarquiaEntidade.DataFimVigencia >= dataAtual || !i.HierarquiaEntidade.DataFimVigencia.HasValue)));

            // Aplica o filtro de instituição de ensino
            AddExpression(this.SeqInstituicaoEnsino, e => this.SeqInstituicaoEnsino.Value == e.SeqInstituicaoEnsino);
            AddExpression(SeqsInstituicoesEnsino, e => SeqsInstituicoesEnsino.Contains(e.SeqInstituicaoEnsino.Value));

            // Aplica os filtros da tela (lookup EntidadeSelecaoMultipla)
            AddExpression(this.Seq, e => e.Seq == this.Seq);
            AddExpression(this.SeqsTipoEntidade, i => this.SeqsTipoEntidade.Contains(i.SeqTipoEntidade));
            AddExpression(this.Nome, e => e.Nome.Contains(this.Nome));

            // Verifica se é para listar apenas as ativas ou não
            if (ListarApenasEntidadesAtivas)
            {
                // Se for listar apenas as ativas, mas tem alguma seleção feita, lista o que está selecionado independente de estar ativa
                if (this.SeqsEntidadesSelecionadas != null && this.SeqsEntidadesSelecionadas.Count > 0)
                {
                    AddExpression(e => e.HistoricoSituacoes.FirstOrDefault(f => f.DataInicio <= dataAtual && (!f.DataFim.HasValue || f.DataFim >= dataAtual)).SituacaoEntidade.CategoriaAtividade != CategoriaAtividade.Inativa ||
                                       this.SeqsEntidadesSelecionadas.Contains(e.Seq));
                }
                else
                {
                    AddExpression(e => e.HistoricoSituacoes.FirstOrDefault(f => f.DataInicio <= dataAtual && (!f.DataFim.HasValue || f.DataFim >= dataAtual)).SituacaoEntidade.CategoriaAtividade != CategoriaAtividade.Inativa);
                }
            }
            else
            {
                // Se passou o parâmetro = FALSE, lista entidades que tenha alguma situação válida
                AddExpression(e => e.HistoricoSituacoes.FirstOrDefault(f => f.DataInicio <= dataAtual && (!f.DataFim.HasValue || f.DataFim >= dataAtual)) != null);
            }

            return GetExpression();
        }
    }
}
