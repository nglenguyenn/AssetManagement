import { takeLatest } from "@redux-saga/core/effects";
import { createNewAsset, getAsset, getHistory, getAssets, editAsset } from "../reducer";
import { handleCreateNewAsset, handleGetAsset, handleGetHistory, handleGetAssets, handleEditAsset  } from "./handles";

export default function* AssetSagas() {
    yield takeLatest(createNewAsset.type, handleCreateNewAsset);
    yield takeLatest(getAsset.type, handleGetAsset);
    yield takeLatest(getHistory.type, handleGetHistory);
    yield takeLatest(getAssets.type, handleGetAssets);
    yield takeLatest(editAsset.type, handleEditAsset)
}