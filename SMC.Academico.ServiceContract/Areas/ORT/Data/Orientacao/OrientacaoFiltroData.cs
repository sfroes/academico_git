using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public List<long?> SeqsEntidadesResponsaveisHierarquiaItem { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqColaborador { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public DateTime? DataInicioOrientacao { get; set; }

        public DateTime? DataFimOrientacao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoSituacaoMatricula { get; set; }

        public bool? PermiteManutencaoManual { get; set; } = true;

        public bool? ExibirOrientacoesFinalizadas { get; set; }

        public List<long?> SeqsTiposSituacoesMatriculas { get; set; }
    }
}
