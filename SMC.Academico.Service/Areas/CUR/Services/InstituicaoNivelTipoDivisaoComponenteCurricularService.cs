using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class InstituicaoNivelTipoDivisaoComponenteCurricularService : SMCServiceBase, IInstituicaoNivelTipoDivisaoComponenteCurricularService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoDivisaoComponenteDomainService InstituicaoNivelTipoDivisaoComponenteDomainService => this.Create<InstituicaoNivelTipoDivisaoComponenteDomainService>();

        #endregion [ DomainService ]

        public InstituicaoNivelTipoDivisaoComponenteData VerificarPermissaoCargaHorariaGrade(long seqTipoDivisaoComponente, long seqComponenteCurricular)
        {
            return this.InstituicaoNivelTipoDivisaoComponenteDomainService.VerificarPermissaoCargaHorariaGrade(seqTipoDivisaoComponente, seqComponenteCurricular).Transform<InstituicaoNivelTipoDivisaoComponenteData>();
        }
    }
}