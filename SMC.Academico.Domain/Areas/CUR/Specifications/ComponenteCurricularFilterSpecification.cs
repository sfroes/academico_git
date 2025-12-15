using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ComponenteCurricularFilterSpecification : SMCSpecification<ComponenteCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public int? CodigoComponenteLegado { get; set; }

        public string BancoLegado { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqInstituicaoNivelResponsavel { get; set; }

        public long[] SeqComponentesCurriculares { get; set; }

        public long[] SeqTipoComponentesCurriculares { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public override Expression<Func<ComponenteCurricular, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => Seq.Value == w.Seq);
            AddExpression(SeqInstituicaoEnsino, w => this.SeqInstituicaoEnsino.Value == w.SeqInstituicaoEnsino);
            AddExpression(SeqTipoComponenteCurricular, w => this.SeqTipoComponenteCurricular.Value == w.SeqTipoComponenteCurricular);
            AddExpression(Codigo, w => w.Codigo.StartsWith(this.Codigo));
            AddExpression(Descricao, w => w.Descricao.Contains(this.Descricao));
            AddExpression(Sigla, w => w.Sigla.StartsWith(this.Sigla));
            AddExpression(CodigoComponenteLegado, w => w.ComponentesLegado.Any(a => a.CodigoComponenteLegado == this.CodigoComponenteLegado));
            AddExpression(BancoLegado, w => w.ComponentesLegado.Any(a => a.BancoLegado.Contains(this.BancoLegado)));
            AddExpression(TipoOrganizacao, w => TipoOrganizacao.Value == w.TipoOrganizacao);
            AddExpression(Ativo, w => Ativo.Value == w.Ativo);
            AddExpression(SeqEntidade, w => w.EntidadesResponsaveis.Count(c => c.SeqEntidade == this.SeqEntidade) > 0);
            AddExpression(SeqInstituicaoNivelResponsavel, w => w.NiveisEnsino.Count(c => c.SeqNivelEnsino == this.SeqInstituicaoNivelResponsavel) > 0);
            AddExpression(SeqComponentesCurriculares, w => SeqComponentesCurriculares.Contains(w.Seq));
            AddExpression(SeqTipoComponentesCurriculares, w => SeqTipoComponentesCurriculares.Contains(w.SeqTipoComponenteCurricular));
            if (SeqMatrizCurricular.HasValue && TipoGestaoDivisaoComponente.HasValue)
            {
                // filtra os componentes que a configuração associada à matriz têm o tipo de gestão informado
                AddExpression(w => w
                    .Configuracoes.Any(a => a
                        .DivisoesMatrizCurricularComponente.Any(d => 
                            d.SeqMatrizCurricular == SeqMatrizCurricular && 
                            d.ConfiguracaoComponente.DivisoesComponente
                                .Any(dc => dc.TipoDivisaoComponente.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente)
                        )
                    )
                );
            }
            else
            {
                // filtra a asosciação à uma matriz OU tipo de gestão de forma independente (já que um dos dois não está preenchido)
                AddExpression(SeqMatrizCurricular, w => w.Configuracoes.Any(a => a.DivisoesMatrizCurricularComponente.Any(d => d.SeqMatrizCurricular == SeqMatrizCurricular)));
                AddExpression(TipoGestaoDivisaoComponente, w => w.TipoComponente.TiposDivisao.Any(a => a.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente));
            }

            if (FormatoConfiguracaoGrupo.HasValue)
            {
                switch (FormatoConfiguracaoGrupo.Value)
                {

                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria:

                        AddExpression(w => w.CargaHoraria.HasValue);

                        break;
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito:

                        AddExpression(w => w.Credito.HasValue);

                        break;
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens:

                        AddExpression(w => !w.CargaHoraria.HasValue && !w.Credito.HasValue);

                        break;
                }
            }

            return GetExpression();
        }
    }
}