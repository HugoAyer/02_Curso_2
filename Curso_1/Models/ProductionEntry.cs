namespace Curso_1.Models
{
    public class ProductionEntry
    {
        public int? docEntry { get; set; }
        public int? docNum { get; set; }
        public int? series { get; set; }
        public DateTime postingDate { get; set; }
        public ProductionEntry_Lines[] lines { get; set; }
    }
}
