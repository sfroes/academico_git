using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqColaborador { get; set; }

        [SMCHidden]
        public long SeqColaboradorVinculo { get { return this.Seq; } }

        [SMCHidden]
        public long SeqEntidadeVinculo { get; set; }

        [SMCHidden]
        public bool EntidadeResponsavelAcessivelFiltroDados { get; set; }

        [SMCDescription]
        [SMCInclude("EntidadeVinculo")]
        [SMCMapProperty("EntidadeVinculo.Nome")]
        [SMCSize(SMCSize.Grid24_24)]
        public string NomeEntidadeVinculo { get; set; }

        [SMCInclude("TipoVinculoColaborador")]
        [SMCMapProperty("TipoVinculoColaborador.Descricao")]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoTipoVinculoColaborador { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public bool InseridoPorCarga { get; set; }

        public List<ColaboradorVinculoCursoListarViewModel> Cursos { get; set; }
    }
}