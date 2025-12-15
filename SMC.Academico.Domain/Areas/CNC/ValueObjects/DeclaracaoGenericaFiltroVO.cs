
using SMC.Academico.Common.Areas.CNC.Enums;
using System.Collections.Generic;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class DeclaracaoGenericaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqPessoa { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOfertaLocalidadeParam { get; set; }

        public List<long?> SeqsTiposDocumentos { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public DateTime? DataFimInclusao { get; set; }

        public DateTime? DataInicioInclusao { get; set; }

    }
}