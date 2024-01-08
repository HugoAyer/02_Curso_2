using Curso_1.Models;
using Curso_1.Utils;
using SAPbobsCOM;

namespace Curso_1.DataActions
{
    public class ProductionIssueContext
    {
        #region ProductionIssue
        public static Respuesta Add(ProductionIssue _data)
        {
            SAPbobsCOM.Documents inventoryGenExit = SAPCompany.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit);
            inventoryGenExit.DocDate = _data.postingDate;
            foreach(ProductionIssue_Lines line in _data.lines) 
            {
                inventoryGenExit.Lines.BaseEntry = line.baseEntry;
                inventoryGenExit.Lines.BaseLine = line.baseLine;
                inventoryGenExit.Lines.BaseType = Constants.WOObjectID;
                inventoryGenExit.Lines.Quantity = line.quantity;
                inventoryGenExit.Lines.WarehouseCode = line.wareHouse;
                inventoryGenExit.Lines.Add();
            }

            int code = inventoryGenExit.Add();
            if (code != Constants.OkNum)
            {
                string message = "";
                SAPCompany.company.GetLastError(out code, out message);
                return new Respuesta
                {
                    _code = code.ToString(),
                    _message = message
                };
            }
            else
            {
                string newKey = SAPCompany.company.GetNewObjectKey();
                return new Respuesta
                {
                    _code = newKey,
                    _message = Constants.PIAddSuccessMessage
                };
            }
        }
        #endregion
    }
}
