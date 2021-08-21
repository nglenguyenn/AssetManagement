import { takeLatest } from "@redux-saga/core/effects";
import { createNewAsset } from "../reducer";
import { handleCreateNewAsset } from "./handles";

export default function* AssetSagas() {
    yield takeLatest(createNewAsset.type, handleCreateNewAsset)
}