import { PayloadAction } from '@reduxjs/toolkit';
import { createSlice } from '@reduxjs/toolkit';
import { Status, SetStatusType } from "../../constants/status";
import IUser from '../../interfaces/User/IUser';
import IPagedModel from '../../interfaces/IPagedModel';
import IQueryUserModel from '../../interfaces/User/IQueryUserModel';
import IError from '../../interfaces/IError';
import ISubmitAction from "../../interfaces/ISubmitActions";
import IAssetForm from "../../interfaces/Asset/IAssetForm"
import IAsset from '../../interfaces/Asset/IAsset';
import { useReducer } from 'react';
import IAssignment from 'src/interfaces/Assignment/IAssignment';
import IQueryAssetModel from 'src/interfaces/Asset/IQueryAssetModel';

type AssetState = {
    loading: boolean;
    assetResult?: IAsset;
    assets: IPagedModel<IAsset> | null;
    status?: number;
    error?: IError;
    disable: boolean;
    asset: IAsset | null;
    history: Array<IAssignment> | null;
    createdAsset?: IAsset;
}

const initialState: AssetState = {
    assets: null,
    loading: false,
    disable: false,
    asset: null,
    history: null
}

const AssetReducerSlide = createSlice({
    name: 'asset',
    initialState,
    reducers: {
        setStatus: (state: AssetState, action: PayloadAction<SetStatusType>) => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            }
        },
        createNewAsset: (state: AssetState, action: PayloadAction<ISubmitAction<IAssetForm>>) => {
            return {
                ...state,
                loading: true,
            }
        },
        getAssets: (state: AssetState, action: PayloadAction<IQueryAssetModel>) => {
            return {
                ...state,
                loading: true,
            }
        },
        setShowCreatedRecord: (state, action: PayloadAction<IAsset | undefined>): AssetState => {
            const value = action.payload;

            return {
                ...state,
                createdAsset: value,
            }
        },
        setAssets: (state, action: PayloadAction<IPagedModel<IAsset>>): AssetState => {
            const assets = action.payload;

            return {
                ...state,
                assets,
                loading: false,
            }
        },
        postAssetComplete(state, action: PayloadAction<IAsset>) {
            const result = action.payload;

            return {
                ...state,
                createdAsset: result,
                showCreatedRecord: true,
            }
        },
        editAsset :(state: AssetState, action: PayloadAction<ISubmitAction<IAssetForm>>) => {
            return {
                ...state,
                loading : true,
            }
        },

        
        cleanUp: (state) => ({
            ...state,
            loading: false,
            status: undefined,
            error: undefined,
        }),

        getAsset: (state: AssetState, action: PayloadAction<number>) => ({
            ...state,
            loading: true
        }),

        setAsset: (state, action: PayloadAction<IAsset>): AssetState => {
            const asset = action.payload;
            return {
                ...state,
                asset,
                loading: false,
            }
        },

        getHistory: (state: AssetState, action: PayloadAction<number>) => ({
            ...state,
            loading: true
        }),

        setHistory: (state, action: PayloadAction<Array<IAssignment>>): AssetState => {
            const history = action.payload;
            return {
                ...state,
                history,
                loading: false,
            }
        },
        
    }
});

export const {
    setStatus, 
    createNewAsset, 
    cleanUp, 
    getAssets, 
    setShowCreatedRecord, 
    setAssets, 
    postAssetComplete,
    getAsset, 
    setAsset, 
    getHistory, 
    setHistory,
    editAsset
} = AssetReducerSlide.actions;

export default AssetReducerSlide.reducer;