using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FormacaoAcademicaInsertedData : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqPessoaAtuacao { get; set; }

        public string Titulacao { get; set; }

        public string DescricaoTitulacao { get; set; }

        public int? AnoInicio { get; set; }
        
        public int? AnoObtencaoTitulo { get; set; }

        public string TitulacaoMaxima { get; set; }

        public string Curso { get; set; }

        public string Instituicao { get; set; }

        public short? QuantidadeMinima { get; set; }        

        public short? QuantidadeMaxima { get; set; }

        public long? SeqHierarquiaClassificacao { get; set; }        
        
        public List<long> DocumentosApresentados { get; set; }

        public Sexo Sexo { get; set; }


    }
}