using System.Collections.Generic;
using WebApplication2.Models.Responses;
using WebApplication2.Models.Responses.ActionForm;

namespace WebApplication2.Models.Responses.ActionForm
{
    public enum EColumnFixed
    {
        No = 0,
        ToTheLeft = 1,
        ToTheRight = 2,
    }

    public enum EOrderType
    {
        Undefined = 0,
        Ascending = 1,
        Descending = 2,
    }

#pragma warning disable SA1300 // MX code list
    public enum FormType
    {
        end,
        start,
        other,
    }

    /// <summary>
    /// Code list  is used to determine the type of control in action form (field VisualizationType).
    /// Visualization type determines how to visualize the data with determining the type of control used.
    ///
    /// Every visualization type can be used only with proper combination of Value type and Value data type of data.
    /// Possible combinations are listed in documentation on Confluence:
    /// https://jira.mxframe.net:8443/display/IW/10.+Data+Type+Representation.
    ///
    /// Some controls are used to visualize data, and other controls are used to group different sets of data.
    /// Listed types determine only controls used to visualize data.
    ///
    /// Controls that are of one of visalization types MUST CONTAIN atribute Properties.
    /// List of properties to set is listed in documentation on Confluence:
    /// https://jira.mxframe.net:8443/display/IW/2.+Extra+Parameters+Form+Dialog.
    /// </summary>
#pragma warning restore SA1300 // MX code list

    public enum VisualizationType
    {
        Graph,
        Table,
        File,
        Dropdown,
        RadioGroup,
        DropdownMultiple,
        List,
        Checkbox,
        Switch,
        Datebox,
        Numberbox,
        Textbox,
        RangeSelector,
        RangeSlider,
        Color,
        Image,
        CronEditor,
        IntervalEditor,
        MonacoEditor,
        Tab,
        Group,
        TimeSelection,
        Button,
        Label,
        TreeList,
        ElectronicSignature,
        HtmlEditor,
        HtmlViewer,
        Barcode,
        ScanInfo,
        ButtonsWithActions,
        WorkUnitStatusDisplay,
        EquipmentStatusCard,
        Weighing,
        DynamicFilter,
    }

#pragma warning disable SA1300 // MX code list
    public enum ActionFormEventPropertiesUrlParametersType
    {
        body,
        queryString,
    }
#pragma warning restore SA1300 // MX code list

    // https://jira.mxframe.net:8443/pages/viewpage.action?pageId=60263152
#pragma warning disable SA1300 // MX code list
    public enum ActionFormValidationType
    {
        greater,
        lower,
        required,
    }
#pragma warning restore SA1300 // MX code list

#pragma warning disable SA1300 // MX code list
    public enum ActionFormEventType
    {
        action,
        refreshTrigger,
        apiCall,
        getForm,
        apiCallSecondary,
    }
#pragma warning restore SA1300 // MX code list

#pragma warning disable SA1300 // MX code list
    public enum EditingMode
    {
        row,
        batch,
        cell,
        form,
        popup,
    }
#pragma warning restore SA1300 // MX code list

    /// <summary>
    /// Standard model for returning extra parameter form from cdr.
    /// </summary>
    public class ActionFormQueryDTO
    {
        public string Id { get; set; }
        public bool IsWizard { get; set; } = false;
        public FormType? FormType { get; set; }
        public string Title { get; set; }
        public int? Width { get; set; }
        public List<ActionFormControl> Controls { get; set; }
        public List<ActionFormEvent> Events { get; set; }
        public List<ActionFormValidation> Validations { get; set; }
        public object DefaultValues { get; set; }
        public Dictionary<string, string> DynamicValues { get; set; }
        public string CancelEndpoint { get; set; }
        public string CancelConfirmationMessage { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? CompactDesign { get; set; }
        public bool? PopupNoPadding { get; set; }
        public ActionsQueryDTO SubmitAction { get; set; }
        public List<ActionFormVisibility> Visibilities { get; set; }
    }

    public class ActionFormControl
    {
        public string DataField { get; set; }

        /// <summary>
        /// If specified, values are nested into hierarchy, which is done based on this array. Nesting is done by the dot. Example: ['printData', 'userDefinedParameters', 'parameter.with.dot']
        /// </summary>
        public string[] DataFieldArray { get; set; }

        /// <summary>
        /// You can override the default visualization type, which is defined from ValueType-ValueDataType combination.
        /// </summary>
        public VisualizationType? VisualizationType { get; set; }
        public string ValueDataType { get; set; }
        public string ValueType { get; set; }
        public string Label { get; set; }
        public bool? Required { get; set; }

        /// <summary>
        /// Each visualization type has its own set of properties. Use predefined classes in Metronik.CDR.Core.Models.Responses.ActionForm.ActionFormControlProperties.
        /// </summary>
        public object Properties { get; set; }
        public int? Width { get; set; }
        public string Description { get; set; }
        public bool? Visible { get; set; } = true;

        /// <summary>
        /// If true, Value is always sent as string, regardless of its ValueDataType.
        /// Setting does not apply for table (or complex object).
        /// </summary>
        public bool SendValueAsString { get; set; } = false;

        /// <summary>
        /// If given, icon will be displayed in front of a label
        /// Setting does not apply for table (or complex object).
        /// </summary>
        public string LabelIconUrl { get; set; }

        /// <summary>
        /// If given, icon of the label will be colored with given color
        /// Setting does not apply for table (or complex object).
        /// </summary>
        public string LabelIconColor { get; set; }

        /// <summary>
        /// Css class to be appended on entire editor, can be used for bootstrap styling
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Colspan for editor. Default Value is 2, which means that each editor is in its own row. Set this to 1 if you want editors to be next to each other.
        /// </summary>
        public int ColSpan { get; set; } = 2;
    }

    public class ActionFormEvent
    {
        public string Name { get; set; }
        public string DataField { get; set; }
        public ActionFormEventType? Type { get; set; }
        public object Properties { get; set; }
    }

    public class ActionFormControlPropertyValue
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ActionFormValidation
    {
        public ActionFormValidationType? Type { get; set; }
        public string DataFieldPath1 { get; set; }
        public string DataFieldPath2 { get; set; }
        public string Message { get; set; }
    }

    public class ActionFormVisibility
    {
        public string DataField { get; set; }
        public List<ActionFormVisibilityValue> Values { get; set; }
    }

    public class ActionFormVisibilityValue
    {
        public string ExpressionUsedComponents { get; set; }
        public string ValueForVisible { get; set; }
    }
}
