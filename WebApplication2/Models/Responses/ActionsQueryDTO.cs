using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// Standard model for returning actions from module.
    /// </summary>
    public class ActionsQueryDTO : ActionsCoreQueryDTO
    {
        public string RecordTypeCode { get; set; }
        public string CurrentStateCode { get; set; }

        public string BaseActionName { get; set; }

        public string ActionType { get; set; }
        public string ActionPurpose { get; set; }
        public string ActionEntity { get; set; }
        public string ActionProcedure { get; set; }
        public string ActionProcedureMultiple { get; set; }
        public string ExtraParamsFormUrl { get; set; }

        public bool NeedsESign { get; set; }
        public bool CommentRequired { get; set; }
        public string ConfirmationMsg { get; set; }
        public string Verb { get; set; }
        public string ImageUrl { get; set; }
        public string ImageColor { get; set; }
        public string MenuGroup { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuImageColor { get; set; }
        public string MenuDescription { get; set; }
        public string DataFormat { get; set; }
        public string AppModuleCode { get; set; }
        public int RefreshRecords { get; set; }
        public int PrimaryDisplayOrder { get; set; }
        public int? SecondaryDisplayOrder { get; set; }
    }
}
