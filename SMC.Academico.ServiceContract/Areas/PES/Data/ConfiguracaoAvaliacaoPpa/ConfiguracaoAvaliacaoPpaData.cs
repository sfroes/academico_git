using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
    {
    /// <summary>
    /// Configuração de Avaliação.
    /// </summary>
    public class ConfiguracaoAvaliacaoPpaData : ISMCMappable
    {
        #region Primitive Properties
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoAvaliacaoPpa TipoAvaliacaoPpa { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public Nullable<long> SeqNivelEnsino { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public Nullable<System.DateTime> DataFimVigencia { get; set; }

        public Nullable<System.DateTime> DataLimiteRespostas { get; set; }

        public Nullable<int> CodigoOrigemPpa { get; set; }

        public Nullable<int> CodigoInstrumentoPpa { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public Nullable<System.DateTime> DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        public Nullable<long> SeqCicloLetivo { get; set; }

        public Nullable<int> CodigoAvaliacaoPpa { get; set; }

        public Nullable<int> CodigoAplicacaoQuestionarioSgq { get; set; }

        public Nullable<int> SeqTipoInstrumentoPpa { get; set; }

        public Nullable<int> SeqEspecieAvaliadorPpa { get; set; }

        public Nullable<bool> CargaRealizada { get; set; }

        public string ParteFixaNomeAvaliacao { get; set; }

        #endregion Primitive Properties

    }
}
