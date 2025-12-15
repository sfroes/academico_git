using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarComponenteData : ISMCMappable
    {
        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public short CargaHoraria { get; set; }

        public short Creditos { get; set; }

        public short Nota { get; set; }

        public string DescricaoConceito { get; set; }

        public short Faltas { get; set; }

        public string DescricaoSituacaoFinal { get; set; }
    }
}