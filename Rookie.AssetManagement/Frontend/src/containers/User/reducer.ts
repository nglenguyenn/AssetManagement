import { PayloadAction } from '@reduxjs/toolkit';
import { createSlice } from '@reduxjs/toolkit';
import { Status, SetStatusType } from "src/constants/status";
import IUser from '../../interfaces/User/IUser';
import IPagedModel from '../../interfaces/IPagedModel';
import IQueryUserModel from '../../interfaces/User/IQueryUserModel';
import IError from '../../interfaces/IError';
import ISubmitAction from "src/interfaces/ISubmitActions";
import IUserForm from "src/interfaces/User/IUserForm";
import StateManager from 'react-select';

type UserState = {
    loading: boolean;
    userResult?: IUser;
    users: IPagedModel<IUser> | null;
    status?: number;
    error?: IError;
    disable: boolean;
    user: IUser | null;
    createdUser?: IUser;
    selectUser?: IUser | null;
    // showCreatedRecord: boolean;
}

const initialState: UserState = {
    users: null,
    loading: false,
    disable: false,
    user: null,
    selectUser: null,
    // showCreatedRecord: false,
}

const UserReducerSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setStatus: (state: UserState, action: PayloadAction<SetStatusType>) => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            }
        },
        getUsers: (state, action: PayloadAction<IQueryUserModel>): UserState => {

            return {
                ...state,
                loading: true,
            }
        },

        postUserComplete(state, action: PayloadAction<IUser>) {
            const result = action.payload;

            return {
                ...state,
                createdUser: result,
                showCreatedRecord: true,
            }

        },

        setUsers: (state, action: PayloadAction<IPagedModel<IUser>>): UserState => {
            const users = action.payload;

            return {
                ...state,
                users,
                loading: false,
            }
        },

        createNewUser: (state: UserState, action: PayloadAction<ISubmitAction<IUserForm>>) => {
            return {
                ...state,
                loading: true,
            }
        },
        cleanUp: (state) => ({
            ...state,
            loading: false,
            users: null,
            status: undefined,
            error: undefined,
            selectUser: null,
        }),

        getUser: (state: UserState, action: PayloadAction<number>) => ({
            ...state,
            loading: true
        }),

        setUser: (state, action: PayloadAction<IUser>): UserState => {
            const user = action.payload;
            return {
                ...state,
                user,
                loading: false,
            }
        },

        editUser: (state, action: PayloadAction<IUserForm>): UserState => {
            return {
                ...state,
                loading: false,
            }
        },

        setShowCreatedRecord: (state, action: PayloadAction<IUser | undefined>): UserState => {
            const value = action.payload;

            return {
                ...state,
                createdUser: value,
            }
        },
    }
});

export const {
    setStatus, getUsers, setUsers, createNewUser, cleanUp, getUser, setUser, postUserComplete, editUser, setShowCreatedRecord
} = UserReducerSlice.actions;

export default UserReducerSlice.reducer;
