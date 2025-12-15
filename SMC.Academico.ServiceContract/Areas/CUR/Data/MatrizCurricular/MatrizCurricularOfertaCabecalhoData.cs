using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularOfertaCabecalhoData : ISMCMappable
    {       
        public long Seq { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public string MatrizCodigo { get; set; }

        public string MatrizDescricao { get; set; }

        public string MatrizDescricaoComplementar { get; set; }

        public string Codigo { get; set; }

        public string DescricaoUnidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }
      
    }
}
