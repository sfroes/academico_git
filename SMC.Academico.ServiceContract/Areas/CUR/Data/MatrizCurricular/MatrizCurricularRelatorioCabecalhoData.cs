using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularRelatorioCabecalhoData : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoSituacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }
    }
}