using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoCursoOfertaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculo { get; set; }

        public long SeqCursoOferta { get; set; }

        public bool Ativo { get; set; }

        public short? QuantidadeHoraAulaObrigatoria { get; set; }

        public short? QuantidadeHoraRelogioObrigatoria { get; set; }

        public short? QuantidadeHoraAulaOptativa { get; set; }

        public short? QuantidadeHoraRelogioOptativa { get; set; }

        public short? QuantidadeCreditoObrigatorio { get; set; }

        public short? QuantidadeCreditoOptativo { get; set; }

        public short? QuantidadeAssociadaHoraAulaObrigatoria { get; set; }

        public short? QuantidadeAssociadaHoraRelogioObrigatoria { get; set; }

        public short? QuantidadeAssociadaHoraAulaOptativa { get; set; }

        public short? QuantidadeAssociadaHoraRelogioOptativa { get; set; }

        public short? QuantidadeAssociadaCreditoObrigatorio { get; set; }

        public short? QuantidadeAssociadaCreditoOptativo { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public long? SeqCurso { get; set; }

        public string CursoSigla { get; set; }

        public string CursoDescricao { get; set; }

        public string CursoOfertaDescricao { get; set; }
    }
}