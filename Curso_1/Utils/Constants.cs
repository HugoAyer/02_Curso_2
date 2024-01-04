namespace Curso_1.Utils
{
    public static class Constants
    {
        #region General
        public const string OkCode = "0";
        public const int OkNum = 0;
        public const string OkMessage = "";
        #endregion

        #region Conectar
        public const int Connected = 0;
        public const string ErrorGenConnecting = "-1";
        #endregion

        #region UDOs
        public const string DemoUDO = "Demo";
        public const string curso_line = "CURSO_LINE";
        #endregion

        #region DemoUdoMessages
        public const string DemoUDOSuccess = "Se agregó el registro No. ";
        #endregion

        #region purhchaseRequest
        public const string PRAddSuccessMessage = "Se ha creado la requisición No. ";
        public const string PRUpdateGetDocNotFoundCode = "-1";
        public const string PRUpdateGetDocNotFoundMessage = "No se encontró el documento";
        public const string PRUpdateSuccess = "Documento actualizado con éxito";
        #endregion

        #region purchaseorders
        public const int POid = 1470000113;
        public const string POSuccessMessage = "Se ha generado la orden de compra";
        #endregion

        #region workorders
        public const string WOAddSuccessMessage = "Se ha generado la orden de producción";
        public const string WOGetErrorMessage = "No se encontró la orden de producción";
        #endregion
    }
}
