using SAPbobsCOM;

namespace Curso_1.Models
{
    public class ProductionIssue
    {
        public int? docEntry {  get; set; }
        public int? docNum {  get; set; }
        public int? series {  get; set; }
        public DateTime postingDate { get; set; }
        public ProductionIssue_Lines[] lines { get; set; }
    }
}
