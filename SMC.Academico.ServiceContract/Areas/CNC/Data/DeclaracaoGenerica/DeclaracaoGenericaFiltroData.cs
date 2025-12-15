using SMC.Academico.Common.Areas.CNC.Enums;
using System.Collections.Generic;
using System;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica
{
    public class DeclaracaoGenericaFiltroData : SMCPagerFilterData, ISMCMappable
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
