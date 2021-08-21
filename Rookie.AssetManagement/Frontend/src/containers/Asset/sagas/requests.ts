import { AxiosResponse } from "axios";
import RequestService from '../../../services/request';
import EndPoints from '../../../constants/endpoints';
import IAssetForm from "../../../interfaces/Asset/IAssetForm";
import IAsset from "../../../interfaces/Asset/IAsset";
import Endpoints from "../../../constants/endpoints";

export function postCreateNewAsset(newAssetModel: IAssetForm): Promise<AxiosResponse<IAsset>> {
    console.log(newAssetModel);
    return RequestService.axios.post(EndPoints.createNewAsset, newAssetModel);
}