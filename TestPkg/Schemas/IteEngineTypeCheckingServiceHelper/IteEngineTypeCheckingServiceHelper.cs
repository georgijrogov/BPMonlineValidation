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
		
		public IEnumerable<string> GetActualEngineTypes(string showroomId)
		{
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "IteCar");
			var engineTypeCol = esq.AddColumn("IteEngineType.Name");
			var esqFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "IteShowroom.Id", showroomId);
			esq.Filters.Add(esqFilter);
			var entities = esq.GetEntityCollection(_userConnection);
			var actualEngineTypes = entities.Select(x => x.GetTypedColumnValue<string>(engineTypeCol.Name)).Distinct();

			return actualEngineTypes;
		}
		
		public IEnumerable<string> GetAllEngineTypes()
		{
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "IteEngineType");
			var engineTypeCol = esq.AddColumn("Name");
			var entities = esq.GetEntityCollection(_userConnection);
			var allEngineTypes = entities.Select(x => x.GetTypedColumnValue<string>(engineTypeCol.Name));
			
			return allEngineTypes;
		}
		
		public string ValidateAllEngineTypes(string showroomId)
		{
			var actualEngineTypes = GetActualEngineTypes(showroomId);
			var allEngineTypes = GetAllEngineTypes();
			
			var missingEngineTypes = allEngineTypes.Except(actualEngineTypes);
			
			return missingEngineTypes.Any() ? string.Join(", ", missingEngineTypes) : string.Empty;
		}
	}
}