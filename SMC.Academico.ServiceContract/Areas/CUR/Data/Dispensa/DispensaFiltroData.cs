using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DispensaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqComponenteCurricular { get; set; }

        //SMCCheckBoxList não permite ser null para realizar o bind correto comparar com nenhum
        public TipoDispensa TipoFiltro { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public MatrizExcecaoDispensa? MatrizAssociada { get; set; }

        public ModoExibicaoHistoricoEscolar? ModoExibicaoHistoricoEscolar { get; set; }
    }
}