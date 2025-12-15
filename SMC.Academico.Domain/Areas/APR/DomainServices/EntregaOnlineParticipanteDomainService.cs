using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class EntregaOnlineParticipanteDomainService : AcademicoContextDomain<EntregaOnlineParticipante>
    {

    }
}