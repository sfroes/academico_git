using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class HierarquiaEntidadeDuplicidadeTipoHierarquiaSpecification : SMCSpecification<HierarquiaEntidade>
    {
        public DateTime DataInicioVigencia { get; set; }
        public Nullable<DateTime> DataFimVigencia { get; set; }
        public TipoVisao? Visao { get; set; }

        public HierarquiaEntidadeDuplicidadeTipoHierarquiaSpecification(TipoVisao? Visao, DateTime DataInicioVigencia, Nullable<DateTime> DataFimVigencia = null)
        {
            this.DataInicioVigencia = DataInicioVigencia;
            this.DataFimVigencia = DataFimVigencia;
            this.Visao = Visao;
        }

        /// <summary>
        /// A interseção entre os períodos de vigência não pode ocorrer para um mesmo tipo de visão
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<HierarquiaEntidade, bool>> SatisfiedBy()
        {
            AddExpression(this.Visao, p =>
                                 (p.TipoHierarquiaEntidade.TipoVisao.HasValue && p.TipoHierarquiaEntidade.TipoVisao == this.Visao.Value)

                              //Tratamento para a interseção de Datas
                              && (
                                        (
                                             (
                                                   //     Verificando se a data de início para a vigência a ser cadastrada irá fazer interseção com a vigência corrente vinda da base
                                                   // &&  Verificando se a data fim para a vigência a ser cadastrada irá fazer interseção com a vigência corrente vinda da base 
                                                   (this.DataInicioVigencia >= p.DataInicioVigencia && this.DataInicioVigencia <= p.DataFimVigencia)
                                                || (this.DataFimVigencia.Value >= p.DataInicioVigencia && this.DataFimVigencia.Value <= p.DataFimVigencia)
                                             )

                                          //Tratamento para a a vigência sem fim cadastrado
                                          || (!this.DataFimVigencia.HasValue ? p.DataInicioVigencia >= this.DataInicioVigencia || p.DataFimVigencia >= this.DataInicioVigencia : false)
                                        )

                                        ||

                                        (
                                             (
                                                   //     Verificando se a data de início para a vigência corrente vinda da base irá fazer interseção com a vigência a ser cadastrada
                                                   // &&  Verificando se a data fim para a vigência corrente vinda da base irá fazer interseção com a vigência a ser cadastrada
                                                   (p.DataInicioVigencia >= this.DataInicioVigencia && p.DataInicioVigencia <= this.DataFimVigencia.Value)
                                                || (p.DataFimVigencia >= this.DataInicioVigencia && p.DataFimVigencia.Value <= this.DataFimVigencia.Value)
                                             )

                                         //Tratamento para a a vigência sem fim vinda da base
                                         || (!p.DataFimVigencia.HasValue ? this.DataInicioVigencia >= p.DataInicioVigencia || this.DataFimVigencia >= p.DataInicioVigencia : false)

                                        )
                                 )
                        );

            return GetExpression();
        }
    }
}