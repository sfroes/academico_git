using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long NumeroRegistroAcademico { get; set; }

		public DateTime? Data { get; set; }

		public DateTime? DataDefesa { get; set; }

		public bool GeraFinanceiroEntregaTrabalho { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public string Titulo { get; set; }

        public string TituloPortugues { get; set; }

        public string TipoTrabalho { get; set; }

        public List<string> Autores { get; set; }

		public List<TrabalhoAcademicoAutorListaData> Alunos { get; set; }

		public List<string> NomesAutores { get; set; }

        public List<string> Orientadores { get; set; }

        public List<string> Coorientadores { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string OfertaCursoLocalidadeTurno { get; set; }

        public SituacaoTrabalhoAcademico? Situacao { get; set; }

        public long SeqTipoTrabalho { get; set; }

        public string DescricaoTipoTrabalho { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string OrdenacaoAutor { get; set; }

        public string OrdnacaoOrientador { get; set; }

        public string ResumoPortugues { get; set; }

        public int? NumeroDefesa { get; set; }

        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public string JustificativaSegundoDeposito { get; set; }

        public string UsuarioInclusaoSegundoDeposito { get; set; }

        public DateTime? DataInclusaoSegundoDeposito { get; set; }

        public SMCUploadFile ArquivoAnexadoSegundoDeposito { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public bool PublicacaoBibliotecaObrigatoria { get; set; }
        public string ProtocoloSolicitacaoInclusao { get; set; }

    }
}