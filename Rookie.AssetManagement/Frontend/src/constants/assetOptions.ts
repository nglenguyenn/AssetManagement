import ISelectOption from "src/interfaces/ISelectOption";
import {
    AvailableAsset,
    UnavailableAsset,
    AvailableAssetLabel,
    AllAssetLabel,
    AllStateAsset,
    AssignedAssetLabel,
    AssignedAsset,
    WaitingForRecycleAsset,
    WaitingForRecycleAssetLabel,
    RecycledAsset,
    RecycledAssetLabel,
    AvailableAssetString,
    AssignedAssetString,
    WaitingForRecycleAssetString,
    RecycledAssetString,
    UnavailableAssetString,
    UnavailableAssetLabel,
  } from "./Asset/StateConstants";
  
  import {
    LaptopCategory,
    MonitorCategoryLabel,
    LaptopCategoryLabel,
    MonitorCategory,
    PersonalComputerCategory,
    PersonalComputerCategoryLabel,
    AllCategory,
    AllCategoryLabel,
    LaptopCategoryId,
    MonitorCategoryId,
    PersonalComputerCategoryId
  } from "./Asset/CategoryConstants";

  export const AssetStateOptions: ISelectOption[] = [
    { id: 1, label: AvailableAssetLabel, value: AvailableAsset },
    { id: 2, label: UnavailableAssetLabel, value: UnavailableAsset },
  ];
  
  export const AssetCategoryOptions: ISelectOption[] = [
    { id: 1, label: LaptopCategoryLabel, value: LaptopCategoryId },
    { id: 2, label: MonitorCategoryLabel, value: MonitorCategoryId },
    { id: 3, label: PersonalComputerCategoryLabel, value: PersonalComputerCategoryId},
  ];
  
  export const FilterAssetStateOptions: ISelectOption[] = [
    { id: 0, label: AllAssetLabel, value: AllStateAsset },
    { id: 1, label: AssignedAssetLabel, value: AssignedAsset },
    { id: 2, label: AvailableAssetLabel, value: AvailableAsset },
    { id: 3, label: UnavailableAssetLabel, value: UnavailableAsset},   
    { id: 4, label: WaitingForRecycleAssetLabel, value: WaitingForRecycleAsset },
    { id: 5, label: RecycledAssetLabel, value: RecycledAsset },
  ];
  export const FilterAssetCategoryOptions: ISelectOption[] = [
    { id: 0, label: AllCategoryLabel, value: AllCategory },
    { id: 1, label: LaptopCategoryLabel, value: LaptopCategory },
    { id: 2, label: MonitorCategoryLabel, value: MonitorCategory },
    { id: 3, label: PersonalComputerCategoryLabel, value: PersonalComputerCategory },
  ];
  export const AssetStates: ISelectOption[] = [
    { id: 1, label: AvailableAssetLabel, value: AvailableAssetString },
    { id: 2, label: AssignedAssetLabel, value: AssignedAssetString},
    { id: 3, label: UnavailableAssetLabel, value: UnavailableAssetString },
    { id: 4, label: WaitingForRecycleAssetLabel, value: WaitingForRecycleAssetString },
    { id: 5, label: RecycledAssetLabel, value: RecycledAssetString },
  ];
  export const AssetStatesForEdit : ISelectOption[] = [
    { id: 1, label: AvailableAssetLabel, value: AvailableAsset },
    { id: 2, label: UnavailableAssetLabel, value: UnavailableAsset },
    { id: 3, label: WaitingForRecycleAssetLabel, value:WaitingForRecycleAsset},
    { id: 4, label: RecycledAssetLabel, value: RecycledAsset}
  ]