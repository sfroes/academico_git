using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class TrabalhoAcademicoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqInstituicaoLogada { get; set; }

        public List<long?> SeqsTipoTrabalho { get; set; }

        public long? SeqAreaConhecimento { get; set; }

        public long? SeqPrograma { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string Nome { get; set; }

        public List<TipoPesquisaTrabalhoAcademico> TipoPesquisaTrabalho { get; set; }

        public string TituloResumo { get; set; }

        public bool? PesquisaDetalhada { get; set; }

        public bool? EmPublicacao { get; set; }

        public bool? EmFuturasDefesas { get; set; }
    }
}
