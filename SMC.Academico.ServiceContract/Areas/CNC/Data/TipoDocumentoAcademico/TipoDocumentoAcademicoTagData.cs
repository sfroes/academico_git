using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TipoDocumentoAcademicoTagData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public long SeqTag { get; set; }

        public bool? PermiteEditarDado { get; set; }

        public string InformacaoTag { get; set; }

        public bool? TipoReadOnly { get; set; }

        public TipoPreenchimentoTag TipoPreenchimentoTag { get; set; }
    }
}
