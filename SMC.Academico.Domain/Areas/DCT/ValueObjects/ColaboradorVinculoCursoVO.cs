using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVinculoCursoVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("CursoOfertaLocalidade.Nome")]
        public string NomeCursoOfertaLocalidade { get; set; }

        [SMCMapMethod("CursoOfertaLocalidade.RecuperarNomeLocalidade")]
        public string NomeLocalidade { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long SeqColaboradorVinculo { get; set; }

        public List<TipoAtividadeColaborador> TipoAtividadeColaborador { get; set; }

        public List<SMCDatasourceItem> TiposAtividades { get; set; }
    }
}