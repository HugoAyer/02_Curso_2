using SAPbobsCOM;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Curso_1.Utils
{
    public class ProductionTypes
    {
        public Dictionary<int,BoProductionOrderTypeEnum> orderTypes;

        public ProductionTypes()
        {
            orderTypes = new Dictionary<int,BoProductionOrderTypeEnum>();
            orderTypes.Add(0,BoProductionOrderTypeEnum.bopotStandard);
            orderTypes.Add(1,BoProductionOrderTypeEnum.bopotSpecial);
            orderTypes.Add(2,BoProductionOrderTypeEnum.bopotDisassembly);
        }
    }
}
