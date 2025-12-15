using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string NivelEnsino { get; set; }

        public string TipoTermoIntercambio { get; set; }

        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public bool Ativo { get; set; }

        public long SeqInstituicaoEnsinoExterna { get; set; }

        public string InstituicaoEnsinoExterna { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<ParceriaIntercambioArquivoData> Arquivos { get; set; }

        public List<TermoIntercambioTipoMobilidadeData> TiposMobilidade { get; set; }
    }
}