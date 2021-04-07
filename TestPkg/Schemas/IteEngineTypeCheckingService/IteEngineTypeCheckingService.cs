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

	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class IteEngineTypeCheckingService: BaseService
	{
		private IteEngineTypeCheckingServiceHelper helper;
		private IteEngineTypeCheckingServiceHelper Helper
		{
			get => helper ?? (helper = new IteEngineTypeCheckingServiceHelper(UserConnection));
		}
		
		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
		ResponseFormat = WebMessageFormat.Json)]
		public BaseServiceResult EngineTypesValidation(string showroomId)
		{
			try
			{
				string response = Helper.ValidateAllEngineTypes(showroomId);
				BaseServiceResult result = new BaseServiceResult(string.IsNullOrEmpty(response), response);
				
				return result;
			}
			catch (Exception ex)
			{
				BaseServiceResult result = new BaseServiceResult(false, ex.Message);
				
				return result;
			}
		}
	}
}