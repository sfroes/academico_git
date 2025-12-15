using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class TipoHierarquiaEntidadeDomainService : AcademicoContextDomain<TipoHierarquiaEntidade>
    {
        /// <summary>
        /// Recupera o tipo de hierarquia na instituição de ensino respeitando o filtro global
        /// </summary>
        /// <param name="tipoVisao">Tipo da hierarqua</param>
        /// <exception cref="TipoHierarquiaEntidadeNaoConfigurouTipoException">
        /// Exceção lançada caso o tipo de hierarquia com a visão informada
        /// não esteja configurado na instituição de ensino
        /// </exception>
        /// <returns>Tipo de hierarquia na instituição</returns>
        public TipoHierarquiaEntidade BuscarTipoHierarquiaEntidadeNaInstituicao(TipoVisao tipoVisao)
        {
            var tipoHierarquiaEntidadeNaInstituicao =
                this.SearchByKey(new TipoHierarquiaEntidadeFilterSpecification() { TipoVisao = tipoVisao },
                                 IncludesTipoHierarquiaEntidade.InstituicaoEnsino);

            if (tipoHierarquiaEntidadeNaInstituicao == null)
            {
                throw new TipoHierarquiaEntidadeNaoConfigurouTipoException(tipoVisao.SMCGetDescription());
            }

            return tipoHierarquiaEntidadeNaInstituicao;
        }
    }
}