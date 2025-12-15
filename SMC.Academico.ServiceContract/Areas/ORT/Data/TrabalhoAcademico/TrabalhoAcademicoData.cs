using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoTrabalhoComparacao { get; set; }

        public bool AlterarDataDepositoSecretaria { get; set; }

        public bool GeraFinanceiroEntregaTrabalho { get; set; }

        public bool ExisteAvaliacaoCadastrada { get; set; }

        public bool ExistePublicacaoBdp { get; set; }

        public string Titulo { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public List<TrabalhoAcademicoDivisaoComponenteData> DivisoesComponente { get; set; }

        public long SeqTipoTrabalho { get; set; }

        public DateTime? DataDepositoSecretaria { get; set; }

        public List<TrabalhoAcademicoAutoriaData> Autores { get; set; }

        public List<TrabalhoAcademicoMembroBancaData> MembrosBancaExaminadora { get; set; }

        public bool ExibeDuracao { get; set; }

        public bool? HabilitaDuracao { get; set; }

        public bool LimparRegistrosAutorizacao { get; set; }

        public short? NumeroDiasDuracaoAutorizacaoParcial { get; set; }

        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public string JustificativaSegundoDeposito { get; set; }

        public string UsuarioInclusaoSegundoDeposito { get; set; }

        public DateTime DataInclusaoSegundoDeposito { get; set; }

        public SMCUploadFile ArquivoAnexadoSegundoDeposito { get; set; }

        public bool HabilitaPotencial { get; set; }
        public bool ExibirPotencial { get; set; }

        public bool? PotencialPatente { get; set; }
        public bool? PotencialRegistroSoftware { get; set; }
        public bool? PotencialNegocio { get; set; }
    }
}