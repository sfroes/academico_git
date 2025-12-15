using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica
{
    public class DeclaracaoGenericaDadosGeraisData : ISMCMappable
    {
        #region DadosAluno

        public long? NumeroRegistroAcademico { get; set; } //RA
        public int? CodigoAlunoMigracao { get; set; }
        public string Nome { get; set; } //N_PES_023 - Nome e Nome Social - Visão Administrativo
        public string NomeSocial { get; set; }
        public string Cpf { get; set; }
        public virtual string NumeroPassaporte { get; set; }
        public long? SeqPessoaDadosPessoais { get; set; } //Dados do aluno -> Comando
        public string NivelEnsino { get; set; }
        public string Vinculo { get; set; } //Exibir o campo "Descrição da pessoa-atuação" da pessoa-atuação em questão

        #endregion

        #region Dados do Documento
        public long Seq { get; set; } //ID
        public string TipoDocumento { get; set; }

        //O comando visualizar deverá ser exibido com um link para abrir a consulta do respectivo documento do GAD,
        //conforme o UC_DOC_001_07_01 - Visualizar Documento (Acadêmico).
        public long? SeqDocumentoGAD { get; set; }

        #endregion

        #region Situações do Documento
        public List<DeclaracaoGenericaHistoricoListarData> Situacoes { get; set; }

        #endregion


    }
}
