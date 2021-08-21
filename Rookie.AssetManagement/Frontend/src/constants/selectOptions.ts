import ISelectOption from "src/interfaces/ISelectOption";
import {
  MaleUserGender,
  FemaleUserGender,
  MaleUserGenderLabel,
  FemaleUserGenderLabel,
} from "./User/GenderConstants";
import {
  AdminUserType,
  AdminUserTypeLabel,
  StaffUserTypeLabel,
  StaffUserType,
  AllUserType,
  AllUserTypeLabel,
} from "./User/RoleConstants";
import {
  AvailableAsset,
  UnavailableAsset,
  AvailableAssetLabel,
  UnavailableAsetLabel,
  AllAssetLabel,
  AllStateAsset,
  AssignedAssetLabel,
  AssignedAsset,
  WaitingAsset,
  WaitingAssetLabel,
  RecycledAsset,
  RecycledAssetLabel
} from "./Asset/StateConstants";

import {
  LaptopCategory,
  MonitorCategoryLabel,
  LaptopCategoryLabel,
  MonitorCategory,
  PersonalComputerCategory,
  PersonalComputerCategoryLabel,
} from "./Asset/CategoryConstants";

export const UserTypeOptions: ISelectOption[] = [
  { id: 1, label: AdminUserTypeLabel, value: AdminUserType },
  { id: 2, label: StaffUserTypeLabel, value: StaffUserType },
];

export const UserGenderOptions: ISelectOption[] = [
  { id: 1, label: MaleUserGenderLabel, value: MaleUserGender },
  { id: 2, label: FemaleUserGenderLabel, value: FemaleUserGender },
];

export const FilterUserTypeOptions: ISelectOption[] = [
  { id: 0, label: AllUserTypeLabel, value: AllUserType },
  { id: 1, label: AdminUserTypeLabel, value: AdminUserType },
  { id: 2, label: StaffUserTypeLabel, value: StaffUserType },
];
export const AssetStateOptions: ISelectOption[] = [
  { id: 1, label: AvailableAssetLabel, value: AvailableAsset },
  { id: 2, label: UnavailableAsetLabel, value: UnavailableAsset },
];

export const AssetCategoryOptions: ISelectOption[] = [
  { id: 1, label: LaptopCategoryLabel, value: LaptopCategory },
  { id: 2, label: MonitorCategoryLabel, value: MonitorCategory },
  { id: 3, label: PersonalComputerCategoryLabel, value: PersonalComputerCategory},
];

export const FilterAssetStateOptions: ISelectOption[] = [
  { id: 0, label: AllAssetLabel, value: AllStateAsset },
  { id: 1, label: AvailableAssetLabel, value: AvailableAsset },
  { id: 2, label: AssignedAssetLabel, value: AssignedAsset},
  { id: 3, label: UnavailableAsetLabel, value: UnavailableAsset },
  { id: 4, label: WaitingAssetLabel, value: WaitingAsset },
  { id: 5, label: RecycledAssetLabel, value: RecycledAsset },
];
export const FilterAssetCategoryOptions: ISelectOption[] = [
  { id: 0, label: LaptopCategoryLabel, value: LaptopCategory },
  { id: 1, label: MonitorCategoryLabel, value: MonitorCategory },
  { id: 2, label: PersonalComputerCategoryLabel, value: PersonalComputerCategory },
];
