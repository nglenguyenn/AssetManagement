import React from "react";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import {  } from "../reducer";
import AssetFormContainer from "../AssetForm";

const EditAssetContainer = () => {
    const { asset } = useAppSelector(
        (state) => state.assetReducer
    );

    return (
        <div className="ml-5">
            <div className="primaryColor text-title intro-x">Edit Asset</div>
            <div className="row">
                <AssetFormContainer initialAssetForm={ asset ?? undefined } />
            </div>
        </div>
    );
};
export default EditAssetContainer;