import { AxiosResponse } from "axios";
import RequestService from 'src/services/request';
import EndPoints from 'src/constants/endpoints';
import IAccountRole from "src/interfaces/IAccountRole";
import ILoginModel from "src/interfaces/ILoginModel";
import IAccount from "src/interfaces/IAccount";
import IChangePassword from "src/interfaces/IChangePassword";
import IFirstTimeChangePassword from "src/interfaces/IFirstTimeChangePassword";

export function loginRequest(login: ILoginModel): Promise<AxiosResponse<IAccount>> {
    return RequestService.axios.post(EndPoints.login, login);
}

export function getMeRequest(): Promise<AxiosResponse<IAccount>> {
    return RequestService.axios.get(EndPoints.me);
}

export function getRoleRequest(): Promise<AxiosResponse<IAccountRole>> {
    return RequestService.axios.get(EndPoints.getRole);
}

export function putChangePassword(changePasswordModel: IChangePassword): Promise<AxiosResponse<IAccount>> {
    return RequestService.axios.post(EndPoints.changePassword, changePasswordModel);
}

export function putFirstTimeChangePassword(firstTimeChangePasswordModel: IFirstTimeChangePassword): Promise<AxiosResponse<IAccount>> {
    return RequestService.axios.post(EndPoints.firstTimeChangePassword, firstTimeChangePasswordModel);
}
