import { AxiosResponse } from "axios";
import qs from 'qs';
import RequestService from '../../../services/request';
import EndPoints from '../../../constants/endpoints';
import IAssetForm from "../../../interfaces/Asset/IAssetForm";
import IAsset from "../../../interfaces/Asset/IAsset";
import IAssignment from "src/interfaces/Assignment/IAssignment";
import Endpoints from "../../../constants/endpoints";
import IQueryAssetModel from "src/interfaces/Asset/IQueryAssetModel";

export function postCreateNewAsset(newAssetModel: IAssetForm): Promise<AxiosResponse<IAsset>> {
    return RequestService.axios.post(EndPoints.createNewAsset, newAssetModel);
}
export function putEditAsset(newAssetModel : IAssetForm) : Promise<AxiosResponse<IAsset>>{
    return RequestService.axios.post(EndPoints.editAsset, newAssetModel);
}
export function getAssetRequest(id: number): Promise<AxiosResponse<IAsset>> {
    return RequestService.axios.get(EndPoints.getAsset(id));
}

export function getHistoryRequest(id: number): Promise<AxiosResponse<Array<IAssignment>>> {
    return RequestService.axios.get(EndPoints.getHistory(id));
}

export function getAssetsRequest(query: IQueryAssetModel): Promise<AxiosResponse<IAsset>> {
    return RequestService.axios.get(Endpoints.getAssets, {
        params: query,
        paramsSerializer: params => qs.stringify(params),
    });
}