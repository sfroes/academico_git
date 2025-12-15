using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    /// <summary>
    /// Data para transferência de dados da entidade CursoFormacaoEspecifica
    /// </summary>
    public class CursoFormacaoEspecificaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqCurso { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public CursoTipoFormacao CursoTipoFormacao { get; set; }

        [SMCMapProperty("FormacaoEspecifica.TipoFormacaoEspecifica.ClasseTipoFormacao")]
        public ClasseTipoFormacao ClasseTipoFormacao { get; set; } = ClasseTipoFormacao.Curso;

        [SMCMapProperty("FormacaoEspecifica.SeqTipoFormacaoEspecifica")]
        public long SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqGrauAcademico { get; set; }

        [SMCMapProperty("FormacaoEspecifica.Descricao")]
        public string Descricao { get; set; }

        public List<CursoFormacaoEspecificaHierarquiaData> Hierarquia { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        [SMCMapProperty("FormacaoEspecifica.SeqCursoOferta")]
        public long? SeqCursoOferta { get; set; }

        [SMCMapProperty("Curso.TipoCurso")]
        public TipoCurso TipoCurso { get; set; }

        [SMCMapProperty("Curso.SeqNivelEnsino")]
        public List<long> SeqNivelEnsino { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public List<TitulacaoCursoFormacaoEspecificaData> Titulacoes { get; set; }

        public CursoData Curso { get; set; }

        //CursoFormacaoEspecifica quando o select do tipo tem apenas o valor grau com autoselect o mesmo não executa o dependency
        public bool GrauAcademicoRequerido { get; set; }
        public bool TitulacaoRequeridoPorFormacao { get; set; }

        public long SeqFormacaoEspecificaID { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }
    }
}