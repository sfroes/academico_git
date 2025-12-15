using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Models
{
    public class ComponenteCurricularViewModel : SMCViewModelBase, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoReduzida { get; set; }

        public string Sigla { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public bool Ativo { get; set; }

        public string Observacao { get; set; }

        public bool? ExigeAssuntoComponente { get; set; }

        public short? QuantidadeSemanas { get; set; }

        public short? NumeroVersaoCarga { get; set; }
    }
}