using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class MotivoAlteracaoBeneficioDomainService : AcademicoContextDomain<MotivoAlteracaoBeneficio>
    {
        /// <summary>
        /// Buscar todos os motivos de alteração benefcio por instituicão de ensino
        /// </summary>
        /// <param name="seqIntituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os motivos de alteração beneficio</returns>
        public List<MotivoAlteracaoBeneficio> BuscarMotivoAlteracaoBeneficioInstituicaoEnsino(long seqIntituicaoEnsino)
        {
            var spec = new MotivoAlteracaoBeneficioFilterSpecification() { SeqIntitiuicaoEnsino = seqIntituicaoEnsino };

            return this.SearchBySpecification(spec).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Buscar o sequencial do motivo de alteração por token
        /// </summary>
        /// <param name="token">Token de validação</param>
        /// <returns>Sequencial motivo alteração</returns>
        public long BuscarMotivoAlteracaoBeneficioPorToken(string token)
        {
            var spec = new MotivoAlteracaoBeneficioFilterSpecification() { Token = token };

            return this.SearchProjectionBySpecification(spec, p => p.Seq).FirstOrDefault();
        }
    }
}