using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class CabecalhoPublicacaoBdpVO : ISMCMappable
    {
        public long SeqTrabalhoAcademico { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public DateTime? DataSituacao { get; set; }

        public string EntidadeResponsavel { get; set; }

        public long SeqEntidadeResponsvel { get; set; }

        public string OfertaCursoLocalidadeTurno { get; set; }

        public List<string> AreaConhecimento { get; set; }

        public string AreaConcentracao { get; set; }

        public SituacaoTrabalhoAcademico Situacao { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public bool BloqueiaAlteracoes { get; set; }
    }
}
