using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioAlunosPorSituacaoFiltroVO : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        public List<long> SeqsSituacaoMatricula { get; set; }

        public List<long> SeqsTipoVinculoAluno { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public List<SituacaoIngressante> SituacoesIngressante { get; set; }

        public List<short> SeqsSituacoesIngressante
        {
            get
            {
                List<short> lista = new List<short>();

                if (this.SituacoesIngressante == null) { return lista; }

                foreach (var item in this.SituacoesIngressante)
                    lista.Add((short)item);
                return lista;
            }
        }
    }
}