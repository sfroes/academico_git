using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RegistroDocumentosAtendimentoDocumentoViewModel
    {

        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCHidden]
        public List<SMCDatasourceItem> SolicitacoesEntregaDocumento { get; set; }

        #endregion Data Sources
        
        [SMCHidden]
        public string DescricaoTipoDocumento { get; set; }

        [SMCHidden]
        public bool PermiteVarios { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public Sexo Sexo { get; set; }

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

        [SMCDetail(SMCDetailType.Block)]
        public SMCMasterDetailList<DocumentoAtendimentoViewModel> Documentos { get; set; }

    }
}