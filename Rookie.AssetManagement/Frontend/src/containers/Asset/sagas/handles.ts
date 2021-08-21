import IAsset from '../../../interfaces/Asset/IAsset'
import { call, put } from "redux-saga/effects";
import IError from "../../../interfaces/IError";
import IAssetForm from "../../../interfaces/Asset/IAssetForm";
import { PayloadAction } from "@reduxjs/toolkit";
import { setStatus,createNewAsset,cleanUp } from "../reducer";
import { Status } from "../../../constants/status";
import {postCreateNewAsset } from "./requests";

export function* handleCreateNewAsset(action: PayloadAction<IAssetForm>)
{
    const values = action.payload;
    try {
        const { data } = yield call(postCreateNewAsset, values);

        yield put(
            setStatus({
                status: Status.Success,
                error: undefined,
            })
        );

    } catch (error) {
        const errorModel = error.response.data as IError;

        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}
