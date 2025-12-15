using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService : AcademicoContextDomain<InstituicaoTipoEntidadeHierarquiaClassificacao>
    {

        /// <summary>
        /// Busca todas os InstituicaoTipoEntidadeHierarquiaClassificacao referentes à InstituicaoTipoEntidade
        /// </summary>
        /// <param name="SeqInstituicaoTipoEntidade">Seq da InstituicaoTipoEntidade</param>
        /// <returns></returns>
        public List<InstituicaoTipoEntidadeHierarquiaClassificacao> BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(long SeqInstituicaoTipoEntidade)
        {
            InstituicaoTipoEntidadeHierarquiasClassificacaoFilterSpecification specification = new InstituicaoTipoEntidadeHierarquiasClassificacaoFilterSpecification() {
                 SeqInstituicaoTipoEntidade = SeqInstituicaoTipoEntidade
            };

            return base.SearchBySpecification(specification, IncludesInstituicaoTipoEntidadeHierarquiaClassificacao.HierarquiaClassificacao).ToList();
        }


    }
}
