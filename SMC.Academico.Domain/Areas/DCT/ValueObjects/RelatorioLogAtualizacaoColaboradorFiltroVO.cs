using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class RelatorioLogAtualizacaoColaboradorFiltroVO : ISMCMappable
    {
        public DateTime? DataInicioReferencia { get; set; }

        public DateTime? DataFimReferencia { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqColaborador { get; set; }
    }
}