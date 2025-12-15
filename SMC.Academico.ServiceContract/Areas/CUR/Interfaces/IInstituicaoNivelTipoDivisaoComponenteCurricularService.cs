using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IInstituicaoNivelTipoDivisaoComponenteCurricularService : ISMCService
    {
        InstituicaoNivelTipoDivisaoComponenteData VerificarPermissaoCargaHorariaGrade(long seqTipoDivisaoComponente, long seqComponenteCurricular);
    }
}