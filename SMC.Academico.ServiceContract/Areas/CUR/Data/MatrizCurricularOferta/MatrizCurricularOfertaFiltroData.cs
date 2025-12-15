using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularOfertaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public long? SeqDispensa { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsCampanhaOferta { get; set; }

        public List<long> SeqsTermoIntercambio { get; set; }

        public List<long> Seqs { get; set; }

        public long? Seq { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public DateTime? DataHistorico { get; set; }

        public DateTime? DataAtivacaoMatriz { get; set; }

        public long? SeqCurso { get; set; }

        public List<long> GrupoOrigemSeqComponentesCurriculares { get; set; }

        public List<long> GrupoDispensadoSeqComponentesCurriculares { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqCurriculo { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        public List<long> SeqsCurriculoCursoOfertas { get; set; }

        public bool IgnorarFiltroDados { get; set; }
    }
}