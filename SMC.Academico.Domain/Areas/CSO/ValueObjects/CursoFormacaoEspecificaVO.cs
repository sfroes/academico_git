using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoFormacaoEspecificaVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqCurso { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public Curso Curso { get; set; }

        public FormacaoEspecifica FormacaoEspecifica { get; set; }

        public IList<CursoFormacaoEspecificaTitulacao> Titulacoes { get; set; }

        public List<FormacaoEspecificaListaVO> Hierarquia { get; set; }

        public CursoTipoFormacao CursoTipoFormacao { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        //CursoFormacaoEspecifica quando o select do tipo tem apenas o valor grau com autoselect o mesmo não executa o dependency
        public bool GrauAcademicoRequerido { get; set; }
        public bool TitulacaoRequeridoPorFormacao { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public long SeqFormacaoEspecificaID { get; set; }

        [SMCMapProperty("GrauAcademico.Descricao")]
        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }
    }
}