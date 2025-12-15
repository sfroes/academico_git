using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class ConferenciaPublicacaoBdpVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public DateTime? DataDefesa { get; set; }

        public string TituloTrabalho { get; set; }

        public string TituloIdiomaTrabalho { get; set; }

        public string Titulo { get; set; }

        public string TipoTrabalho { get; set; }

        public SituacaoTrabalhoAcademico Situacao { get; set; }

        public List<string> Autores { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string OfertaCursoLocalidadeTurno { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoNivel { get; set; }
    }
}