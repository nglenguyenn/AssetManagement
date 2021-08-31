import React, { useState } from "react";
import IColumnOption from "src/interfaces/IColumnOption";
import IPagedModel from "src/interfaces/IPagedModel";
import Table, { SortType } from "src/components/Table";
import IAsset from "src/interfaces/Asset/IAsset";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { useHistory } from "react-router-dom";
import AssetRecord from "./AssetRecords";
import { getAsset, getHistory } from "../reducer";
import AssetDetailsModal from "src/containers/Asset/Details";
import { EDIT_ASSET } from "src/constants/pages"

type Props = {
    assets: IPagedModel<IAsset> | null;
    handlePage: (page: number) => void;
    handleSort: (colValue: string) => void;
    sortState: SortType;
    fetchData: Function;
    isSelecting?: boolean;
    setSelectRecord?: (asset: IAsset | null) => void;
    selectedAsset?: IAsset | null;
}

const columns: IColumnOption[] = [
    { columnName: "Asset Code", columnValue: "assetCode", isSelected: true },
    { columnName: "Asset Name", columnValue: "assetName", isSelected: true },
    { columnName: "Category", columnValue: "categoryId", isSelected: true },
    { columnName: "State", columnValue: "state", isSelected: false },
];

const AssetTable: React.FC<Props> = ({
    assets,
    handlePage,
    handleSort,
    sortState,
    fetchData,
    isSelecting = false,
    setSelectRecord,
    selectedAsset = null,
}) => {

    const dispatch = useAppDispatch();
    const { asset, createdAsset, history } = useAppSelector((state) => state.assetReducer);
    const [showAssetModalDetails, setShowAssetModalDetails] = useState(false);
    const foundAsset = assets?.items.find(item => item.id === createdAsset?.id);
    const historic = useHistory();
    
    const handleShowInfo = (id: number) => {
        dispatch(getAsset(id));
        dispatch(getHistory(id));
        setShowAssetModalDetails(true);
    }

    const handleSelect = (id: number) => {
        const foundAsset = assets?.items.find(asset => asset.id == id) || null
        if (setSelectRecord) setSelectRecord(foundAsset);
    }

    const hideAssetModal = () => {
        setShowAssetModalDetails(false);
    }

    const handleShowColumn = (): IColumnOption[] => {
        if (isSelecting) {
            return columns.filter(column => column.isSelected === true)
        }
        return columns
    }

    const handleEdit = (id: number) => {
        dispatch(getAsset(id));
        historic.push(EDIT_ASSET);
    }

    return (
        <>
            <Table
                columns={handleShowColumn()}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: assets?.currentPage,
                    totalPage: assets?.totalPages,
                    handleChange: handlePage,
                }}
                isSelecting={isSelecting}
            >
                {
                    isSelecting === false ?
                        (createdAsset !== undefined) ?
                            <AssetRecord
                                data={createdAsset}
                                // handleDisable={handleDisable}
                                handleEdit={handleEdit}
                                handleShowInfo={isSelecting ? handleSelect : handleShowInfo}
                                isSelecting={isSelecting}
                                selectedAsset={selectedAsset}
                            />
                            : <></>
                        : <></>
                }
                {
                    assets?.items.map((data, index) => (
                        (data.id !== foundAsset?.id || isSelecting == true) ?
                            <AssetRecord key={index}
                                data={data}
                                // handleDisable={handleDisable}
                                handleEdit={handleEdit}
                                handleShowInfo={isSelecting ? handleSelect : handleShowInfo}
                                isSelecting={isSelecting}
                                selectedAsset={selectedAsset}
                            /> : <></>
                    ))
                }
            </Table>
            <AssetDetailsModal
                isShow={showAssetModalDetails}
                hide={hideAssetModal}
                onHide={hideAssetModal}
                asset={asset}
                historyAssignment={history} />
        </>
    );
};
export default AssetTable;