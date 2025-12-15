using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SMC.Academico.ServiceContract.Data
{
    [DataContract(Namespace = NAMESPACES.MODEL, IsReference = true)]
    public class EnderecoEletronicoData : ISMCMappable
    {
        [SMCMapProperty("SeqEnderecoEletronico")]
        public long Seq { get; set; }

        [DataMember]
        public virtual string Descricao { get; set; }

        [DataMember]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        public CategoriaEnderecoEletronico? CategoriaEnderecoEletronico { get; set; }

        [SMCMapForceFromTo]
        public string DescricaoTipoEnderecoEletronico 
        { 
            get
            {
                var descricao = this.TipoEnderecoEletronico.SMCGetDescription();
                if (this.CategoriaEnderecoEletronico.HasValue && this.CategoriaEnderecoEletronico != Common.Enums.CategoriaEnderecoEletronico.Nenhum)
                    descricao = descricao + " - " + this.CategoriaEnderecoEletronico.SMCGetDescription();
                return descricao;
            }

            set 
            {
                // Split realizado de outra forma devido ao tipo de endereço eletrônico E-mail.
                var descricaoSplit = value.Split().SMCRemove(c => c.Equals("-"));

                // Se a descrição tiver exatamente dois itens significa que o endereço eletrônico tem uma categoria
                if (descricaoSplit.SMCCount() > 1)
                {
                    // Necessário tratamento para tipo de endereço eletrônico E-mail
                    string tipoEndEletronico = descricaoSplit.FirstOrDefault().Replace("-", string.Empty).Trim().SMCTrimInside();
                    // Encoding usado para remover caracteres especiais quando a categoria for 'Coordenação' sem afetar as outras categorias
                    string categoriaEndEletronico = Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(descricaoSplit.LastOrDefault().Trim().SMCTrimInside()));

                    this.TipoEnderecoEletronico = SMCEnumHelper.GetEnumSafely<TipoEnderecoEletronico>(tipoEndEletronico);
                    this.CategoriaEnderecoEletronico = SMCEnumHelper.GetEnumSafely<CategoriaEnderecoEletronico>(categoriaEndEletronico);
                }
                else
                {
                    // Necessário tratamento para tipo de endereço eletrônico E-mail
                    string tipoEndEletronico = descricaoSplit.FirstOrDefault().Replace("-", string.Empty).Trim().SMCTrimInside();
                    this.TipoEnderecoEletronico = SMCEnumHelper.GetEnumSafely<TipoEnderecoEletronico>(tipoEndEletronico);
                    this.CategoriaEnderecoEletronico = null;
                }
            }
        }
    }
}
