using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickerQ.EntityFrameworkCore.Entities;

namespace TickerQ.EntityFrameworkCore.Configurations
{
    public class TimeTickerConfigurations : IEntityTypeConfiguration<TimeTickerEntity>
    {
        public void Configure(EntityTypeBuilder<TimeTickerEntity> builder)
        {
            builder.HasKey("Id");

            builder.Property(x => x.LockHolder)
                .IsConcurrencyToken()
                .IsRequired(false);

            builder.HasOne(e => e.ParentJob)
                .WithMany(x => x.ChildJobs)
                .HasForeignKey(x => x.BatchParent)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.HasIndex("ExecutionTime")
                .HasName("IX_TimeTicker_ExecutionTime");

            builder.HasIndex("Status", "ExecutionTime")
                .HasName("IX_TimeTicker_Status_ExecutionTime");

            builder.ToTable("TimeTickers", "ticker");
        }
    }
}