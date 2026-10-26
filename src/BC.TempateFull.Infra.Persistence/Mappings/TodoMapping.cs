using Ds.FleetManagement.Domain.ToDo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace BC.TempateFull.Infra.Persistence.Mappings;

public class TodoMapping : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.OptionType);
        builder.Property(x => x.Total);
        builder.OwnsOne(x => x.Title, title =>
        {
            title.HasIndex(x => x.Value).IsUnique();
            title.Property(x => x.Value)
                .HasColumnName("Title")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Value, value =>
        {
            value.Property(x => x.Value)
                .HasColumnName("Value")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Tag, tag =>
        {
            tag.Property(x => x.Value)
                .HasColumnName("Tag")
                .HasMaxLength(20)
                .IsRequired(false);
        });

        builder.OwnsMany(x => x.Items, item =>
        {
            item.WithOwner().HasForeignKey("TodoId");
            item.ToTable("TodoItems");
            item.HasKey(x => x.Id);

            item.OwnsOne(n => n.Name, value =>
            {
                value.Property(p => p.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(256)
                .IsRequired();
            });

            


        });

        builder.Navigation(x => x.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}