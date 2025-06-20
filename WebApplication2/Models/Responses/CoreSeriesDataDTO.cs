using System.Collections.Generic;

namespace WebApplication2.Models.Responses
{
    public class CoreSeriesDataDTOValues
    {
        public object X { get; set; }
        public object Y { get; set; }
        //public ValueQuality Quality { get; set; }
    }

    public class CoreSeriesDataDTO<T>
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitX { get; set; }
        public string UnitY { get; set; }

        public string DataTypeX { get; set; }
        public string DataTypeY { get; set; }
        public int? SignificantDigitsX { get; set; }
        public int? SignificantDigitsY { get; set; }

        public List<T> Values { get; set; }
    }

    public class CoreSimpleValueDataDTO
    {
        public string UnitY { get; set; }
        public object Value { get; set; }
    }
}
