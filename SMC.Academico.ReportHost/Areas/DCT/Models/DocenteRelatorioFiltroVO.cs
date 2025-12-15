using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.DCT.Models
{
    public class DocenteRelatorioFiltroVO : SMCViewModelBase, ISMCMappable
    {
        public long SeqInstituicaoEnsino { get; set; }

        public TipoRelatorio TipoRelatorio { get; set; }

        public DateTime DataInicioReferencia { get; set; }

        public DateTime DataFimReferencia { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqColaborador { get; set; }

    }
}