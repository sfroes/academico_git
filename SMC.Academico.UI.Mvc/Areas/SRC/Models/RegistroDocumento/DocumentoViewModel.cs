using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DocumentoViewModel : SMCViewModelBase
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCHidden]
        public List<SMCDatasourceItem> SolicitacoesEntregaDocumento { get; set; }

        #endregion Data Sources

        [SMCHidden]
        public bool PermiteVarios { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumento { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public Sexo Sexo { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        public SMCMasterDetailList<DocumentoItemViewModel> Documentos { get; set; }

        [SMCHidden]
        public long SeqDocumentoRequerido { get; set; }

        [SMCHidden]
        public List<GrupoDocumentoViewModel> Grupos { get; set; }

        [SMCHidden]
        public bool PermiteEntregaPosterior { get; set; }

        [SMCHidden]
        public bool Obrigatorio { get; set; }

        [SMCHidden]
        public bool ObrigatorioUpload { get; set; }

        [SMCHidden]
        public SituacaoEntregaDocumentoLista SituacaoEntregaDocumentoLista
        {
            get
            {
                if (Documentos == null || !Documentos.Any() || Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoEntrega || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoValidacao || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Nenhum))
                    return SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Deferido || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Pendente || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Indeferido))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoIndeferido;
                else
                    throw new SMCApplicationException("Situação não tratada para o documento");
            }
        }

        public SituacaoEntregaDocumentoLista SituacaoTipoDocumento
        {
            get
            {
                if (Documentos == null || !Documentos.Any() || Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoEntrega || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoValidacao || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                    return SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Deferido || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Pendente))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Indeferido))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoIndeferido;
                else
                    throw new SMCApplicationException("Situação não tratada para o documento");
            }
        }
    }
}