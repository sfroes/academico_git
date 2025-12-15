using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class RelatorioBolsistasFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoLogada { get; set; }

        public DateTime? DataInicioReferencia { get; set; }

        public DateTime? DataFimReferencia { get; set; }

        public List<TipoAtuacao> TipoAtuacoes { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public List<long> SeqsBeneficios { get; set; }

        public SituacaoChancelaBeneficio SituacaoBeneficio { get; set; }

        public bool ExibirParcelasEmAberto { get; set; }

        public bool ExibirReferenciaContrato { get; set; }

        public List<short> SeqsTipoAtuacao
        {
            get
            {
                List<short> lista = new List<short>();

                if (this.TipoAtuacoes == null) { return lista; }

                foreach (var item in this.TipoAtuacoes)
                    lista.Add((short)item);
                return lista;
            }
        }

        public long? SeqCicloLetivoIngresso { get; set; }
        public long? SeqNivelEnsino { get; set; }
    }
}
