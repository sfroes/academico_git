using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoCursoOfertaCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqCurriculo { get; set; }

        [SMCHidden]
        public long SeqCursoOferta { get; set; }

        [SMCHidden]
        public string CurriculoCodigo { get; set; }

        [SMCHidden]
        public string CurriculoDescricao { get; set; }

        [SMCHidden]
        public string CurriculoDescricaoComplementar { get; set; }

        [SMCHidden]
        public long? SeqCurso { get; set; }

        [SMCHidden]
        public string CursoSigla { get; set; }

        [SMCHidden]
        public string CursoDescricao { get; set; }

        [SMCHidden]
        public bool Ativo { get; set; }

        [SMCDescription]
        public string CursoOfertaDescricao { get; set; }

        public string CursoCompleto
        {
            get
            {
                return string.Format("{0:d4} - {1}", SeqCurso, CursoDescricao);

                //if (string.IsNullOrEmpty(CursoSigla))
                //    return $"{CursoDescricao}";
                //else
                //    return $"{CursoSigla} - {CursoDescricao}";
            }
        }

        public string CursoOfertaCompleto
        {
            get
            {
                return string.Format("{0:d4} - {1}", SeqCursoOferta, CursoOfertaDescricao);
            }
        }

        public string CurriculoCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(CurriculoDescricaoComplementar))
                    return $"{CurriculoCodigo} - {CurriculoDescricao}";
                else
                    return $"{CurriculoCodigo} - {CurriculoDescricao} - {CurriculoDescricaoComplementar}";
            }
        }

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

        [SMCHidden]
        public bool QuantidadeAssociadaHoraAulaObrigatoriaDestacada { get { return QuantidadeHoraAulaObrigatoria.GetValueOrDefault() < QuantidadeAssociadaHoraAulaObrigatoria.GetValueOrDefault(); } }

        [SMCHidden]
        public bool QuantidadeAssociadaHoraRelogioObrigatoriaDestacada { get { return QuantidadeHoraRelogioObrigatoria.GetValueOrDefault() < QuantidadeAssociadaHoraRelogioObrigatoria.GetValueOrDefault(); } }

        [SMCHidden]
        public bool QuantidadeAssociadaHoraAulaOptativaDestacada { get { return QuantidadeHoraAulaOptativa.GetValueOrDefault() < QuantidadeAssociadaHoraAulaOptativa.GetValueOrDefault(); } }

        [SMCHidden]
        public bool QuantidadeAssociadaHoraRelogioOptativaDestacada { get { return QuantidadeHoraRelogioOptativa.GetValueOrDefault() < QuantidadeAssociadaHoraRelogioOptativa.GetValueOrDefault(); } }

        [SMCHidden]
        public bool QuantidadeAssociadaCreditoObrigatorioDestacada { get { return QuantidadeCreditoObrigatorio.GetValueOrDefault() < QuantidadeAssociadaCreditoObrigatorio.GetValueOrDefault(); } }

        [SMCHidden]
        public bool QuantidadeAssociadaCreditoOptativoDestacada { get { return QuantidadeCreditoOptativo.GetValueOrDefault() < QuantidadeAssociadaCreditoOptativo.GetValueOrDefault(); } }
    }
}