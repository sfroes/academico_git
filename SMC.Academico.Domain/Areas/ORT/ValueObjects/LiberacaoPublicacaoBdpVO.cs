using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class LiberacaoPublicacaoBdpVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public List<TrabalhoAcademicoAutoriaVO> Alunos { get; set; }

        public short? QuantidadeVolumes { get; set; }

        public short? QuantidadePaginas { get; set; }

        public TipoAutorizacao? TipoAutorizacao { get; set; }

        public List<PublicacaoBdpIdiomaVO> Idiomas { get; set; }

        public List<PublicacaoBdpArquivoVO> Arquivos { get; set; }

        public string Titulo { get; set; }
    }
}
