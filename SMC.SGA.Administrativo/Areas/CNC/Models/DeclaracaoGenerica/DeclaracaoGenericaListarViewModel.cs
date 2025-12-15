using System;
using SMC.Framework.DataAnnotations;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DeclaracaoGenericaListarViewModel : SMCDynamicViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCOrder(0)]        
        public long? NumeroRegistroAcademico { get; set; }

        [SMCOrder(1)]
        [SMCValueEmpty("-")]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCOrder(2)]
        [SMCSortable(true, false, "PessoaDadosPessoais.Nome")]
        public string NomeAlunoGrid {
            get
            {
                return !string.IsNullOrWhiteSpace(NomeSocialAluno)
                    ? $"{NomeSocialAluno} ({NomeAluno})"
                    : NomeAluno;
            }
        }

        [SMCOrder(3)]        
        [SMCValueEmpty("-")]
        public string DescricaoCursoOfertaLocalidade { get; set; }

        [SMCOrder(4)]
        [SMCSortable(true, false, "TipoDocumentoAcademico.Descricao")]
        public string TipoDocumento { get; set; }

        [SMCOrder(5)]
        [SMCSortable(true, false, "DataInclusao")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataEmissao { get; set; }


        [SMCOrder(6)]
        [SMCSortable(true, false, "SituacaoAtual.SituacaoDocumentoAcademico.Descricao")]        
        public string SituacaoAtual  { get; set; }

        [SMCHidden]
        public string NomeSocialAluno { get; set; }
        
        [SMCHidden]
        public string NomeAluno { get; set; }
    }
}