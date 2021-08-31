import { PayloadAction } from '@reduxjs/toolkit';
import { createSlice } from '@reduxjs/toolkit';
import { Status, SetStatusType } from "../../constants/status";
import IPagedModel from '../../interfaces/IPagedModel';
import IError from '../../interfaces/IError';
import ISubmitAction from "../../interfaces/ISubmitActions";
import { useReducer } from 'react';
import IAssignment from 'src/interfaces/Assignment/IAssignment';
import IQueryAssignmentModel from "src/interfaces/Assignment/IQueryAssignmentModel";
import IAssignmentForm from 'src/interfaces/Assignment/IAssignmentForm';

type AssignmentState = {
    loading: boolean;
    assignmentResult?: IAssignment;
    assignments: IPagedModel<IAssignment> | null;
    status?: number;
    error?: IError;
    assignment: IAssignment | null;
    createdAssignment?: IAssignment;
    disable: boolean;
}

const initialState: AssignmentState = {
    assignments: null,
    loading: false,
    disable: false,
    assignment: null,
}

const AssignmentReducerSlide = createSlice({
    name: 'assignment',
    initialState,
    reducers: {
        setStatus: (state: AssignmentState, action: PayloadAction<SetStatusType>) => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            }
        },
        getAssignments: (state: AssignmentState, action: PayloadAction<IQueryAssignmentModel>) => {
            return {
                ...state,
                loading: true,
            }
        },
        createNewAssignment: (state: AssignmentState, action: PayloadAction<ISubmitAction<IAssignmentForm>>) => {
            return {
                ...state,
                loading: true,
            }
        },
        setShowCreatedRecord: (state, action: PayloadAction<IAssignment | undefined>): AssignmentState => {
            const value = action.payload;

            return {
                ...state,
                createdAssignment: value,
            }
        },
        setAssignments: (state, action: PayloadAction<IPagedModel<IAssignment>>): AssignmentState => {
            const assignments = action.payload;

            return {
                ...state,
                assignments,
                loading: false,
            }
        },
        postAssignmentComplete(state, action: PayloadAction<IAssignment>) {
            const result = action.payload;

            return {
                ...state,
                createdAssignment: result,
                showCreatedRecord: true,
            }
        },      
        cleanUp: (state) => ({
            ...state,
            loading: false,
            status: undefined,
            error: undefined,
        }),
        getAssignment: (state: AssignmentState, action: PayloadAction<number>) => ({
            ...state,
            loading: true
        }),
        setAssignment: (state, action: PayloadAction<IAssignment>): AssignmentState => {
            const assignment = action.payload;
            return {
                ...state,
                assignment,
                loading: false,
            }
        },
    }
});

export const {
    setStatus, 
    getAssignments, 
    setShowCreatedRecord, 
    setAssignments, 
    postAssignmentComplete,
    createNewAssignment, 
    cleanUp,
    getAssignment,
    setAssignment    
} = AssignmentReducerSlide.actions;

export default AssignmentReducerSlide.reducer;