import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from "redux-saga/effects";

import { Status } from "src/constants/status";
import IChangePassword from "src/interfaces/IChangePassword";
import IError from "src/interfaces/IError";
import ILoginModel from "src/interfaces/ILoginModel";
import ISubmitAction from "src/interfaces/ISubmitActions";
import IFirstTimeChangePassword from "src/interfaces/IFirstTimeChangePassword";

import { me, setAccount, setRole, setStatus } from "../reducer";
import {
  loginRequest,
  getMeRequest,
  putChangePassword,
  getRoleRequest,
  putFirstTimeChangePassword,
} from "./requests";

export function* handleLogin(action: PayloadAction<ILoginModel>) {
  const loginModel = action.payload;

  try {
    const { data } = yield call(loginRequest, loginModel);
    yield put(setAccount(data));
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

export function* handleGetMe() {
  try {
    const { data } = yield call(getMeRequest);
    if (data.userName) {
      yield put(setAccount(data));
    }
  } catch (error) { }
}

export function* handleChangePassword(action: PayloadAction<IChangePassword>) {
  const values = action.payload;
  try {
    const { data } = yield call(putChangePassword, values);

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

export function* handleFirstTimeChangePassword(
  action: PayloadAction<IFirstTimeChangePassword>
) {
  const values = action.payload;

  try {
    yield call(putFirstTimeChangePassword, values);
    yield put(me());
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


