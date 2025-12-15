using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class OrientacaoTurmaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqOrientacao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long?[] SeqsAlunos { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqColaborador { get; set; }

        //String de tokens separados por virgula
        public string TokensTipoSituacaoMaticula { get; set; }

        public bool? VinculoAtivo { get; set; }

    }
}
