using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculoCursoOfertaGrupoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoConfiguracaoGrupoCurricular { get; set; }

        public string DescricaoTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupoGrupoCurricular { get; set; }

        public string QuantidadeFormatada { get; set; }

        public CurriculoCursoOfertaGrupoValorVO QuantidadesDisponiveis { get; set; }
    }
}