using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class PublicacaoBdpVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public DateTime? DataPublicacao { get; set; }

        public short? QuantidadeVolumes { get; set; }

        public short? QuantidadePaginas { get; set; }

        public long? CodigoAcervo { get; set; }

        public DateTime? DataAutorizacao { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataDefesa { get; set; }

        public TipoAutorizacao? TipoAutorizacao { get; set; }

        public Guid? CodigoAutorizacao { get; set; }
  
        public List<PublicacaoBdpArquivoVO> Arquivos { get; set; }

        public TrabalhoAcademicoVO TrabalhoAcademico { get; set; }

        public List<PublicacaoBdpHistoricoSituacaoVO> HistoricoSituacoes { get; set; }

        public List<PublicacaoBdpIdiomaVO> InformacoesIdioma { get; set; }

        public string DescricaoTipoTrabalho { get; set; }

        public string DescricaoTituloTrabalho { get; set; }

        public SituacaoTrabalhoAcademico UltimaSituacaoTrabalho { get; set; }

        public string NomeReduzidaInstituicao { get; set; }
    }
}