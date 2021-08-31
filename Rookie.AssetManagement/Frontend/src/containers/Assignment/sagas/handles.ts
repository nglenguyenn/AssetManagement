import { call, put } from "redux-saga/effects";
import IError from "../../../interfaces/IError";
import { PayloadAction } from "@reduxjs/toolkit";
import { setStatus, cleanUp, setAssignments, postAssignmentComplete,setAssignment } from "../reducer";
import { Status } from "../../../constants/status";
import { getAssignmentsRequest, postCreateNewAssignment, getAssignmentRequest } from "./requests";
import IQueryAssignmentModel from "src/interfaces/Assignment/IQueryAssignmentModel";
import IAssignmentForm from 'src/interfaces/Assignment/IAssignmentForm';
import IAssignment from 'src/interfaces/Assignment/IAssignment';


export function* handleGetAssignments(action: PayloadAction<IQueryAssignmentModel>) {
    const query = action.payload;

    try {
        const { data } = yield call(getAssignmentsRequest, query);
        yield put(setAssignments(data));
    }
    catch (error: any) {
        const errorModel = error.response.data as IError;
        yield put(setStatus({
            status: Status.Failed,
            error: errorModel,
        }))
    }
}
export function* handleGetAssignment(action: PayloadAction<number>) {
    const values = action.payload;
    try {
       const { data } = yield call(getAssignmentRequest, values);
       yield put(setAssignment(data));
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
export function* handleCreateNewAssignment(action: PayloadAction<IAssignmentForm>)
{
    const values = action.payload;
    try {
        const { data } = yield call(postCreateNewAssignment, values);
        yield put(postAssignmentComplete(data as IAssignment));

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