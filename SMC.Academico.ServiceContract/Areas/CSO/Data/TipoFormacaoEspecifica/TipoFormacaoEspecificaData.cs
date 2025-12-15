using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoFormacaoEspecificaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }

        public ClasseTipoFormacao? ClasseTipoFormacao { get; set; }

        public bool? ExigeGrau { get; set; }

        public bool? PermiteEmitirDocumentoConclusao { get; set; }

        public bool? GeraCarimbo { get; set; }

        public bool PermiteTitulacao { get; set; }

        public bool ExibeGrauDescricaoFormacao { get; set; }

        public List<TipoFormacaoEspecificaTipoCursoData> TiposCurso { get; set; }
    }
}