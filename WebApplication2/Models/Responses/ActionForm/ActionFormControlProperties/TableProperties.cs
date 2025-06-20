using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public static class TablePropertiesSelection
    {
        public const string Single = "single";
        public const string None = "none";
        public const string Multiple = "multiple";
    }

    public static class TablePropertiesColumnsEditingColSpan
    {
        public const int One = 1;
        public const int Two = 2;
    }

    public class TableProperties
    {
        public int? Height { get; set; } = 480;
        public string IdDataField { get; set; }
        public string Selection { get; set; } = TablePropertiesSelection.None;
        public bool FirstRowSelectedByDefault { get; set; } = false;
        public string SelectRowsByDataField { get; set; }
        public List<object> SelectRowsValues { get; set; }
        public List<TablePropertiesColumns> Columns { get; set; }
        public TablePropertiesActions Actions { get; set; }
        public string DataSource { get; set; }
        public string ParentIdDataField { get; set; }
        public string IsDisabledDataField { get; set; }
        public bool ApplyChangesDirectly { get; set; } = false;
        public bool CompactDesign { get; set; } = false;
        public TableEditing Editing { get; set;  }
        public TableColumnsEditModeDependencies ColumnsEditModeDependencies { get; set;  }
        public bool ExpandAllOnInit { get; set; } = true;
        public bool LoadInitialData { get; set; } = true;
        public TablePropertiesRefresh Refresh { get; set; }
        public RowDragging RowDragging { get; set;  }
        public Paging Paging { get; set; }
        public PopupSettings PopupSettings { get; set; }
        public TableOptions TableOptions { get; set; }
        public TablePropertiesGrouping Grouping { get; set; }
        public bool TableUsedForSelection { get; set; } = true;
        public List<RowColor> RowColors { get; set; }
        public List<TablePropertiesTextDecoration> TextDecorations { get; set; }
        public TablePropertiesExtendedData ExtendedData { get; set; }
    }

    public class RowColor
    {
        public string Color { get; set; }
        public string DataField { get; set; }
        public string EqualTo { get; set; }
    }

    public class RowDragging
    {
        public bool UseDragging { get; set; } = false;
        public string FieldOrder { get; set; }
        public string ActionCode { get; set; }
    }

    public class PopupSettings
    {
        public string PopupTitleDataFieldName { get; set; }
        public string PopupTitleSingleItemText { get; set; }
        public int Width { get; set; }
    }

    public class TableOptions
    {
        public bool AllowColumnResizing { get; set; } = true;
        public bool AllowSorting { get; set; } = true;
        public bool ColumnAutoWidth { get; set; } = true;
        public bool ColumnChooser { get; set; } = true;
        public bool ColumnFixing { get; set; } = true;
        public bool Export { get; set; } = true;
        public bool FilterPanel { get; set; } = true;
        public bool FilterRow { get; set; } = true;
        public bool GroupPanel { get; set; } = true;
        public bool HeaderFilter { get; set; } = true;
        public bool HideHeaderTitleRow { get; set; } = false;
        public bool RowAlternationEnabled { get; set; } = true;
        public bool SearchPanel { get; set; } = true;
        public bool ShowBorders { get; set; } = true;
        public bool ShowLastUpdatedText { get; set; } = true;
        public bool ShowLoadPanel { get; set; } = true;
    }

    public class Paging
    {
        public List<string> AllowedPageSizes { get; set; }
        public string PageSize { get; set; }
    }

    public class TableEditing
    {
        public bool AllowEditing { get; set; } = false;
        public bool AllowAdding { get; set; } = false;
        public bool AllowDeleting { get; set; } = false;
        public EditingMode? Mode { get; set; } = EditingMode.batch;
        public List<string> DataFieldsOrderInEditMode { get; set; } = new List<string>();
        public bool InsertMultiple { get; set; } = false;
        public bool InsertMultipleFullScreen { get; set; } = false;
    }

    public class TablePropertiesActions
    {
        public string RecordTypeCode { get; set; }
        public string RecordIdDataField { get; set; }
        public string RecordStateCodeDataField { get; set; }
        public string AppModuleCode { get; set; }
        public string BaseActionCodesToFilterOut { get; set; }
        public string ActionCodesToFilterOut { get; set; }
        public string BaseActionCodesToShow { get; set; }
        public string ActionCodesToShow { get; set; }
        public string NameDataField { get; set; }
        public string EventNameForRetrievingActions { get; set; }
        public string EventNameForExecutingActions { get; set; }
    }

    public class TablePropertiesColumns
    {
        public TablePropertiesColumns()
        {
        }

        public TablePropertiesColumns(string dataField, string caption)
        {
            this.DataField = dataField;
            this.Caption = caption;
        }

        public string DataField { get; set; }
        public string DataFieldNameForTable { get; set; }
        public string DataFieldSelectionDisplayValue { get; set; }
        public string DataFieldSignalR { get; set; }
        public string Caption { get; set; }
        public string CaptionTitle { get; set; }
        public string DataType { get; set; } = "STRING";
        public string ValueType { get; set; } = "SIMPLE";
        public bool Visible { get; set; } = true;
        public EColumnFixed Fixed { get; set; } = EColumnFixed.No;
        public EOrderType OrderType { get; set; } = EOrderType.Undefined;
        public string ColorFromDataField { get; set; }
        public string TooltipFromDataField { get; set; }
        public string TooltipSelectionFromDataSourceDataFieldFrom { get; set; }
        public string TooltipSelectionFromDataSourceDataFieldMatchingBy { get; set; }
        public bool ShowInColumnChooser { get; set; } = true;
        public string DataFieldComplexObjectPath { get; set; }
        public bool SendValueAsString { get; set; }
        public TablePropertiesColumnsEditing Editing { get; set; }
        public int? Width { get; set; }
    }

    public class TablePropertiesColumnsEditing
    {
        public bool AllowEditing { get; set; } = false;
        public string MinDataField { get; set; }
        public string MaxDataField { get; set; }
        public string RegexDataField { get; set; }
        public string NumberOfDigitsDataField { get; set; }
        public string DisplayValueDataField { get; set; }
        public bool RequiredInEditMode { get; set; } = false;
        public bool IsDataTypeDependent { get; set; } = false;
        public TablePropertiesColumnsSelectionFromDataSource DataTypeDependencyEnum { get; set; }
        public TablePropertiesColumnsSelectionFromDataSource DataTypeDependencyResource { get; set; }
        public TablePropertiesColumnsSelectionFromDataSource SelectionFromDataSource { get; set; }
        public int ColSpan { get; set; } = TablePropertiesColumnsEditingColSpan.One;
        public bool IsMultiline { get; set; } = false;
        public int Height { get; set; } = 0;
        public string Regex { get; set; }
        public string DefaultAddNewValue { get; set; }
        public string DefaultAddNewValuePageConnectionPointValue { get; set; }
    }

    public class TablePropertiesColumnsSelectionFromDataSource
    {
        public string DataSource { get; set; }
        public string ValueDataField { get; set; }
        public string DisplayDataField { get; set; }
        public string GroupingDataField { get; set; }
        public string ParentIdDataField { get; set; }
        public string ColorDataField { get; set; }
        public List<string> FilteringDataFields { get; set; }
        public bool SelectFirstByDefault { get; set; }
        public string SelectedValueIfFound { get; set; }
        public string SelectedValueDataMemberForMatching { get; set; }
    }

    public class TableColumnsEditModeDependencies
    {
        public List<TableColumnsEditModeDependenciesVisibilityDisability> Visibility { get; set; }
        public List<TableColumnsEditModeDependenciesVisibilityDisability> Disability { get; set; }
        public List<TableColumnsEditModeDependenciesValueFilling> ValueFilling { get; set; }
    }

    public class TableColumnsEditModeDependenciesVisibilityDisability
    {
        public bool OppositeBehaviour { get; set; } = false;
        public List<string> ColumnsFor { get; set; }
        public List<string> Values { get; set; }
        public string DataField { get; set; }
        public string DataField_Override { get; set; }
        public string DataFieldFromSelectionFromDataSource { get; set; }
        public string DataFieldIdForMatchingFromSelectionFromDataSource { get; set; }
        public bool HiddenOrDisabledWhenValueNotEmpty { get; set; } = false;
        public bool ClearContentOnHiddenOrDisabled { get; set; } = true;
    }

    public class TableColumnsEditModeDependenciesValueFilling
    {
        public string ColumnFor { get; set; }
        public string ColumnFor_Override { get; set; }
        public string DataField { get; set; }
        public string DataField_Override { get; set; }
        public string DataFieldFromSelectionFromDataSource { get; set; }
        public string DataFieldIdForMatchingFromSelectionFromDataSource { get; set; }
        public string ValueToFill { get; set; }
        public bool OnlyFillIfNotDisabled { get; set; } = false;
        public bool EvaluateOnEditingStart { get; set; } = false;
    }

    public class TablePropertiesRefresh
    {
        public bool ShowRefreshButton { get; set; } = true;
        public int RefreshIntervalInSeconds { get; set; } = 0;
        public bool UseSignalR { get; set; }
        public string SignalRCode { get; set; }
        public string SignalRDataField { get; set; }
    }

    public class TablePropertiesGrouping
    {
        public string ByTabColumnToGroupBy { get; set; }
        public bool ByTabGroupedByDefault { get; set; } = true;
        public List<string> ColumnsToGroupBy { get; set; }
        public List<string> ColumnsToSortBy { get; set; }
        public EOrderType SortOrderType { get; set; } = EOrderType.Undefined;
    }

    public class TablePropertiesTextDecoration
    {
        public List<string> DataFields { get; set; } = new List<string>();
        public string DataFieldForCondition { get; set; }
        public string EqualTo { get; set; }

        /// <summary>
        /// Gets or sets style type which can be one of: bold, italic, underline, lineThrough
        /// </summary>
        public string StyleType { get; set; }
    }

    public class TablePropertiesExtendedData
    {
        public string Endpoint { get; set; }
    }
}
