import IUser from 'src/interfaces/User/IUser';
import { call, put } from "redux-saga/effects";
import IError from "../../../interfaces/IError";
import IUserForm from "../../../interfaces/User/IUserForm";
import { PayloadAction } from "@reduxjs/toolkit";
import { setStatus, setUsers, postUserComplete, setUser } from "../reducer";
import { Status } from "../../../constants/status";
import { getUsersRequest, postCreateNewUser, getUserRequest, putEditUser } from "./requests";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";

export function* handleCreateNewUser(
    action: PayloadAction<IUserForm>
) {
    const values = action.payload;

    try {
        const { data } = yield call(postCreateNewUser, values);

        yield put(postUserComplete(data as IUser))

        yield put(
            setStatus({
                status: Status.Success,
                error: undefined,
            })
        );

    } catch (error:any) {
        const errorModel = error.response.data as IError;

        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}

export function* handleGetUser(action: PayloadAction<number>) {
    const values = action.payload;
    try {
        const { data } = yield call(getUserRequest, values);
        yield put(setUser(data));
    } catch (error:any) {
        const errorModel = error.response.data as IError;

        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}

export function* handleGetUsers(action: PayloadAction<IQueryUserModel>) {
    const query = action.payload;

    try {
        const { data } = yield call(getUsersRequest, query);

        yield put(setUsers(data));
    }
    catch (error:any) {
        const errorModel = error.response.data as IError;

        yield put(setStatus({
            status: Status.Failed,
            error: errorModel,
        }))
    }
}

export function* handleEditUser(
    action: PayloadAction<IUserForm>
) {
    const values = action.payload;
    try {
        const { data } = yield call(putEditUser, values);

        yield put(postUserComplete(data as IUser))

        yield put(
            setStatus({
                status: Status.Success,
                error: undefined,
            })
        );

    } catch (error:any) {
        const errorModel = error.response.data as IError;
        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}