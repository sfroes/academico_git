using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoBloqueioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public long SeqMotivoBloqueioAuxiliarEditar { get; set; }

        public long SeqMotivoBloqueioAuxiliarSalvar { get; set; }

        public string Descricao { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string Observacao { get; set; }

        public bool PermiteDesbloqueioTemporarioEfetivacao { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public TipoDesbloqueio? TipoDesbloqueio { get; set; }

        public string JustificativaDesbloqueio { get; set; }

        public DateTime? DataDesbloqueioTemporario { get; set; }

        public string DescricaoReferenciaAtuacao { get; set; }

        public string UsuarioDesbloqueioTemporario { get; set; }

        public DateTime? DataDesbloqueioEfetivo { get; set; }

        public string UsuarioDesbloqueioEfetivo { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        public MotivoBloqueio MotivoBloqueio { get; set; }

        public PessoaAtuacao PessoaAtuacao { get; set; }

        public IList<PessoaAtuacaoBloqueioComprovante> Comprovantes { get; set; }

        public IList<PessoaAtuacaoBloqueioItem> Itens { get; set; }

        public string DescricaoVinculo { get; set; }

        public long? NumeroIngressante { get; set; }

        public long? RegistroAcademico { get; set; }

        public string DescricaoTipoBloqueio { get; set; }
    }
}