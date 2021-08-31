import { takeLatest } from "@redux-saga/core/effects";
import { getAssignments, createNewAssignment, getAssignment } from "../reducer";
import { handleGetAssignments, handleCreateNewAssignment, handleGetAssignment } from "./handles";

export default function* AssignmentSagas() {
    yield takeLatest(getAssignments.type, handleGetAssignments);
    yield takeLatest(getAssignment.type, handleGetAssignment);
    yield takeLatest(createNewAssignment.type, handleCreateNewAssignment);
}