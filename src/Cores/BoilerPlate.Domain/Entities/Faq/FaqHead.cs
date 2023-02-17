using BoilerPlate.Shared.Abstraction.Entities;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Faq
{
    public class FaqHead : BaseEntity
    {
        public FaqHead()
        {
            FqHeadId = Guid.NewGuid();

            FaqContent = new HashSet<FaqContent>();
        }

        /// <summary>
        /// Primary key of the object.
        /// </summary>
        public Guid FqHeadId { get; set; }

        /// <summary>
        /// Foreign key to FaqContent. This will point to the currently used FaqContent
        /// </summary>
        public Guid FqActiveContentId { get; set; }

        public ICollection<FaqContent> FaqContent { get; set; }
    }

    public sealed class FaqHeadConfiguration : BaseEntityConfiguration<FaqHead>
    {
        protected override void EntityConfiguration(EntityTypeBuilder<FaqHead> builder)
        {
            builder.HasKey(e => e.FqHeadId);
            builder.Property(e => e.FqHeadId).ValueGeneratedNever();
            builder.HasIndex(e => e.FqHeadId);
            builder.HasIndex(e => e.FqActiveContentId);
        }
    }
}
