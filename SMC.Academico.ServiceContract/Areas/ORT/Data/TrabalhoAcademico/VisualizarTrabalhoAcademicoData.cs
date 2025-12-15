using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class VisualizarTrabalhoAcademicoData : ISMCMappable
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

        public List<TrabalhoAcademicoMembroBancaData> BancaExaminadora { get; set; }

        public DateTime? DataDefesa { get; set; }

        public DateTime? DataPrevistaDefesa { get; set; }

        public string Local { get; set; }

        public List<string> Telefones { get; set; }

        public List<TrabalhoAcademicoIdiomasData> InformacoesEstrangeiras { get; set; }

        public List<PublicacaoBdpArquivoData> Arquivos { get; set; }

        public SituacaoTrabalhoAcademico? Situacao { get; set; }
    }

}
