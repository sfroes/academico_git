using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class VisualizarTrabalhoAcademicoVO : ISMCMappable
    {
        public string TipoTrabalho { get; set; }

        public string NivelEnsino { get; set; }

        public string Titulo { get; set; }

        public List<string> Autores { get; set; }

        public string Programa { get; set; }

        public List<string> AreaConhecimento { get; set; }

        public string AreaConcentracao { get; set; }

        public List<string> Orientadores { get; set; }

        public List<string> Coorientadores { get; set; }

        public List<BancaExaminadoraVO> BancaExaminadora { get; set; }

        public DateTime? DataDefesa { get; set; }

        public DateTime? DataPrevistaDefesa { get; set; }

        public string Local { get; set; }

        public List<string> Telefones { get; set; }

        public List<Telefone> TelefonesComercial { get; set; }

        public List<TrabalhoAcademicoIdiomasVO> InformacoesEstrangeiras { get; set; }

        public List<PublicacaoBdpArquivoVO> Arquivos { get; set; }

        public long? SeqPublicacaoBdp { get; set; }

        public TipoAutorizacao? TipoAutorizacaoBDP { get; set; }

        public SituacaoTrabalhoAcademico? Situacao { get; set; }

        // Suporte
        public bool PossuiPublicacaoBDP { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public string TituloIdiomaTrabalho { get; set; }
    }

    public class TrabalhoAcademicoIdiomasVO
    {
        public SMCLanguage Idioma { get; set; }

        public bool IdiomaTrabalho { get; set; }

        public string Titulo { get; set; }

        public List<string> PalavrasChave { get; set; }

        public string Resumo { get; set; }
    }

    public class BancaExaminadoraVO
    {
        public string Nome { get; set; }

        public string Instituicao { get; set; }

        public string ComplementoInstituicao { get; set; }

        public TipoMembroBanca TipoMembroBanca { get; set; }
    }
}
