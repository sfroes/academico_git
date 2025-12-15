using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class GrupoAuxiliarDocumentoRequeridoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public short MinimoObrigatorio { get; set; }
        public List<long> Itens { get; set; }
        public string NomeGrupo { get; set; }
        
    }
}
