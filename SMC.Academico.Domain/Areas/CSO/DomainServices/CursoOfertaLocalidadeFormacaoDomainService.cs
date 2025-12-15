using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Common.Areas.CSO.Includes;
using System.Linq;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using System.Collections.Generic;
using SMC.Framework.Model;
using SMC.Framework.Extensions;
using System;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoOfertaLocalidadeFormacaoDomainService : AcademicoContextDomain<CursoOfertaLocalidadeFormacao>
    {
        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();

        public List<long?> BuscarGrauAcademicoPorCursoOfertaLocalidadeFormacao(long? seq)
        {
            var cursoOfertaLocalidadeFormacao = this.SearchBySpecification(new CursoOfertaLocalidadeFormacaoFilterSpecification() { SeqCursoLocalidade = seq },
                                                                           IncludesCursoOfertaLocalidadeFormacao.FormacaoEspecifica |
                                                                           IncludesCursoOfertaLocalidadeFormacao.CursoOfertaLocalidade_CursoOferta).ToList();

            var listaSeqCurso = cursoOfertaLocalidadeFormacao.Select(s => new CursoFormacaoEspecificaVO
            {
                SeqCurso = s.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqFormacaoEspecifica = s.SeqFormacaoEspecifica
            }).ToList();

            var listaSeqGrauAcademico = new List<long?>();
            if (listaSeqCurso.Any())
            {
                var filtro = new CursoFormacaoEspecificaFilterSpecification()
                {
                    SeqCurso = listaSeqCurso.Select(s => s.SeqCurso).Distinct().FirstOrDefault(),
                    SeqsFormacaoEspecifica = listaSeqCurso.Select(s => s.SeqFormacaoEspecifica).Distinct().ToList()
                };

                listaSeqGrauAcademico = CursoFormacaoEspecificaDomainService.BuscarGrauAcademico(filtro);
                return listaSeqGrauAcademico;
            }

            return listaSeqGrauAcademico;
        }
    }
}
