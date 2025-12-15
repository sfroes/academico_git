using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class RgVO : ISMCMappable
    {
        public string Numero { get; set; }
        public string Uf { get; set; } //enum AC, AL, AM, AP, BA, CE, DF, ES, GO, MA, MG, MS, MT, PA, PB, PE, PI, PR, RJ, RN, RO, RR, RS, SC, SE, SP, TO
        public string OrgaoExpedidor { get; set; }
    }
}
