using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParceriaIntercambioListarVO : ISMCMappable
    {
        //public ParceriaIntercambioListarVO()
        //{
        //    this.InstituicoesExternas = new List<string>();
        //    this.TiposTermo = new List<string>();
        //}
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public bool Ativo { get; set; }

        public bool ProcessoNegociacao { get; set; }

        public List<ParceriaIntercambioArquivoVO> Arquivos { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [SMCMapProperty("TiposTermo")]
        public List<string> TiposTermo { get; set; }

        [SMCMapProperty("InstituicoesExternas")]
        public List<string> InstituicoesExternas { get; set; }

        public int TotalTermos { get; set; }

        public string InstituicaoEnsinoLogada { get; set; }
    }
}
