using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaMatriculaListarDetailData : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public List<SMCDatasourceItem> DivisoesTurmas { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public string DivisaoTurmaDescricao { get; set; }

        public string DivisaoTurmaRelatorioDescricao { get; set; }

        public bool PermitirGrupo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        public string Situacao { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        public long SeqTurma { get; set; }
    }
}
