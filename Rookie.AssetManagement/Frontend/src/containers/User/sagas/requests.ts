import qs from 'qs';

import { AxiosResponse } from "axios";
import RequestService from '../../../services/request';
import EndPoints from '../../../constants/endpoints';
import IUserForm from "../../../interfaces/User/IUserForm";
import IUser from "../../../interfaces/User/IUser";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";
import Endpoints from "../../../constants/endpoints";
import { QuestionSquare } from "react-bootstrap-icons";

export function postCreateNewUser(newUserModel: IUserForm): Promise<AxiosResponse<IUser>> {
    newUserModel.timeOffset = newUserModel.dateOfBirth?.getTimezoneOffset();
    return RequestService.axios.post(EndPoints.createNewUser, newUserModel);
}
export function getUsersRequest(query: IQueryUserModel): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.get(Endpoints.getUsers, {
        params: query,
        paramsSerializer: params => qs.stringify(params),
    });
}

export function getUserRequest(id: number): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.get(EndPoints.getUser(id));
}

export function putEditUser(editUserModel: IUserForm) {
    editUserModel.timeOffset = editUserModel.dateOfBirth?.getTimezoneOffset();
    return RequestService.axios.post(Endpoints.editUser, editUserModel);
}