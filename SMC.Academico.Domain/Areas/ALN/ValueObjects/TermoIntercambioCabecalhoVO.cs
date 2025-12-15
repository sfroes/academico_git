using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioCabecalhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public string Parceria { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public List<ParceriaIntercambioTipoTermoVO> TiposTermo { get; set; }

        public bool ProcessoNegociacao { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<string> InstituicoesExternas { get; set; }

        public List<ParceriaIntercambioVigencia> Vigencias { get; set; }
    }
}