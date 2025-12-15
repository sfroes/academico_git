using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class TrabalhoAcademicoViewModel : SMCViewModelBase
    {
        public string TipoTrabalho { get; set; }

        public string NivelEnsino { get; set; }

        public string Titulo { get; set; }

        [SMCDetail]
        public SMCMasterDetailList<TrabalhoAcademicoAutoriaViewModel> Autores { get; set; }

        public string Programa { get; set; }

        public List<string> AreaConhecimento { get; set; }
        
        public string AreaConcentracao { get; set; }

        public List<string> Orientadores { get; set; }

        public List<string> Coorientadores { get; set; }

        public List<BancaExaminadoraViewModel> MembrosBancaExaminadora { get; set; }

        public DateTime? DataDefesa { get; set; }

        public DateTime? DataPrevistaDefesa { get; set; }

        public string Local { get; set; }

        public List<string> Telefones { get; set; }

        public List<string> PalavrasChave { get; set; }

        public string Resumo { get; set; }

        public List<TrabalhoAcademicoIdiomasViewModel> InformacoesEstrangeiras { get; set; }

        public TipoAutorizacao? TipoAutorizacao { get; set; }

        public string NomeArquivo { get; set; }

        public string UrlArquivo { get; set; }

        public SituacaoTrabalhoAcademico? Situacao { get; set; }

        public short? NumeroDiasDuracaoAutorizacaoParcial { get; set; }
    }

    public class TrabalhoAcademicoIdiomasViewModel : SMCViewModelBase
    {
        public SMCLanguage Idioma { get; set; }

        public string Titulo { get; set; }

        public List<string> PalavrasChave { get; set; }

        public string Resumo { get; set; }
    }

    public class BancaExaminadoraViewModel : SMCViewModelBase
    {
        public string Nome { get; set; }

        public string Instituicao { get; set; }

        public TipoMembroBanca TipoMembroBanca { get; set; }
    }

    public class TrabalhoAcademicoAutoriaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqTrabalhoAcademicoAutoria { get; set; }

        [SMCHidden]
        public long SeqAluno { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeAutor { get; set; }

        [SMCHidden(SMCViewMode.Edit)]
        public string NomeAutorFormatado { get; set; }

        [SMCEmail]
        [SMCSize(SMCSize.Grid12_24)]        
        public string EmailAutor { get; set; }
    }
}