using System;
using System.Collections.Generic;

namespace WebApplication2.Models.Responses
{
    public class ImportExportParameters
    {
        public int ExportVersion { get; set; }
        public string ExportRecordTypeCode { get; set; }
        public string ExportTypeCode { get; set; }
        public EnumData EnumSetValues { get; set; }
        public List<UnitOfMeasureData> UnitOfMeasures { get; set; }
        public UserAndSystemValue UserAndSystemValues { get; set; }
        public List<ResourceData> Resources { get; set; }

        public Dictionary<string, object> SubModulData { get; set; }
    }

    public class EnumData
    {
        public List<EnumSetData> EnumSets { get; set; }
        public List<EnumValueData> EnumValues { get; set; }
        public List<EnumGroupData> EnumGroups { get; set; }
    }

    public class EnumSetData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? EnumGroupId { get; set; }
        public int RecordStatusId { get; set; }
        public Guid BaseGuid { get; set; }
        public int? TargetId { get; set; }
    }

    public class EnumValueData
    {
        public int Id { get; set; }
        public int EnumSetId { get; set; }
        public string Code { get; set; }
        public string EnumValue { get; set; }
        public string EnumString { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        public Guid BaseGuid { get; set; }
        public int RecordStatusId { get; set; }
    }

    public class EnumGroupData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecordStatusId { get; set; }
        public int? TargetId { get; set; }
    }

    public class UserAndSystemValue
    {
        public List<ValueDataTypeData> ValueDataTypes { get; set; }
        public List<UserAndSystemValueTypeData> UserAndSystemValueTypes { get; set; }
        public List<UserAndSystemValueTypeGroupData> UserAndSystemValueTypeGroups { get; set; }
        public List<UnitOfMeasureData> UserAndSystemValueTypeUoms { get; set; }
    }

    public class ValueDataTypeData
    {
        public int Id { get; set; }
        public string SqlCode { get; set; }
        public string ProgCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ValueTypeCode { get; set; }
        public int? TargetId { get; set; }
    }

    public class UserAndSystemValueTypeData
    {
        public int Id { get; set; }
        public int? BaseDataTypeId { get; set; }
        public string BaseDataTypeCode { get; set; }
        public int ValueTypeId { get; set; }
        public string ValueTypeCode { get; set; }
        public int ValueDataTypeId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string DefaultValue { get; set; }
        public string ValueRangeRegex { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public int? UserAndSystemValueTypeGroupId { get; set; }

        public int RecordStatusId { get; set; }
        public int? TargetId { get; set; }
    }

    public class UserAndSystemValueTypeGroupData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecordStatusId { get; set; }
        public int? TargetId { get; set; }
    }

    public class UnitOfMeasureData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string ConversionFactorSI { get; set; }
        public int? DisplayOrder { get; set; }
        public int RecordStatusId { get; set; }
        public int? TargetId { get; set; }
    }

    public class ResourceData
    {
        public int ResourceTypeId { get; set; }
        public string ResourceTypeCode { get; set; }

        public List<string> Keys { get; set; }

        public List<string> ImportExportKeys { get; set; }
    }
}
