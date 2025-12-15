using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculoCursoOfertaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculo { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public long SeqCurso { get; set; }

        public string CursoSigla { get; set; }

        public string CursoDescricao { get; set; }

        public long SeqCursoOferta { get; set; }

        public string CursoOfertaDescricao { get; set; }

        public bool Ativo { get; set; }

        public short? QuantidadeHoraAulaObrigatoria { get; set; }

        public short? QuantidadeHoraRelogioObrigatoria { get; set; }

        public short? QuantidadeHoraAulaOptativa { get; set; }

        public short? QuantidadeHoraRelogioOptativa { get; set; }

        public short? QuantidadeCreditoObrigatorio { get; set; }

        public short? QuantidadeCreditoOptativo { get; set; }

        public int? QuantidadeAssociadaHoraAulaObrigatoria { get; set; }

        public int? QuantidadeAssociadaHoraRelogioObrigatoria { get; set; }

        public int? QuantidadeAssociadaHoraAulaOptativa { get; set; }

        public int? QuantidadeAssociadaHoraRelogioOptativa { get; set; }

        public int? QuantidadeAssociadaCreditoObrigatorio { get; set; }

        public int? QuantidadeAssociadaCreditoOptativo { get; set; }
    }
}