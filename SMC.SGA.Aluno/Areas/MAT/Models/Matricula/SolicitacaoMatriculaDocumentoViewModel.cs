using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class SolicitacaoMatriculaDocumentoViewModel : SMCViewModelBase
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
        public bool PermiteUploadArquivo { get; set; }

        [SMCIgnoreProp]
        public bool PermiteEntregaPosterior { get; set; }

        public SMCMasterDetailList<SolicitacaoDocumentoDocumentoViewModel> Documentos { get; set; }


        [SMCIgnoreProp]
        public SituacaoEntregaDocumentoLista SituacaoEntregaDocumentoLista
        {
            get
            {
                if (Documentos == null || !Documentos.Any() || Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Nenhum))
                    return SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                else if (Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido))
                    return SituacaoEntregaDocumentoLista.RegistroDocumentoIndeferido;
                else
                    throw new SMCApplicationException("Situação não tratada para o documento");
            }
        }

        public SituacaoEntregaDocumentoAluno Situacao
        {
            get
            {
                if (Documentos != null && !PermiteUploadArquivo && Documentos.Any(d => d.ArquivoAnexado == null))
                    return SituacaoEntregaDocumentoAluno.EntregaPresencialmente;
                if (Documentos != null && Documentos.Any(d => d.ArquivoAnexado != null))
                    return SituacaoEntregaDocumentoAluno.RegistroDocumentoOK;
                return SituacaoEntregaDocumentoAluno.AguardandoRegistroDocumento;
            }
        }

        [SMCIgnoreProp]
        public int NumeroDocumentosDeferidoAguardandoEntrega
        {
            get
            {
                if (Documentos == null || !Documentos.Any())
                {
                    return 0;
                }
                else
                {
                    return Documentos.Count(c => c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel);
                }
            }
        }
    }
}