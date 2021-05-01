namespace Terrasoft.Configuration.IteEngineTypeCheckingService
{
	using System;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.ServiceModel.Activation;
	using Terrasoft.Core;
	using Terrasoft.Web.Common;
	using Terrasoft.Core.Entities; 
	using System.Collections.Generic;

	public class IteEngineTypeCheckingServiceHelper
	{
		private readonly UserConnection _userConnection;
		public IteEngineTypeCheckingServiceHelper(UserConnection userConnection)
		{
			_userConnection = userConnection;
		}
		
		public IEnumerable<EngineType> GetActualEngineTypes(string showroomId)
		{
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "IteCar");
			esq.IsDistinct = true;
			var engineTypeIdCol = esq.AddColumn("IteEngineType.Id");
			var engineTypeNameCol = esq.AddColumn("IteEngineType.Name");
			var esqFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "IteShowroom.Id", showroomId);
			esq.Filters.Add(esqFilter);
			var entities = esq.GetEntityCollection(_userConnection);
			var actualEngineTypes = entities.Select(x => new EngineType { Id = x.GetTypedColumnValue<Guid>(engineTypeIdCol.Name),
				Name = x.GetTypedColumnValue<string>(engineTypeNameCol.Name)});

			return actualEngineTypes;
		}
		
		public IEnumerable<EngineType> GetAllEngineTypes()
		{
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "IteEngineType");
			var engineTypeIdCol = esq.AddColumn("Id");
			var engineTypeNameCol = esq.AddColumn("Name");
			var entities = esq.GetEntityCollection(_userConnection);
			var allEngineTypes = entities.Select(x => new EngineType { Id = x.GetTypedColumnValue<Guid>(engineTypeIdCol.Name),
				Name = x.GetTypedColumnValue<string>(engineTypeNameCol.Name)});
			
			return allEngineTypes;
		}
		
		public string ValidateAllEngineTypes(string showroomId)
		{
			var actualEngineTypes = GetActualEngineTypes(showroomId);
			var allEngineTypes = GetAllEngineTypes();
			
			var missingEngineTypes = allEngineTypes.Where(x => !actualEngineTypes.Select(i => i.Id).Contains(x.Id));
			var missingEngineTypesNames = missingEngineTypes.Select(x => x.Name);

			return missingEngineTypes.Any() ? string.Join(", ", missingEngineTypesNames) : string.Empty;
		}
	}
	public class EngineType
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}