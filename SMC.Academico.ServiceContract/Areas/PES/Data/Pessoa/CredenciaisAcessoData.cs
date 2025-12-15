using SMC.Framework.Mapper;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    [DataContract]
    public class CredenciaisAcessoData : ISMCMappable
    {
        [DataMember]
        public short Ordem { get; set; }

        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public string TextoCredencial { get; set; }

        [DataMember]
        public CredenciaisAcessoBotaoData[] Botoes { get; set; }

        [DataMember]
        public string MensagemTrocaSenha { get; set; }

        [DataMember]
        public string LinkTrocaSenha { get; set; }

        /// <summary>
        ///As propriedades ImagemCredencial estão hospedadas no sharepoint e são gerenciadas pela equipe de design.
        //Elas são usadas na tela de credenciais do novo aplicativo do pucmobile.
        //Caso seja necessário alterações nessas, será necessário contactar os designers para a realização da alteração.
        /// </summary>
        [DataMember]
        public string ImagemCredencial { get; set; }

        [DataMember]
        public bool TrocaSenhaTelaInterna { get; set; }

        [DataMember]
        public CredenciaisAcessoBotaoData[] BotoesDeManual { get; set; }

        [DataMember]
        public bool SuporteTecnico { get; set; }
    }
}