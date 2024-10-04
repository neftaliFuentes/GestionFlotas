using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.dataaccess
{
	public partial class FlotasContext : DbContext, IDisposable
	{
		private readonly IConfiguration _configuration;
		private IDbConnection DbConnection { get; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(DbConnection.ToString());
				//this.ChangeTracker.LazyLoadingEnabled = true;
			}

		}
		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
		}
	}
	public class BlankTriggerAddingConvention : IModelFinalizingConvention
	{
		public virtual void ProcessModelFinalizing(
			IConventionModelBuilder modelBuilder,
			IConventionContext<IConventionModelBuilder> context)
		{
			foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
			{
				var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
				if (table != null
					&& entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null)
					&& (entityType.BaseType == null
						|| entityType.GetMappingStrategy() != RelationalAnnotationNames.TphMappingStrategy))
				{
					entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
				}

				foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
				{
					if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
					{
						entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
					}
				}
			}
		}
	}
}
