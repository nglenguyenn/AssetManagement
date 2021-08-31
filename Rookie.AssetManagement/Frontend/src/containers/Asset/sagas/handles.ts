import IAsset from "../../../interfaces/Asset/IAsset";
import { call, put } from "redux-saga/effects";
import IError from "../../../interfaces/IError";
import IAssetForm from "../../../interfaces/Asset/IAssetForm";
import { PayloadAction } from "@reduxjs/toolkit";
import { setStatus, createNewAsset, cleanUp, setAssets, postAssetComplete, setAsset, setHistory } from "../reducer";
import { Status } from "../../../constants/status";
import { postCreateNewAsset, getAssetsRequest, getAssetRequest, getHistoryRequest, putEditAsset } from "./requests";
import IQueryAssetModel from "src/interfaces/Asset/IQueryAssetModel";

export function* handleCreateNewAsset(action: PayloadAction<IAssetForm>) {
   const values = action.payload;
   try {
      const { data } = yield call(postCreateNewAsset, values);
      yield put(postAssetComplete(data as IAsset));

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

export function* handleGetAsset(action: PayloadAction<number>) {
   const values = action.payload;
   try {
      const { data } = yield call(getAssetRequest, values);
      yield put(setAsset(data));
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

export function* handleEditAsset(action: PayloadAction<IAssetForm>) {
   const values = action.payload;
   try {
      const { data } = yield call(putEditAsset, values);
      yield put(postAssetComplete(data as IAsset));
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

export function* handleGetHistory(action: PayloadAction<number>) {
   const values = action.payload;
   try {
      const { data } = yield call(getHistoryRequest, values);
      yield put(setHistory(data));
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

export function* handleGetAssets(action: PayloadAction<IQueryAssetModel>) {
   const query = action.payload;

   try {
      const { data } = yield call(getAssetsRequest, query);
      yield put(setAssets(data));
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
