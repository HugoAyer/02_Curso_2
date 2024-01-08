using Curso_1.Models;
using Curso_1.Utils;

namespace Curso_1.DataActions
{
    public class ProductionEntryContext
    {
        #region ProductionEntry
        public static Respuesta Add(ProductionEntry _data)
        {
            SAPbobsCOM.Documents inventoryGenEntry = SAPCompany.company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry);
            inventoryGenEntry.DocDate = _data.postingDate;
            foreach (ProductionEntry_Lines line in _data.lines)
            {
                inventoryGenEntry.Lines.BaseEntry = line.baseEntry;
                inventoryGenEntry.Lines.BaseType = Constants.WOObjectID;
                inventoryGenEntry.Lines.Quantity = line.quantity;
                inventoryGenEntry.Lines.WarehouseCode = line.wareHouse;
                inventoryGenEntry.Lines.Add();
            }

            int code = inventoryGenEntry.Add();
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
                    _message = Constants.PEAddSuccessMessage
                };
            }
        }
        #endregion
    }
}
