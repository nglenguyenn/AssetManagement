import React, { useState } from "react";
import { Columns, PencilFill, VolumeDownFill, XCircle } from "react-bootstrap-icons";
import ButtonIcon from "src/components/ButtonIcon";
import IAsset from "src/interfaces/Asset/IAsset";
import { convertDate } from "src/utils/formatDateTime";
import { AssetCategoryOptions, AssetStates } from "src/constants/assetOptions";
import { AssignedAssetLabel } from "../../../constants/Asset/StateConstants";

type RecordProps = {
    data: IAsset;
    handleShowInfo: (id: number) => void;
    handleEdit: (id: number) => void;
    // handleDisable: (id: number) => void;
    isSelecting?: boolean;
    selectedAsset?: IAsset | null;
}

const UserRecord: React.FC<RecordProps> = ({
    data,
    handleShowInfo,
    handleEdit,
    // handleDisable,
    isSelecting = false,
    selectedAsset = null,
}) => {
    const getCategory = (id) => {
        return AssetCategoryOptions.find(item => item.id === id)?.label;
    }

    const getState = (stateString: string) => {
        return AssetStates.find(item => item.value as string === stateString)?.label;
    }

    return (
        data ?
            <tr className="" onClick={() => handleShowInfo(data.id)}>
                <td style={isSelecting ? {} : { display: "none" }}>
                    <input type="radio" checked={selectedAsset?.id === data.id}></input>
                </td>
                <td>{data.assetCode}</td>
                <td>{data.assetName}</td>
                <td>{getCategory(data.categoryId)} </td>
                <td style={isSelecting ? { display: "none" } : {}}>{getState(data.state)}</td>
                <td style={isSelecting ? { display: "none" } : {}} className="d-flex">
                    <ButtonIcon 
                        isSelecting={isSelecting} 
                        onClick={() => handleEdit(data.id)}
                        disable={data.state === AssignedAssetLabel ? true : false} >
                        <PencilFill className="text-black" />
                    </ButtonIcon>
                    <ButtonIcon isSelecting={isSelecting} /*onClick={() => handleDisable(data.id)}*/>
                        <XCircle className="text-danger mx-2" />
                    </ButtonIcon>
                </td>
            </tr>
            : <></>
    )
};

export default UserRecord;