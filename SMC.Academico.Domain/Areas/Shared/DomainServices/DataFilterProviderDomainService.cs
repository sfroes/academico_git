using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Domain.DataFilters;
using SMC.Framework.Repository;
using System.Linq;

namespace SMC.Academico.Domain.Areas.Shared.DomainServices
{
    public class DataFilterProviderDomainService : SMCDataFilterProviderDomainService
    {
        public DataFilterProviderDomainService() : base(AcademicoContextDomain.EF) { }

        public override SMCDataFilterValue[] Search<T>(ISMCRepository<T> repository, string token, ISMCFilterConfig<T> filter, bool isSelfRelationship, string includeFilter)
        {
            if (token == FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES || 
                token == FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL ||
                token == FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS ||
                token == FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA)
            {
                var spec = new HierarquiaEntidadeItemFilterSpecification();
                if (token == FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES) spec.TipoVisaoHierarquia = Common.Areas.ORG.Enums.TipoVisao.VisaoLocalidades;
                else if (token == FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL) spec.TipoVisaoHierarquia = Common.Areas.ORG.Enums.TipoVisao.VisaoOrganizacional;
                else if (token == FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS) spec.TipoVisaoHierarquia = Common.Areas.ORG.Enums.TipoVisao.VisaoPolosVirtuais;
                else spec.TipoVisaoHierarquia = Common.Areas.ORG.Enums.TipoVisao.VisaoUnidade;

                var rep = repository as ISMCRepository<HierarquiaEntidadeItem>;

                //ACHO QUE NAO PRECISA DISSO
               // rep.SearchAll(c => c.Seq);

                var results = rep.SearchProjectionBySpecification(spec, p => new SMCDataFilterValue
                {
                    Name = p.Entidade.Nome,
                    Seq = p.Seq,
                    SeqParent = p.SeqItemSuperior
                }, true).ToArray();

                return results;
            }
            else
            {
                return base.Search(repository, token, filter, isSelfRelationship, includeFilter);
            }
        }
    }
}