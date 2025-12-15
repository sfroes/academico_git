using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoCursoOfertaGrupoTipoConfiguracaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoConfiguracaoGrupoCurricular { get; set; }

        public string DescricaoTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupoGrupoCurricular { get; set; }

        public string QuantidadeFormatada { get; set; }
    }
}
