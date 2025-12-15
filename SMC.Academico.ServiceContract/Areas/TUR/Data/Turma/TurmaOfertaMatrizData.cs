using SMC.Academico.Common.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaOfertaMatrizData : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }

        public List<TurmaOfertaMatrizDetailData> OfertasMatriz { get; set; }

        public List<TurmaOfertaMatrizDetailData> OfertasMatrizDisplay { get; set; }

        public bool PermitirApenasInserirOfertas { get; set; }

        public bool DesabilitarAlteracaoOfertas { get; set; }

        public bool DesabilitarOfertasMatrizConfiguracaoPrincipal { get; set; }

        public bool DesabilitarMatrizCurricularOferta { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }
    }
}
