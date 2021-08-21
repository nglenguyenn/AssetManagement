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

type AssetState = {
    loading: boolean;
    assetResult?: IAsset;
    assets: IPagedModel<IAsset> | null;
    status?: number;
    error?: IError;
    disable: boolean;
    asset: IAsset | null;
}

const initialState : AssetState = {
    assets: null,
    loading: false,
    disable: false,
    asset: null,
}

const AssetReducerSlide = createSlice({
    name: 'asset',
    initialState,
    reducers:{
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

        cleanUp: (state) => ({
            ...state,
            loading: false,
            status: undefined,
            error: undefined,
        }),
        
    }
})
export const {
    setStatus,createNewAsset,cleanUp
 } = AssetReducerSlide.actions;

 export default AssetReducerSlide.reducer;