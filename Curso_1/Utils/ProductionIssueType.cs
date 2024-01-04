using SAPbobsCOM;

namespace Curso_1.Utils
{
    public class ProductionIssueType
    {
        public Dictionary<int, SAPbobsCOM.BoIssueMethod> ProductionIssues { get; set; }
        //bopotStandard = 0
        //bopotSpecial = 1
        //bopotDisassembly = 2
        public ProductionIssueType() 
        {
            ProductionIssues = new Dictionary<int, SAPbobsCOM.BoIssueMethod>();
            ProductionIssues.Add(0, BoIssueMethod.im_Backflush);
            ProductionIssues.Add(1, BoIssueMethod.im_Manual);                       
        }
    }
}
