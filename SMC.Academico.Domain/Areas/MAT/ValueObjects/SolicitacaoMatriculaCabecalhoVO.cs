using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaCabecalhoVO : ISMCMappable
    {
        public string DescricaoProcesso { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoModalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoCurso { get; set; }

        public List<FormacoesEspecificasSolicitacaoMatriculaVO> FormacoesEspecificas { get; set; }

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