using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ContratoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public string NumeroRegistro { get; set; }

        public string Descricao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }         

        public long? SeqCurso { get; set; }
    } 
}
