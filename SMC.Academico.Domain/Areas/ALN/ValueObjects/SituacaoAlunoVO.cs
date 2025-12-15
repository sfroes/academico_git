using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class SituacaoAlunoVO : ISMCMappable
    {
        public long? SeqAlunoHistoricoSituacao { get; set; }

        public long? SeqSituacao { get; set; }

        public long SeqCiclo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoXSD { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public bool? VinculoAlunoAtivo { get; set; }

        public string TokenSituacaoMatricula { get; set; }

        public string TokenTipoSituacaoMatricula { get; set; }
        public string UsuarioExclusao { get; set; }
        public string ObservacaoExclusao { get; set; }

        public long? SeqPeriodoIntercambio { get; set; }
        public DateTime? DataExclusao { get; set; }

        public DateTime DataInicioSituacao { get; set; }
    }
}