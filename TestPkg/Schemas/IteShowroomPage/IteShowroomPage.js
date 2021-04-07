define("IteShowroomPage", ["ServiceHelper"], function(ServiceHelper) {
	return {
		entitySchemaName: "IteShowroom",
		attributes: {},
		modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{
			"Files": {
				"schemaName": "FileDetailV2",
				"entitySchemaName": "IteShowroomFile",
				"filter": {
					"masterColumn": "Id",
					"detailColumn": "IteShowroom"
				}
			},
			"IteSchemaa5d02ebbDetail79ce229a": {
				"schemaName": "IteSchemaa5d02ebbDetail",
				"entitySchemaName": "IteCar",
				"filter": {
					"detailColumn": "IteShowroom",
					"masterColumn": "Id"
				}
			}
		}/**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
		methods: {
			asyncValidate: function(callback, scope) {
				this.callParent([function(response) {
					if (!this.validateResponse(response)) {
						return;
					}
					Terrasoft.chain(
						function(next) {
							this.validateEngineTypes(function(response) {
								if (this.validateResponse(response)) {
									next();
								}
							}, this);
						},
						function(next) {
							callback.call(scope, response);
							next();
						}, this);
				}, this]);
			},
			validateEngineTypes: function(callback, scope) {
				var showroomId = this.get("Id");
					// Объект, инициализирующий входящие параметры для метода сервиса.
				var serviceData = {
					// Название свойства совпадает с именем входящего параметра метода сервиса.
					showroomId: showroomId
				};
				ServiceHelper.callService("IteEngineTypeCheckingService", "EngineTypesValidation", function(response) {
					var result = {success: true};
					if (response && response.EngineTypesValidationResult){
						var responseResult = response.EngineTypesValidationResult;
						if (responseResult && responseResult.message === "") {
							this.showInformationDialog(this.get("Resources.Strings.successCaption"));
							callback.call(scope || this, result);
							return;
						} else {
							var failure = this.get("Resources.Strings.failureCaption");
							result.message = this.showInformationDialog(failure.replace("{0}", responseResult.message));
							result.success = false;
						}
					}
					callback.call(scope || this, result);
				},serviceData , this);
			},
		},
		dataModels: /**SCHEMA_DATA_MODELS*/{}/**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/[
			{
				"operation": "insert",
				"name": "IteNamec379ac2a-0123-4373-9f7b-ed5c3b2bf5dc",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "ProfileContainer"
					},
					"bindTo": "IteName"
				},
				"parentName": "ProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Tabc40f57d7TabLabel",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Tabc40f57d7TabLabelTabCaption"
					},
					"items": [],
					"order": 0
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "IteSchemaa5d02ebbDetail79ce229a",
				"values": {
					"itemType": 2,
					"markerValue": "added-detail"
				},
				"parentName": "Tabc40f57d7TabLabel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "remove",
				"name": "ESNTab"
			},
			{
				"operation": "remove",
				"name": "ESNFeedContainer"
			},
			{
				"operation": "remove",
				"name": "ESNFeed"
			}
		]/**SCHEMA_DIFF*/
	};
});
