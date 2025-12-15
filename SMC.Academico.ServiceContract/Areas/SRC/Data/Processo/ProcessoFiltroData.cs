using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqUnidadeResponsavel { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoServico { get; set; }

        public long? SeqServico { get; set; }

        public string Descricao { get; set; }

        public long? SeqCampanhaCicloLetivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long[] SeqsServicos { get; set; }

        public long[] SeqsEntidadesResponsaveis { get; set; }

        public long[] SeqsProcesso { get; set; }

        public bool? ListarProcessosEncerrados { get; set; }
    }
}