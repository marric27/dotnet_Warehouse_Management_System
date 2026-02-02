namespace dotnet_Warehouse_Management_System.Common
{
    public enum ErrorReason
    {
        NO_ERROR,
        //description = "Picked quantity is lower than the requested quantity.")
        MISSING_QTY,

        //description = "Goods were found damaged during picking.")
        DAMAGED_GOODS,

        //description = "Wrong item or SKU was found in the picking location.")
        WRONG_ITEM,

        //description = "Goods were expired or not suitable for shipment.")
        EXPIRED_GOODS,

        //description = "Picking operation was interrupted or cancelled.")
        PICKING_ABORTED,

        //description = "Other unspecified reason.")
        OTHER
    }

}
