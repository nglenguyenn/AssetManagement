import ISelectOption from "src/interfaces/ISelectOption";
import {
  AcceptedAssignment,
  AcceptedAssignmentLabel,
  AcceptedAssignmentString,
  AllStateAssignment,
  AllStateAssignmentLabel,
  WaitingAssignment,
  WaitingAssignmentLabel,
  WaitingAssignmentString
} from "./Assignment/StateConstants";
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


export const FilterAssignmentStateOptions: ISelectOption[] = [
  { id: 0, label: AllStateAssignmentLabel, value: AllStateAssignment },
  { id: 1, label: AcceptedAssignmentLabel, value: AcceptedAssignment },
  { id: 2, label: WaitingAssignmentLabel, value: WaitingAssignment },
];

export const AssignmentStates: ISelectOption[] = [
  { id: 1, label: AcceptedAssignmentLabel, value: AcceptedAssignmentString },
  { id: 2, label: WaitingAssignmentLabel, value: WaitingAssignmentString },
];
