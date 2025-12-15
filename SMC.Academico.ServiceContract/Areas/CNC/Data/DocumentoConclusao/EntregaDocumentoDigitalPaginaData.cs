using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class EntregaDocumentoDigitalPaginaData : ISMCMappable
    {
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        public string Cpf { get; set; }

        public string NomeAluno { get; set; }

        public string NomeSocial { get; set; }

        public Sexo Sexo { get; set; }

        public DateTime DataNascimento { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public string UfIdentidade { get; set; }

        public string NumeroPassaporte { get; set; }

        public string NomePaisEmissaoPassaporte { get; set; }

        public string DescricaoNacionalidade { get; set; }

        public string Naturalidade { get; set; }

        public List<EntregaDocumentoDigitalFiliacaoPaginaData> Filiacao { get; set; }

        public List<string> FiliacaoDisplay { get; set; }

        public List<EntregaDocumentoDigitalDocumentoConclusaoPaginaData> DocumentosConclusao { get; set; }

        public bool? ConfirmacaoAluno { get; set; }

        public string Observacao { get; set; }

        public List<EntregaDocumentoDigitalUploadPaginaData> DocumentosUpload { get; set; }
    }
}
