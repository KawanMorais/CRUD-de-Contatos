

using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeContatos.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            // 1. Define a chave primária real do Contato (O Id dele)
            builder.HasKey(x => x.Id);

            // 2. Cria o VÍNCULO (Relacionamento):
            builder.HasOne(x => x.Usuario);       // Um contato tem UM Usuário;


        }
    }
}
