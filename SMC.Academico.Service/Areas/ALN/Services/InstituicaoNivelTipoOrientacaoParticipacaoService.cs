using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoService : SMCServiceBase, IInstituicaoNivelTipoOrientacaoParticipacaoService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoOrientacaoParticipacaoDomainService InstituicaoNivelTipoOrientacaoParticipacaoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoOrientacaoParticipacaoDomainService>(); }
        }

        #endregion [ DomainService ]

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoComObrigatoriedadeSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoComObrigatoriedadeSelect(filtros.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification>());
        }

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(filtros.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO>());
        }

        public InstituicaoNivelTipoOrientacaoParticipacaoData BuscarInstituicaoNivelTipoOrientacaoParticipacao(long seq)
        {
            return this.InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacao(seq).Transform<InstituicaoNivelTipoOrientacaoParticipacaoData>();
        }

        public List<InstituicaoNivelTipoOrientacaoParticipacaoData> BuscarInstituicaoNivelTipoOrientacaoParticipacoes(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacoes(filtros.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification>()).TransformList<InstituicaoNivelTipoOrientacaoParticipacaoData>();
        }

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoOrigemSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoOrigemSelect(filtros.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO>());
        }
    }
}