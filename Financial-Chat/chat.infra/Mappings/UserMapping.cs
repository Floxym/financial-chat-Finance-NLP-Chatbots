
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Mappings
{
    public sealed class UserMapping : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
