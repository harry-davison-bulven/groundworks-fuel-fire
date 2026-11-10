namespace Groundworks.FuelFire {
public static class FuelRepairs {
 public static bool ConsumeKit(ModuleInventoryPart inventory){return inventory!=null&&inventory.TotalAmountOfPartStored("evaRepairKit")>0&&inventory.RemoveNPartsFromInventory("evaRepairKit",1,true);}
}
}
