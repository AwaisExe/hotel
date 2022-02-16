using DOMAIN.Entities;
using INFRASTRUCTURE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace INFRASTRUCTURE.Configurations
{
    public abstract class BaseConfiguration<T> : IBaseEntityConfiguration where T : class
    {
        protected readonly ModelBuilder _modelBuilder;
        public static IEnumerable<System.Security.Claims.ClaimsIdentity> _claims;
        protected EntityTypeBuilder<T> _builder => _modelBuilder.Entity<T>();
        protected BaseConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
    }
}
