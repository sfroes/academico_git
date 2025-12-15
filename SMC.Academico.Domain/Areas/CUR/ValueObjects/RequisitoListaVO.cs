using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class RequisitoListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public string CodigoComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public short? CreditoComponenteCurricular { get; set; }

        public short? CargaHorariaComponenteCurricular { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public long? SeqNivelComponenteCurricular { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public short? NumeroDivisaoCurricularItem { get; set; }

        public string DescricaoTipoDivisaoCurricular { get; set; }

        public string DescricaoDivisaoCurricularItem { get; set; }

        public bool UnicaMatrizAssociada { get; set; }

        public TipoRequisitoAssociado Associado { get; set; }

        public List<RequisitoItemVO> Itens { get; set; }

        public List<MatrizCurricularCabecalhoVO> MatrizesCurriculares { get; set; }
    }
}