using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class TrabalhoAcademicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoTrabalhoComparacao { get; set; }

        public bool AlterarDataDepositoSecretaria { get; set; }

        public bool GeraFinanceiroEntregaTrabalho { get; set; }

        public bool ExisteAvaliacaoCadastrada { get; set; }

		public bool ExistePublicacaoBdp { get; set; }

        public bool LimparRegistrosAutorizacao { get; set; }

        public string Titulo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long SeqTipoTrabalho { get; set; }

        public DateTime? DataDepositoSecretaria { get; set; }

        public List<TrabalhoAcademicoAutoriaVO> Autores { get; set; }

        public List<TrabalhoAcademicoDivisaoComponenteVO> DivisoesComponente { get; set; }

        public List<BancaExaminadoraVO> MembrosBancaExaminadora { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public TipoTrabalho TipoTrabalho { get; set; }

        public bool? ExibeDuracao { get; set; }

        public bool? HabilitaDuracao { get; set; }

        public short? NumeroDiasDuracaoAutorizacaoParcial { get; set; }

        public bool HabilitaPotencial { get; set; }
        public bool ExibirPotencial { get; set; }
        public bool? PotencialPatente { get; set; }
        public bool? PotencialRegistroSoftware { get; set; }
        public bool? PotencialNegocio { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
    }
}