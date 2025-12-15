using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioListarData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public string Descricao { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public bool Ativo { get; set; }

        public bool ProcessoNegociacao { get; set; }
      
        public List<ParceriaIntercambioArquivoData> Arquivos { get; set; } 

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<string> TiposTermo { get; set; }

        public List<string> InstituicoesExternas { get; set; }

        public int TotalTermos { get; set; }

        public string InstituicaoEnsinoLogada { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}
 