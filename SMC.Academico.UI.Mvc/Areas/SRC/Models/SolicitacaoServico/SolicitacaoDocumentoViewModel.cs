using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDocumentoViewModel : SMCViewModelBase
    {

        [SMCHidden]
        public bool PermiteVarios { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public long SeqDocumentoRequerido { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumento { get; set; }

        [SMCIgnoreProp]
        public List<GrupoDocumentoViewModel> Grupos { get; set; }

        [SMCIgnoreProp]
        public bool Obrigatorio { get; set; }

        [SMCIgnoreProp]
        public bool PermiteEntregaPosterior { get; set; }

        [SMCIgnoreProp]
        public DateTime? DataLimiteEntrega { get; set; }

        [SMCDetail]
        public SMCMasterDetailList<SolicitacaoDocumentoDocumentoViewModel> Documentos { get; set; }
        

        //retirada propriedade para correção de exibição de legenda em upload de arquivo
        //[SMCIgnoreProp]
        //public SituacaoEntregaDocumentoLista SituacaoEntregaDocumentoLista
        //{
        //    get
        //    {

        //            if (Documentos == null || !Documentos.Any() || Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoEntrega || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoValidacao || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Nenhum || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
        //                return SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
        //            else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Deferido || d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Pendente))
        //                return SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
        //            else if (Documentos.Any(d => d.SituacaoEntregaDocumento == DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.Indeferido))
        //                return SituacaoEntregaDocumentoLista.RegistroDocumentoIndeferido;
        //            else
        //                throw new SMCApplicationException("Situação não tratada para o documento");
        //    }
        //}

        [SMCIgnoreProp]
        public SituacaoEntregaDocumentoLista SituacaoEntregaDocumentoLista { get; set; }

        public SituacaoEntregaDocumentoLista SituacaoEntregaDocumentoListaAluno { get; set; } 

        public SituacaoEntregaDocumentoAluno Situacao
        {
            get
            {
                if (Documentos != null && Documentos.Any() && Documentos.All(f => f.SeqArquivoAnexado.HasValue))
                    return SituacaoEntregaDocumentoAluno.RegistroDocumentoOK;
                return SituacaoEntregaDocumentoAluno.AguardandoRegistroDocumento;
            }
        }

        public string DescricaoEtapa { get; set; }
    }
}
