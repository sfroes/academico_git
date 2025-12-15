using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoArquivoDigital : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("XML Diploma Digital")]
        XmlDiplomaDigital = 1,

        [EnumMember]
        [Description("Representação Visual Diploma Digital")]
        RepresentacaoVisualDiplomaDigital = 2,

        [EnumMember]
        [Description("Relatório Manifesto Assinaturas Diploma Digital")]
        RelatorioManifestoAssinaturasDiplomaDigital = 3,

        [EnumMember]
        [Description("XML Histórico Escolar")]
        XMLHistoricoEscolar = 4,

        [EnumMember]
        [Description("Representação Visual Histórico Escolar")]
        RepresentacaoVisualHistoricoEscolar = 5,
        
        [EnumMember]
        [Description("Relatório Manifesto Assinaturas Histórico Escolar")]
        RelatorioManifestoAssinaturasHistoricoEscolar = 6
    }
}