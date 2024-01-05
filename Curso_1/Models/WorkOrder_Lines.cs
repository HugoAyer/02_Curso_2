namespace Curso_1.Models
{
    public class WorkOrder_Lines
    {
        public int? lineNum { get; set; }
        public string itemNo { get; set; }
        public double baseQuantity { get; set; }
        public double plannedQuantity {  get; set; }
        public string wareHouse {  get; set; }
        public int issueType {  get; set; }
    }
}
