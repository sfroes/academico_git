using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class BeneficioHistoricoSituacaoDomainService : AcademicoContextDomain<BeneficioHistoricoSituacao>
    {
        /// <summary>
        /// Buscar situacao chancela beneficio atual
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuacao beneficio</param>
        /// <returns>Situacao chancela beneficio atual</returns>
        public SituacaoChancelaBeneficio BuscarHistoricoSituacaoChancelaBeneficioAtual(long seqPessoaAtuacaoBeneficio)
        {
            var spec = new BeneficioHistoricoSituacaoFilterSpecification() { SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaoBeneficio, Atual = true };
            var retorno = this.SearchBySpecification(spec).FirstOrDefault();

            //Se por alguma caso não tenha cadastrado nenhuma situação no historico
            if(retorno == null)
            {
                return SituacaoChancelaBeneficio.Nenhum;
            }

            return retorno.SituacaoChancelaBeneficio;
        }
    }
}