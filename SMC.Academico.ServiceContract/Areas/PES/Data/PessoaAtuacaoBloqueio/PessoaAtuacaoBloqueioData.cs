using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCMapProperty("MotivoBloqueio.TipoBloqueio.Seq")]
        public long SeqTipoBloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.Seq")]
        public long SeqMotivoBloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.Seq")]
        public long SeqMotivoBloqueioAuxiliarEditar { get; set; }

        public long SeqMotivoBloqueioAuxiliarSalvar { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public bool PermiteDesbloqueioTemporarioEfetivacao { get; set; }

        public string DescricaoReferenciaAtuacao { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataCriacao { get; set; }

        [SMCMapProperty("DataAlteracao")]
        public DateTime? UltimaAtualizacao { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string ResponsavelBloqueio { get; set; }

        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        public DateTime? DataDesbloqueioTemporario { get; set; }

        public string UsuarioDesbloqueioTemporario { get; set; }

        public DateTime? DataDesbloqueioEfetivo { get; set; }

        public string UsuarioDesbloqueioEfetivo { get; set; }

        public string JustificativaDesbloqueio { get; set; }

        public List<PessoaAtuacaoBloqueioCompovanteData> Comprovantes { get; set; }

        public List<PessoaAtuacaoBloqueioItemData> Itens { get; set; }

        public virtual MotivoBloqueioData MotivoBloqueio { get; set; }

        public string DescricaoVinculo { get; set; }

        public long? NumeroIngressante { get; set; }

        public long? RegistroAcademico { get; set; }

        [SMCMapProperty("MotivoBloqueio.PermiteItem")]
        public bool ExibirItensBloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.Descricao")]
        public string DescricaoMotivoBloqueio { get; set; }

        public bool CadastroIntegracao { get; set; }

        public string DescricaoTipoBloqueio { get; set; }
    }
}