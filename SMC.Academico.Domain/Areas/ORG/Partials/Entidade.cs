using SMC.Academico.Common.Areas.ORG.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Models
{
    public partial class Entidade
    {
        /// <summary>
        /// Retorna a situação atual da entidade
        /// </summary>
        [NotMapped]
        public SituacaoEntidade SituacaoAtual
        {
            get
            {
                //Conversado com a Ellen quando não tem a situação atual, pode ser que esta cadastrando para uma data futura ou a data o período já ocorreu 
                //sempre considerar a entidade como Ativa. Só será considerado Inativa se estiver explícito na sua situação atual
                if (this.HistoricoSituacoes?.FirstOrDefault(s => s.Atual)?.SituacaoEntidade == null)
                    return new SituacaoEntidade() { CategoriaAtividade = CategoriaAtividade.Ativa };
                else
                    return this.HistoricoSituacoes?.FirstOrDefault(s => s.Atual)?.SituacaoEntidade;
            }
        }

        /// <summary>
        /// Sequencial da situação atual
        /// </summary>
        private long? _SeqSituacaoAtual;

        [NotMapped]
        public long SeqSituacaoAtual
        {
            get { return _SeqSituacaoAtual.HasValue ? _SeqSituacaoAtual.Value : (SituacaoAtual?.Seq ?? 0); }
            set { _SeqSituacaoAtual = value; }
        }

        /// <summary>
        /// Sequencia do ItemHierarquiaEntidade da entidade superior utilizada na gravação de entidades externadas
        /// </summary>
        [NotMapped]
        public long SeqHierarquiaEntidadeItemSuperior { get; set; }

        /// <summary>
        /// Data inicial da situação atual
        /// </summary>
        private DateTime? _DataInicioSituacaoAtual;

        [NotMapped]
        public DateTime DataInicioSituacaoAtual
        {
            get { return _DataInicioSituacaoAtual.HasValue ? _DataInicioSituacaoAtual.Value : this.HistoricoSituacoes?.FirstOrDefault(s => s.Atual)?.DataInicio ?? default(DateTime); }
            set { _DataInicioSituacaoAtual = value; }
        }

        /// <summary>
        /// Data final da situação atual
        /// </summary>
        private DateTime? _DataFimSituacaoAtual;

        [NotMapped]
        public DateTime? DataFimSituacaoAtual
        {
            get { return _DataFimSituacaoAtual.HasValue ? _DataFimSituacaoAtual.Value : this.HistoricoSituacoes?.FirstOrDefault(s => s.Atual)?.DataFim; }
            set { _DataFimSituacaoAtual = value; }
        }

        /// <summary>
        /// Categoria de atividade da situação atual
        /// </summary>
        private CategoriaAtividade? _CategoriaAtividadeSituacaoAtual;

        [NotMapped]
        public CategoriaAtividade CategoriaAtividadeSituacaoAtual
        {
            get { return _CategoriaAtividadeSituacaoAtual.HasValue ? _CategoriaAtividadeSituacaoAtual.Value : this.HistoricoSituacoes?.FirstOrDefault(s => s.Atual)?.SituacaoEntidade?.CategoriaAtividade ?? CategoriaAtividade.Nenhum; }
            set { _CategoriaAtividadeSituacaoAtual = value; }
        }

        /// <summary>
        /// Verifica se a situação atual é ativa.
        /// A entidade será considerada como inativa apenas se a situação atual for inativa.
        /// Caso não tenha uma situação atual (por exemplo a data atual é maior que o intervalo da última situação) também será considerada como ativa.
        /// </summary>
        /// <returns>Se a situação atual não é inativo</returns>
        public bool VerificarSituacaoAtualAtiva()
        {
            SituacaoEntidade situacaoAtual = this.SituacaoAtual;
            return situacaoAtual == null || situacaoAtual.CategoriaAtividade != CategoriaAtividade.Inativa;
        }
    }
}