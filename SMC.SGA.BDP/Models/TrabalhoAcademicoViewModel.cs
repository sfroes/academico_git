using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SMC.SGA.BDP.Models
{
    public class TrabalhoAcademicoViewModel : SMCViewModelBase
    {
        public string ViewTitle { get => Titulo; }

        public string TipoTrabalho { get; set; }

        public string TipoTrabalhoNivelEnsino { get => TipoTrabalho + " de " + NivelEnsino; }

        public string NivelEnsino { get; set; }

        public string Titulo { get; set; }

        public List<string> Autores { get; set; }

        public string Programa { get; set; }

        public List<string> AreaConhecimento { get; set; }

        public string AreaConcentracao { get; set; }

        public List<string> Orientadores { get; set; }

        public List<string> Coorientadores { get; set; }

        public List<BancaExaminadoraViewModel> BancaExaminadora { get; set; }

        public DateTime? DataDefesa { get; set; }

        public DateTime? DataPrevistaDefesa { get; set; }

        public string Local { get; set; }

        public List<string> Telefones { get; set; }

        public List<string> PalavrasChave { get; set; }

        public string Resumo { get; set; }

        public List<TrabalhoAcademicoIdiomasViewModel> InformacoesEstrangeiras { get; set; }

        public List<TrabalhoAcademicoArquivosViewModel> Arquivos { get; set; }

        public SituacaoTrabalhoAcademico? Situacao { get; set; }
    }

    public class TrabalhoAcademicoIdiomasViewModel : SMCViewModelBase
    {
        public SMCLanguage Idioma { get; set; }

        public string Titulo { get; set; }

        public List<string> PalavrasChave { get; set; }

        public string Resumo { get; set; }
    }

    public class TrabalhoAcademicoArquivosViewModel : SMCViewModelBase
    {
        public TipoAutorizacao TipoAutorizacao { get; set; }

        public string NomeArquivo { get; set; }        

        public string UrlArquivo { get; set; }
    }

    public class BancaExaminadoraViewModel : SMCViewModelBase
    {
        public string Nome { get; set; }

        public string Instituicao { get; set; }

        public string ComplementoInstituicao { get; set; }

        public string InstituicaoFormatada
        {
            get
            {
                return string.IsNullOrEmpty(ComplementoInstituicao) ? Instituicao : $"{Instituicao} - {ComplementoInstituicao}";
            }
        }
    }
}