using BoilerPlate.Domain.Entities.Faq;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Faq
{
    public class FaqContent : BaseEntity
    {
        public FaqContent()
        {
            FqContentId = Guid.NewGuid();
        }

        /// <summary>
        /// Primary key of the object.
        /// </summary>
        public Guid FqContentId { get; set; }

        /// <summary>
        /// Title of the Faq.
        /// </summary>
        public string? FqTitle { get; set; }

        /// <summary>
        /// Content of the Faq.
        /// </summary>
        public string? FqContent { get; set; }

        /// <summary>
        /// Foreign key to FaqHead. This hopefully can make making Faq history easier
        /// </summary>
        public Guid FqHeadId { get; set; }

        public virtual FaqHead FaqHead { get; set; }
    }


    public sealed class FaqContentConfiguration : BaseEntityConfiguration<FaqContent>
    {
        protected override void EntityConfiguration(EntityTypeBuilder<FaqContent> builder)
        {
            builder.HasKey(e => e.FqContentId);
            builder.Property(e => e.FqContentId).ValueGeneratedNever();
            builder.Property(e => e.FqTitle).HasMaxLength(100);
            builder.HasIndex(e => e.FqTitle);
            builder.HasIndex(e => e.FqHeadId);
            builder.Property(e => e.FqHeadId).ValueGeneratedNever();
        }
    }
}