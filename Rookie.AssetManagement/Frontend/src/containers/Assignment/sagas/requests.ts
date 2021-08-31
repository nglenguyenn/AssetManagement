import { AxiosResponse } from "axios";
import qs from 'qs';
import RequestService from '../../../services/request';
import EndPoints from '../../../constants/endpoints';
import IAssignment from "src/interfaces/Assignment/IAssignment";
import IAssignmentForm from "src/interfaces/Assignment/IAssignmentForm";
import Endpoints from "../../../constants/endpoints";
import IQueryAssignmentModel from "src/interfaces/Assignment/IQueryAssignmentModel";

export function getAssignmentsRequest(query: IQueryAssignmentModel): Promise<AxiosResponse<IAssignment>> {
    return RequestService.axios.get(Endpoints.getAssignments, {
        params: query,
        paramsSerializer: params => qs.stringify(params),
    });
}
export function getAssignmentRequest(id: number): Promise<AxiosResponse<IAssignment>> {
    return RequestService.axios.get(EndPoints.getAssignment(id));
}
export function postCreateNewAssignment(newAssignmentModel: IAssignmentForm): Promise<AxiosResponse<IAssignment>> {
    return RequestService.axios.post(EndPoints.createNewAssignment, newAssignmentModel);
}