using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string NivelEnsino { get; set; }

        public string TipoTermoIntercambio { get; set; }

        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public bool Ativo { get; set; }

        public long SeqInstituicaoEnsinoExterna { get; set; }

        public string InstituicaoEnsinoExterna { get; set; }

        public int CodigoPaisInstituicaoEnsinoExterna { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<ParceriaIntercambioArquivoVO> Arquivos { get; set; }

        public List<TermoIntercambioTipoMobilidadeVO> TiposMobilidade { get; set; }

        public List<string> Orientadores { get; set; }
        public List<string> TiposParticipacaoOrientacao { get; set; }
    }
}