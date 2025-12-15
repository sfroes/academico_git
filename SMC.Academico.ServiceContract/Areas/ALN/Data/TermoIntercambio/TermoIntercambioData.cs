using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{

    public class TermoIntercambioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public string Descricao { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }

        public long SeqParceriaIntercambioTipoTermo { get; set; }

        public string NameDescriptionParceriraIntercabioTipoTermo { get; set; }

        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public List<TermoIntercambioVigenciaData> Vigencias { get; set; }

        public List<TermoIntercambioTipoMobilidadeData> TiposMobilidade { get; set; }

        public List<TermoIntercambioArquivoData> Arquivos { get; set; }

        public bool PossuiPessoaAtuacao { get; set; }

        public bool ConcedeFormacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool ExibeVigencias { get; set; }
    }
}
