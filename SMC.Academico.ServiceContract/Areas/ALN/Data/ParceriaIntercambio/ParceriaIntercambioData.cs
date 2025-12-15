using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public bool ProcessoNegociacao { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string InstituicaoEnsino { get; set; }

        public List<ParceriaIntercambioVigenciaData> Vigencias { get; set; }

        public List<ParceriaIntercambioTipoTermoData> TiposTermo { get; set; }

        public List<ParceriaIntercambioInstituicaoExternaData> InstituicoesExternas { get; set; }

        public List<ParceriaIntercambioArquivoData> Arquivos { get; set; }
    }
}
