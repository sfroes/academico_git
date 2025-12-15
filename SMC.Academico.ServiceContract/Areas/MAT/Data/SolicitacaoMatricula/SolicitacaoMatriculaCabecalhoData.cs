using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaCabecalhoData : ISMCMappable
    {
        public string DescricaoProcesso { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        //public string DescricaoCicloLetivo { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoModalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoCurso { get; set; }

        public List<FormacoesEspecificasSolicitacaoMatriculaData> FormacoesEspecificas { get; set; }

        public string NomeOrientador { get; set; }

        public bool ExigeCurso { get; set; }

        public bool ExibeFormacoesEspecificas { get; set; }

        public string DescricaoTipoOrientacao { get; set; }

        public bool ExibeEntidadeResponsavelEVinculo { get; set; }

        public bool ExibeContatoSecretariaAcademica { get; set; }

        public string ContatoSecretariaAcademica { get; set; }

        public bool ExibeContatoCoordenacaoCurso { get; set; }

        public string ContatoCoordenacaoCurso { get; set; }

        public bool ExibeContatoSetorMatricula { get; set; }

        public string ContatoSetorMatricula { get; set; }
    }
}