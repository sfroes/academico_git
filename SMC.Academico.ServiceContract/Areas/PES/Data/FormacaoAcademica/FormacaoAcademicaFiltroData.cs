using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FormacaoAcademicaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

      
    }
}