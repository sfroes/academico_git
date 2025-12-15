using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno
{
    public class CancelarMatriculaAlunoData : ISMCMappable
    {
        #region Parametros documentação EA (RN_MAT_083 - Cancelamento de matrícula)

        public string TokenSituacaoCancelamento { get; set; }

        public string ObservacaoSituacaoMatricula { get; set; }

        public DateTime DataReferencia { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public bool Jubilado { get; set; }

        public bool CancelarBeneficio { get; set; }

        public string ObservacaoCancelamentoSolicitacao { get; set; }

        public short TipoCancelamentoSGP { get; set; }

        #endregion

        #region Parametros auxiliares
        
        public long? SeqCicloLetivo { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public ArquivoAnexadoData ArquivoAnexado { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public int? CodigoAlunoSGP { get; set; }

        public string NumeroProtocolo { get; set; }

        #endregion
    }
}
