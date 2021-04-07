define("IteShowroomSection", ["ConfigurationEnums"], function(ConfigurationEnums) {
	return {
		entitySchemaName: "IteShowroom",
		details: /**SCHEMA_DETAILS*/{}/**SCHEMA_DETAILS*/,
		diff: /**SCHEMA_DIFF*/[]/**SCHEMA_DIFF*/,
		methods: {
			editRecord: function(primaryColumnValue) {
				var activeRow = this.getActiveRow();
				var typeColumnValue = this.getTypeColumnValue(activeRow);
				var schemaName = this.getEditPageSchemaName(typeColumnValue);
				this.set("ShowCloseButton", true);
				window.open(Terrasoft.combinePath(Terrasoft.workspaceBaseUrl,"Nui/ViewModule.aspx#CardModuleV2",
					schemaName, ConfigurationEnums.CardStateV2.EDIT, primaryColumnValue));
			}
		}
	};
});
