﻿namespace Warehouse.ProductWarehouse.Controller.Response
{
    public class ProductWarehouseResponse
    {
        public int IdProduct { get; set; }
        public int IdWarehouse { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}