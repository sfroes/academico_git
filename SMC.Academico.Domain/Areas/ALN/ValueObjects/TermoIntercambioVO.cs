using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio")]
        public long SeqTipoTermoIntercambio { get; set; }

        public long SeqParceriaIntercambioTipoTermo { get; set; }

        public string NameDescriptionParceriraIntercabioTipoTermo { get; set; }

        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public List<TermoIntercambioVigenciaVO> Vigencias { get; set; }

        public List<TermoIntercambioTipoMobilidadeVO> TiposMobilidade { get; set; }

        public List<TermoIntercambioArquivoVO> Arquivos { get; set; }

        public bool PossuiPessoaAtuacao { get; set; }

        public bool ConcedeFormacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool ExibeVigencias { get; set; }
    }
}
