using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.Validators;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Framework;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Util;
using SMC.Framework.Validation;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class ProgramaTipoAutorizacaoBdpDomainService : AcademicoContextDomain<ProgramaTipoAutorizacaoBdp>
    {
        /// <summary>
        /// Buscar os tipos de autorização da publicação bpd por programa
        /// </summary>
        /// <param name="seqPrograma">Sequencial do programa</param>
        /// <returns>Lista select dos tipos de autorização</returns>
        public List<SMCDatasourceItem> BuscarTipoAutorizacaoPorProgramaSelect(long seqPrograma)
        {
            var spec = new ProgramaTipoAutorizacaoBdpFilterSpecification() { SeqPrograma = seqPrograma };

            var result = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = (int)p.TipoAutorizacao

            }).ToList();

            result.SMCForEach(f => f.Descricao = SMCEnumHelper.GetDescription(SMCEnumHelper.GetEnum<TipoAutorizacao>(f.Seq.ToString())));

            return result;
        }
    }
}