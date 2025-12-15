using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteCabecalhoVO : ISMCMappable
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

        public List<FormacoesEspecificasSolicitacaoMatriculaVO> FormacoesEspecificas { get; set; }

        public string NomeOrientador { get; set; }

        public bool ExigeCurso { get; set; }

        public bool ExibeFormacoesEspecificas { get; set; }

        public string DescricaoTipoOrientacao { get; set; }
    }
}