using System.Collections.Generic;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CabecalhoAtendimentoPadraoData : ISMCMappable
    {
        public string Protocolo { get; set; }

        public string NomeSolicitante { get; set; }

        public string NomeSocialSolicitante { get; set; }

        public string DescricaoProcesso { get; set; }

        public string CriadoPor { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DadosVinculo { get; set; }

        public long SeqPessoaAtuacao { get; set; }
    }
}