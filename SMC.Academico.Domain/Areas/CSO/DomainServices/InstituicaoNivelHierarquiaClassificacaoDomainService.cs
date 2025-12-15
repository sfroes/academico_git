using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoNivelHierarquiaClassificacaoDomainService : AcademicoContextDomain<InstituicaoNivelHierarquiaClassificacao>
    {
        /// <summary>
        /// Busca todas os InstituicaoNivelHierarquiaClassificacao referentes à InstituicaoTipoEntidade
        /// </summary>
        /// <param name="seqNivelEnsino">Seq do Nível de Ensino</param>
        /// <returns></returns>
        public List<InstituicaoNivelHierarquiaClassificacao> BuscarInstituicaoNivelHierarquiasClassificacao(long seqNivelEnsino)
        {
            InstituicaoNivelHierarquiasClassificacaoFilterSpecification specification = new InstituicaoNivelHierarquiasClassificacaoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino
            };

            //Include de instituição nível necessário para que seja aplicado o filtro de instiuição de ensino
            return base.SearchBySpecification(specification, IncludesInstituicaoNivelHierarquiaClassificacao.HierarquiaClassificacao | IncludesInstituicaoNivelHierarquiaClassificacao.InstituicaoNivel).ToList();
        }
    }
}