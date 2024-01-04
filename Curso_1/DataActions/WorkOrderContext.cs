using Curso_1.Models;
using Curso_1.Utils;
using Newtonsoft.Json;
using SAPbobsCOM;

namespace Curso_1.DataActions
{
    public class WorkOrderContext
    {
        public static Respuesta Add(WorkOrder _data)
        {
            SAPbobsCOM.ProductionOrders productionOrders = SAPCompany.company.GetBusinessObject(BoObjectTypes.oProductionOrders);
                
            ProductionTypes productionTypes = new ProductionTypes();
            productionOrders.ProductionOrderType = productionTypes.orderTypes[_data.orderType];
            productionOrders.ItemNo = _data.itemNo;
            productionOrders.PlannedQuantity = _data.plannedQuantity;
            productionOrders.Warehouse = _data.wareHouse;
            productionOrders.PostingDate = _data.postingDate;
            productionOrders.DueDate = _data.dueDate;

            foreach(Models.WorkOrder_Lines lines in _data.lines) 
            {
                ProductionIssueType productionIssueType = new ProductionIssueType();
                productionOrders.Lines.ItemNo = lines.itemNo;
                productionOrders.Lines.BaseQuantity = lines.baseQuantity;
                productionOrders.Lines.PlannedQuantity = lines.plannedQuantity;
                productionOrders.Lines.Warehouse = lines.wareHouse;
                productionOrders.Lines.ProductionOrderIssueType = productionIssueType.ProductionIssues[lines.issueType];
                productionOrders.Lines.Add();
            }

            int code = productionOrders.Add();
            if (code != Constants.OkNum)
            {
                string message="";
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
                    _message = Constants.WOAddSuccessMessage
                };
            }

        }

        public static Respuesta Get(int docEntry)
        {
            SAPbobsCOM.ProductionOrders productionOrders = SAPCompany.company.GetBusinessObject(BoObjectTypes.oProductionOrders);
            bool success = productionOrders.GetByKey(docEntry);
            if (success) 
            {
                WorkOrder workOrder = getWorkOrder(productionOrders);
                return new Respuesta
                {
                    _code = Constants.OkCode,
                    _message = JsonConvert.SerializeObject(workOrder)
                };
            }
            else
            {
                return new Respuesta
                {
                    _code = Constants.ErrorGenConnecting,
                    _message = Constants.WOGetErrorMessage
                };
            }
        }

        private static WorkOrder getWorkOrder(SAPbobsCOM.ProductionOrders productionOrders)
        {
            WorkOrder workOrder = new WorkOrder();
            workOrder.docEntry = productionOrders.AbsoluteEntry;
            workOrder.docNum = productionOrders.DocumentNumber;
            workOrder.orderType = (int)productionOrders.ProductionOrderType;
            workOrder.itemNo = productionOrders.ItemNo;
            workOrder.plannedQuantity = productionOrders.PlannedQuantity;
            workOrder.wareHouse = productionOrders.Warehouse;
            workOrder.postingDate = productionOrders.PostingDate;
            workOrder.dueDate = productionOrders.DueDate;

            List<Models.WorkOrder_Lines> lines = new List<Models.WorkOrder_Lines>();
            for (int i = 0; i < productionOrders.Lines.Count; i++)
            {
                productionOrders.Lines.SetCurrentLine(i);

                Models.WorkOrder_Lines line = new Models.WorkOrder_Lines();
                line.itemNo = productionOrders.Lines.ItemNo;
                line.baseQuantity = productionOrders.Lines.BaseQuantity;
                line.plannedQuantity = productionOrders.Lines.PlannedQuantity;
                line.wareHouse = productionOrders.Lines.Warehouse;
                line.issueType = (int)productionOrders.Lines.ProductionOrderIssueType;
                lines.Add(line);
            }

            workOrder.lines = lines.ToArray();            
            return workOrder;
        }
    }
}
