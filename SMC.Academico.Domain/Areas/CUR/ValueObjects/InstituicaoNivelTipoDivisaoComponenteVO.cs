using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class InstituicaoNivelTipoDivisaoComponenteVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.TipoGestaoDivisaoComponente")]
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }

        public long? SeqTipoTrabalho { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public string DescricaoTipoEventoAgd { get; set; }

        public bool PermiteCargaHorariaGrade { get; set; }
    }
}