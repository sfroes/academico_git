using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public string Parceria { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public List<ParceriaIntercambioTipoTermoData> TiposTermo { get; set; }

        public bool ProcessoNegociacao { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<string> InstituicoesExternas { get; set; }

        public List<ParceriaIntercambioVigenciaData> Vigencias { get; set; }
    }
}