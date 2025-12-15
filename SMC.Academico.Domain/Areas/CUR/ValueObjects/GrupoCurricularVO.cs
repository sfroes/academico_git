using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        [SMCMapProperty(nameof(Seq))]
        public long SeqGrupoCurricular { get; set; }

        [SMCMapProperty("TipoConfiguracaoGrupoCurricular.Descricao")]
        public string TipoConfiguracaoDescricao { get; set; }

        public long Index { get; set; }

        public long SeqCurriculo { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        public string Descricao { get; set; }

        public long SeqTipoGrupoCurricular { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long SeqTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public short? QuantidadeItens { get; set; }

        public short? QuantidadeHoraRelogio { get; set; }

        public short? QuantidadeHoraAula { get; set; }

        public short? QuantidadeCreditos { get; set; }

        public List<Beneficio> Beneficios { get; set; }

        public virtual List<CondicaoObrigatoriedade> CondicoesObrigatoriedade { get; set; }
    }
}