using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientadoresRelatorioData : ISMCMappable
    {
        public long SeqEntidade { get; set; }
        public string NomeEntidade { get; set; }
        public long SeqColaborador { get; set; }
        public string NomeOrientador { get; set; }
        public long SeqNivelEnsino { get; set; }
        public string NivelEnsino { get; set; }
        public string RAAluno { get; set; }
        public string NomeAluno { get; set; }
        public string CicloLetivo { get; set; }
        public string SituacaoMatricula { get; set; }
        public string InicioOrientacao { get; set; }
        public string FimOrientacao { get; set; }
    }
}