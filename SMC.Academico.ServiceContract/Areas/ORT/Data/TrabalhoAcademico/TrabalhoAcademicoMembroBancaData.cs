using SMC.Academico.Common.Areas.APR.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoMembroBancaData : ISMCMappable
    {


        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        public List<SMCDatasourceItem> TiposMembroBanca { get; set; }

        public long? Seq { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long? SeqColaborador { get; set; }

        public long SeqAvaliacao { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public string Nome { get; set; }

        public string NomeColaborador { get; set; }

        public TipoMembroBanca TipoMembroBanca { get; set; }

        public string Instituicao { get; set; }

        public string NomeInstituicaoExterna { get; set; }

        public bool? Ativo { get; set; }

        public bool? Participou { get; set; }

        public string DescricaoMembro { get; set; }

		public string ComplementoInstituicao { get; set; }

        public Sexo? Sexo { get; set; }

        public bool AvaliacaoPossuiApuracao { get; set; }

        public bool? Presidiu { get; set; }

    }
}
