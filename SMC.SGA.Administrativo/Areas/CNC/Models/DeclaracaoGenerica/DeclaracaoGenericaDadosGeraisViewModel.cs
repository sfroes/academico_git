using System;
using SMC.Framework.DataAnnotations;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc;
using iTextSharp.text;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DeclaracaoGenericaDadosGeraisViewModel : SMCViewModelBase, ISMCMappable
    {
        #region Dados do Aluno

        [SMCHidden]
        public long? SeqPessoaDadosPessoais { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24)]
        public long? NumeroRegistroAcademico { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCHidden]
        public string Nome {  get; set; }
        
        [SMCHidden]
        public string NomeSocial {  get; set; }

        //TODO: Tratar para incluir nome Social
        //OBS: NV02 - A ordenação não considera o nome social da pessoa
        [SMCDisplay]
        [SMCSize(SMCSize.Grid7_24)]
        public string NomeAluno
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NomeSocial)
                    ? $"{NomeSocial} ({Nome})"
                    : Nome;
            }
        }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCCpf]
        [SMCSize(SMCSize.Grid4_24)]
        public string Cpf { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCCpf]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroPassaporte { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public string NivelEnsino { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid19_24)]
        public string Vinculo { get;set; }

        #endregion
                
        #region Dados do Documento

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid8_24)]
        public string TipoDocumento { get; set; }

        ////O comando visualizar deverá ser exibido com um link para abrir a consulta do respectivo documento do GAD,
        ////conforme o UC_DOC_001_07_01 - Visualizar Documento (Acadêmico).

        [SMCHidden]
        public string TokenTipoDocumento { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqDocumentoGAD { get; set; }

        [SMCHidden]
        public string UrlDocumentoGAD { get; set; }

        #endregion

        #region Situações do Documento
        
        [SMCSize(SMCSize.Grid24_24)]
        public List<DeclaracaoGenericaHistoricoListarViewModel> Situacoes { get; set; }

        #endregion
    }
}