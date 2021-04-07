using System;
using System.Runtime.Serialization;

namespace Terrasoft.Configuration
{
	[DataContract]
	public class BaseServiceResult
	{
		public BaseServiceResult() {}

		public BaseServiceResult(bool isSuccessful, string message = "")
		{
			IsSuccessful = isSuccessful;
			Message = message;
		}

		[DataMember(Name = "isSuccessful")]
		public bool IsSuccessful { get; set; }

		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}
