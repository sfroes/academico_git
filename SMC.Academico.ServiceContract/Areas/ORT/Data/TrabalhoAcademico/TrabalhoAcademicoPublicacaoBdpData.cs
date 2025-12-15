using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoPublicacaoBdpData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public string Titulo { get; set; }

        public List<TrabalhoAcademicoAutoriaData> Alunos { get; set; }

        public string TipoTrabalho { get; set; }

        public DateTime? DataDefesa { get; set; }

        public DateTime? DataEntrega { get; set; }

        public short? QuantidadeVolumes { get; set; }

        public short? QuantidadePaginas { get; set; }

        public long? CodigoAcervo { get; set; }
		
		public SMCUploadFile ArquivoAnexadoAtaDefesa { get; set; }

		public List<TrabalhoAcademicoMembroBancaData> Banca { get; set; }

        public List<PublicacaoBdpIdiomaData> Idiomas { get; set; }

        public List<PublicacaoBdpArquivoData> Arquivos { get; set; }

        public List<PublicacaoBdpAutorizacaoData> Autorizacoes { get; set; }

    }
}
