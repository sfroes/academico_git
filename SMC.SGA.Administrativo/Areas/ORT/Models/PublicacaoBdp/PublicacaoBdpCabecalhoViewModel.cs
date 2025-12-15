using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpCabecalhoViewModel : SMCViewModelBase
    {
        public long SeqTrabalhoAcademico { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public string EntidadeResponsavel { get; set; }

        public DateTime? DataSituacao { get; set; }

        public string OfertaCursoLocalidadeTurno { get; set; }

        public List<string> AreaConhecimento { get; set; }

        public string AreaConcentracao { get; set; }

        [SMCHidden]
        public SituacaoTrabalhoAcademico Situacao { get; set; }

        public string DescricaoSituacao { get => Situacao.SMCGetDescription(); }

        [SMCHidden]
        public bool BloqueiaAlteracoes { get; set; }
    }
}