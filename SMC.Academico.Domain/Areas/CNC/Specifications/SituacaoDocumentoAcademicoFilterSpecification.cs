using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class SituacaoDocumentoAcademicoFilterSpecification : SMCSpecification<SituacaoDocumentoAcademico>
    {
        public long? Seq { get; set; }

        public string Token { get; set; }

        public string Descricao { get; set; }

        public List<GrupoDocumentoAcademico> ListaGrupoDocumentoAcademico { get; set; }
        public GrupoDocumentoAcademico? GrupoDocumentoAcademico { get; set; }


        public override Expression<Func<SituacaoDocumentoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == this.Seq);
            AddExpression(Token, x => x.Token == this.Token);            
            AddExpression(Descricao, x => x.Descricao.Contains(this.Descricao));        
            AddExpression(ListaGrupoDocumentoAcademico, x => x.GruposDocumento.Any(a => ListaGrupoDocumentoAcademico.Contains(a.GrupoDocumentoAcademico)));
            AddExpression(GrupoDocumentoAcademico, x => GrupoDocumentoAcademico == null || x.GruposDocumento.Any(a => a.GrupoDocumentoAcademico == GrupoDocumentoAcademico));

            return GetExpression();
        }
    }
}
