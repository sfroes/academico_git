using iTextSharp.text;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoDocumentoAcademicoListaVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }      

        public List<GrupoDocumentoAcademico> GruposDocumentoAcademico {  get; set; }

        public List<string> ListaGruposDocumento
        {
            get
            {
                var list = new List<string>();
                foreach (var item in GruposDocumentoAcademico)
                {
                    list.Add(item.SMCGetDescription());
                }
                return list;
            }
        }
    }
}
