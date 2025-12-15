using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class PublicacaoBdpViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAutorizacoes { get; set; }

        [SMCHidden]
        [SMCKey]
        [SMCParameter]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivel { get; set; }

        public DateTime? DataPublicacao { get; set; }

        [SMCMinValue(1)]
        public short? QuantidadeVolumes { get; set; }

        [SMCMinValue(1)]
        public short? QuantidadePaginas { get; set; }

        public long? CodigoAcervo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataAutorizacao { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataDefesa { get; set; }

        [SMCSelect]
        public TipoAutorizacao? TipoAutorizacao { get; set; }

        public Guid? CodigoAutorizacao { get; set; }
                
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PublicacaoBdpArquivoViewModel> Arquivos { get; set; }

        public TrabalhoAcademicoViewModel TrabalhoAcademico { get; set; }

        public List<PublicacaoBdpHistoricoSituacaoViewModel> HistoricoSituacoes { get; set; }

        [SMCDetail(SMCDetailType.Modal, min: 2, windowSize: SMCModalWindowSize.Large)]
        public SMCMasterDetailList<PublicacaoBdpIdiomaViewModel> InformacoesIdioma { get; set; }

        public SituacaoTrabalhoAcademico UltimaSituacaoTrabalho { get; set; }

        public string DescricaoTipoTrabalho { get; set; }

        public bool ExibirAutorizacao { get; set; }
    }
}