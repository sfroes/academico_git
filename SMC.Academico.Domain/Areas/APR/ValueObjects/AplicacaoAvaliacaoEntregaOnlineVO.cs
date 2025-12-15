using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class AplicacaoAvaliacaoEntregaOnlineVO : ISMCMappable
    {
        public long SeqAplicacaoAvaliacao { get; set; }

        #region Cabecalho

        public string DescricaoOrigemAvaliacao { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
        public short? QuantidadeMaximaPessoasGrupo { get; set; }
        public string Instrucao { get; set; }
        public long? SeqArquivoAnexadoInstrucao { get; set; }
        public Guid? UidArquivoAnexadoInstrucao { get; set; }
        public long SeqOrigemAvaliacao { get; set; }

        #endregion Cabecalho

        public List<EntregaOnlineVO> EntregasOnline { get; set; }
    }
}
