import { takeLatest } from "@redux-saga/core/effects";
import { createNewUser, editUser, getUser, getUsers } from "../reducer";
import { handleCreateNewUser, handleEditUser, handleGetUser, handleGetUsers } from "./handles";

export default function* UserSagas() {
    yield takeLatest(createNewUser.type, handleCreateNewUser)
    yield takeLatest(getUsers.type, handleGetUsers);
    yield takeLatest(getUser.type, handleGetUser);
    yield takeLatest(editUser.type, handleEditUser);
}
