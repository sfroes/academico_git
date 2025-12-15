using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Domain;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class InstituicaoNivelTipoDivisaoComponenteDomainService : AcademicoContextDomain<InstituicaoNivelTipoDivisaoComponente>
    {
        #region DataSources

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        #endregion


        public InstituicaoNivelTipoDivisaoComponenteVO VerificarPermissaoCargaHorariaGrade(long seqTipoDivisaoComponente, long seqComponenteCurricular)
        {
            var componenteCurricular = this.ComponenteCurricularDomainService.SearchProjectionByKey(seqComponenteCurricular, c => new
            {
                c.NiveisEnsino.Where(n => n.Responsavel).FirstOrDefault().SeqNivelEnsino,
                c.SeqTipoComponenteCurricular
            });

            var specInstituicaoNivelTipoComponenteCurricular = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = componenteCurricular.SeqNivelEnsino,
                SeqTipoComponenteCurricular = componenteCurricular.SeqTipoComponenteCurricular
            };

            var seqInstituicaoNivelTipoComponenteCurricular = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionBySpecification(specInstituicaoNivelTipoComponenteCurricular, i => i.Seq).FirstOrDefault();

            var specInstituicaoNivelTipoDivisaoComponente = new InstituicaoNivelTipoDivisaoComponenteFilterSpecification() 
            {
                SeqTipoDivisaoComponente = seqTipoDivisaoComponente,
                SeqInstituicaoNivelTipoComponenteCurricular = seqInstituicaoNivelTipoComponenteCurricular
            };

            var result = this.SearchProjectionBySpecification(specInstituicaoNivelTipoDivisaoComponente, i => new InstituicaoNivelTipoDivisaoComponenteVO()
            {
                Seq = i.Seq,
                PermiteCargaHorariaGrade = i.PermiteCargaHorariaGrade
            }).FirstOrDefault();

            return result;
        }
    }
}