using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaItensCursadosOutrasInstituicoesData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoDispensa { get; set; }

        public string Instituicao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string Dispensa { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }
              
        public string NomeProfessor { get; set; }

        public long? SeqTitulacaoProfessor { get; set; }

        public decimal? Nota { get; set; }

        public string Conceito { get; set; }

        public string Observacao { get; set; }
    }
}