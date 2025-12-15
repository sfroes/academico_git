using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoTurmaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long? SeqOrientacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long[] SeqsAlunos { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        public string Nome { get; set; }

        public string RaNome { get; set; }

        public List<OrientacaoColaboradorData> Colaboradores { get; set; }
        
        public long SeqTurma { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string TokenTipoSituacaoMatricula { get; set; }

        public bool DiarioFechado { get; set; }

        public bool ExisteHistoricoEscolarAluno { get; set; }
    }
}
