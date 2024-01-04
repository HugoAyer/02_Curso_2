namespace Curso_1.Models
{
    public class WorkOrder
    {
        public int? docEntry { get; set; }
        public int? docNum { get; set; }
        public int orderType {  get; set; }
        public string itemNo { get; set; }
        public double plannedQuantity { get; set; }
        public string wareHouse {  get; set; }
        public DateTime postingDate { get; set; }
        public DateTime dueDate { get; set; }
        public WorkOrder_Lines[] lines { get; set; }
    }
}
