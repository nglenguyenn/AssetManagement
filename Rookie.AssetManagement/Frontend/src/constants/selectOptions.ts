import ISelectOption from "src/interfaces/ISelectOption";
import {
  MaleUserGender,
  FemaleUserGender,
  MaleUserGenderLabel,
  FemaleUserGenderLabel,
  AdminUserType,
  AdminUserTypeLabel,
  StaffUserTypeLabel,
  StaffUserType,AllUserType,AllUserTypeLabel
} from "./User/UserConstants";

export const UserTypeOptions: ISelectOption[] = [
  { id: 2, label: AdminUserTypeLabel, value: AdminUserType },
  { id: 3, label: StaffUserTypeLabel, value: StaffUserType },
];

export const UserGenderOptions: ISelectOption[] = [
  { id: 1, label: MaleUserGenderLabel, value: MaleUserGender },
  { id: 2, label: FemaleUserGenderLabel, value: FemaleUserGender },
];

export const FilterUserTypeOptions: ISelectOption[] = [
  { id: 1, label: AllUserTypeLabel, value: AllUserType },
  { id: 2, label: AdminUserTypeLabel, value: AdminUserType },
  { id: 3, label: StaffUserTypeLabel, value: StaffUserType },
];