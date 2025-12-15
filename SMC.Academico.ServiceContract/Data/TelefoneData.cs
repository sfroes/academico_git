using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Linq;
using System.Text;

namespace SMC.Academico.ServiceContract.Data
{
    public class TelefoneData : ISMCMappable
    {
        [SMCMapProperty("SeqTelefone")]
        public long Seq { get; set; }

        public int? CodigoPais { get; set; }

        public int? CodigoArea { get; set; }

        public string Numero { get; set; }
                
        public TipoTelefone TipoTelefone { get; set; } 

        public string NomeContato { get; set; }

        public bool? Preferencial { get; set; }

        public CategoriaTelefone? CategoriaTelefone { get; set; }

        /// <summary>
        /// Descricao do Select usado na model - Em alguns casos o telefone terá categoria, e isso fica armazenado na descrição do select.
        /// </summary>
        [SMCMapForceFromTo]
        public string DescricaoTipoTelefone
        {
            get 
            {
                var descricao = this.TipoTelefone.SMCGetDescription();
                if (this.CategoriaTelefone.HasValue && this.CategoriaTelefone != Common.Enums.CategoriaTelefone.Nenhum)
                    descricao = descricao + " - " + this.CategoriaTelefone.SMCGetDescription();
                return descricao;
            }

            set
            {
                var descricaoTelSplit = value.Split('-');
                // Se a descrição tiver exatamente dois itens significa que o telefone tem uma categoria
                if (descricaoTelSplit.SMCCount() > 1)
                {
                    string tipoTelefone = descricaoTelSplit.FirstOrDefault().Trim().SMCTrimInside();
                    // Encoding usado para remover caracteres especiais quando a categoria for 'Matrículas' sem afetar as outras categorias
                    string categoriaTelefone = Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(descricaoTelSplit.LastOrDefault().Trim().SMCTrimInside()));
                    this.TipoTelefone = SMCEnumHelper.GetEnumSafely<TipoTelefone>(tipoTelefone);
                    this.CategoriaTelefone = SMCEnumHelper.GetEnumSafely<CategoriaTelefone>(categoriaTelefone);
                }
                else
                {
                    string tipoTelefone = descricaoTelSplit.FirstOrDefault().Trim().SMCTrimInside();
                    this.TipoTelefone = SMCEnumHelper.GetEnumSafely<TipoTelefone>(tipoTelefone);
                    this.CategoriaTelefone = null;
                }
            }
        }
    }
}
