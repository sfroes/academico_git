using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponenteDivisaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public short? Numero { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.Descricao")]
        public string TipoDivisaoDescricao { get; set; }

        [SMCMapProperty("ConfiguracaoComponente.ComponenteCurricular.CargaHoraria")]
        public short CargaHorariaComponente { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public bool PermiteCargaHorariaGrade { get; set; }

        public bool QuantidadeSemanasComponentePreenchida { get; set; }
        public bool PermiteGrupoSomenteLeitura { get; set; }

        public bool PermiteGrupo { get; set; }

        public bool ExibirArtigo { get; set; }

        public QualisCapes? QualisCapes { get; set; }

        public TipoPublicacao? TipoPublicacao { get; set; }

        public TipoEventoPublicacao? TipoEventoPublicacao { get; set; }

        public long? SeqComponenteCurricularOrganizacao { get; set; }

        public TipoDivisaoComponente TipoDivisaoComponente { get; set; }

    }
}