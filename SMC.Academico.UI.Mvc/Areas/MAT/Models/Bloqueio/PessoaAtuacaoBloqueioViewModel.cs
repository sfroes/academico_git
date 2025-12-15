using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class PessoaAtuacaoBloqueioViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoBloqueio { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public bool PermiteDesbloqueioTemporarioEfetivacao { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public DateTime? UltimaAtualizacao { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string ResponsavelBloqueio { get; set; }

        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        public DateTime? DataDesbloqueioTemporario { get; set; }

        public string UsuarioDesbloqueioTemporario { get; set; }

        public DateTime? DataDesbloqueioEfetivo { get; set; }

        public string UsuarioDesbloqueioEfetivo { get; set; }

        public string JustificativaDesbloqueio { get; set; }

        public List<PessoaAtuacaoBloqueioCompovanteViewModel> Comprovantes { get; set; }

        public virtual MotivoBloqueioViewModel MotivoBloqueio { get; set; }

        public string DescricaoVinculo { get; set; }

        public long? NumeroIngressante { get; set; }

        public long? RegistroAcademico { get; set; }

        public string DescricaoReferenciaAtuacao { get; set; }

        public string DescricaoTipoBloqueio { get; set; }
    }
}