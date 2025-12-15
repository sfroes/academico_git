using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class InstituicaoNivelTipoDocumentoAcademicoFilterSpecification : SMCSpecification<InstituicaoNivelTipoDocumentoAcademico>
    {
        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoDocumentoAcademico { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsTipoFormacaoEspecifica { get; set; }

        public GrupoDocumentoAcademico? GrupoDocumentoAcademico { get; set; }

        public UsoSistemaOrigem? UsoSistemaOrigem { get; set; }

        public override Expression<Func<InstituicaoNivelTipoDocumentoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoNivel, x => x.SeqInstituicaoNivel == this.SeqInstituicaoNivel.Value);
            AddExpression(SeqInstituicaoEnsino, x => x.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(SeqTipoDocumentoAcademico, x => x.SeqTipoDocumentoAcademico == this.SeqTipoDocumentoAcademico.Value);
            AddExpression(SeqsTipoFormacaoEspecifica, x => x.FormacoesEspecificas.Any(f => SeqsTipoFormacaoEspecifica.Contains(f.SeqTipoFormacaoEspecifica)));
            AddExpression(GrupoDocumentoAcademico, x => x.TipoDocumentoAcademico.GrupoDocumentoAcademico == this.GrupoDocumentoAcademico);
            AddExpression(SeqNivelEnsino, x => x.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(UsoSistemaOrigem, x => x.UsoSistemaOrigem == this.UsoSistemaOrigem);

            return GetExpression();
        }
    }
}