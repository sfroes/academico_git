using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoListarItemViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqDivisaoCurricularItemDetalhe { get; set; }

        [SMCHidden]
        public long? SeqComponenteCurricularDetalhe { get; set; }

        [SMCHidden]
        public OutroRequisito? OutroRequisito { get; set; }

        [SMCHidden]
        public long? SeqGrupoCurricular { get; set; }

        [SMCHidden]
        public bool? GrupoPertenceCurriculoMatrizOferta { get; set; }

        [SMCHidden]
        public short? QuantidadeOutroRequisito { get; set; }

        [SMCHidden]
        public string DescricaoRequisitoDivisao { get; set; }

        [SMCHidden]
        public string DescricaoRequisitoComponente { get; set; }

        [SMCHidden]
        public string DescricaoRequisitoOutro { get; set; }

        [SMCHidden]
        public string DescricaoRequisitoGrupoCurricular { get; set; }

        [SMCDescription]
        public string DescricaoRequisitoItem
        {
            get
            {
                if (SeqDivisaoCurricularItemDetalhe.HasValue)
                    return DescricaoRequisitoDivisao;

                if (SeqComponenteCurricularDetalhe.HasValue)
                    return DescricaoRequisitoComponente;

                if (OutroRequisito.HasValue && OutroRequisito.Value != Academico.Common.Areas.CUR.Enums.OutroRequisito.Nenhum)
                    return DescricaoRequisitoOutro;

                if (SeqGrupoCurricular.HasValue)
                    return DescricaoRequisitoGrupoCurricular;

                return string.Empty;
            }
        }
    }
}