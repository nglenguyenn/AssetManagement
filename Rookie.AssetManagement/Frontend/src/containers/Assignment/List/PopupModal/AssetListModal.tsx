import { Modal } from "react-bootstrap";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";
import { ACCSENDING, DECSENDING, DEFAULT_ASSET_SORT_COLUMN_NAME, DEFAULT_PAGE_LIMIT, DEFAULT_USER_SORT_COLUMN_NAME } from "src/constants/paging";
import React, { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import UserTable from "src/containers/User/List/UserTable";
import { Link, Search } from "react-feather";
import IAsset from "src/interfaces/Asset/IAsset";
import AssetTable from "src/containers/Asset/List/AssetTable";
import IQueryAssetModel from "src/interfaces/Asset/IQueryAssetModel";
import { getAssets, getHistory, setShowCreatedRecord } from "src/containers/Asset/reducer";
import { AvailableAsset } from "src/constants/Asset/StateConstants";
import { cleanUp } from "src/containers/Authorize/reducer";

type Props = {
    isShow: boolean;
    onHide?: Function;
    onSave: Function;
    onCancel: Function;
    hide: Function;
    currentSelectAsset: IAsset | null;
};

const AssetListModal: React.FC<Props> = ({
    isShow,
    onHide,
    hide,
    onSave,
    onCancel,
    currentSelectAsset = null }) => {

    const dispatch = useAppDispatch();

    const { assets, loading } = useAppSelector((state) => state.assetReducer);

    const [search, setSearch] = useState("");

    const [selectedAsset, setSelectedAsset] = useState(null as IAsset | null);

    const [query, setQuery] = useState({
        page: assets?.currentPage ?? 1,
        limit: DEFAULT_PAGE_LIMIT,
        sortOrder: ACCSENDING,
        sortColumn: DEFAULT_ASSET_SORT_COLUMN_NAME,
        state: [AvailableAsset],
    } as IQueryAssetModel);

    const handleChangeSearch = (e) => {
        e.preventDefault();

        const search = e.target.value;
        setSearch(search);
    };

    const handleSort = (sortColumn: string) => {
        const sortOrder = query.sortOrder === ACCSENDING ? DECSENDING : ACCSENDING;

        dispatch(setShowCreatedRecord(undefined));
        setQuery({
            ...query,
            sortColumn,
            sortOrder,
        })
    }

    const handleSearch = () => {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
            ...query,
            search,
        });
    };

    const handlePage = (page: number) => {
        dispatch(setShowCreatedRecord(undefined));

        setQuery({
            ...query,
            page,
        })
    }

    const onModalSave = () => {
        onSave(selectedAsset)
        setSelectedAsset(selectedAsset);
    }

    const onModalCancel = () => {
        onCancel()
        if (currentSelectAsset == null) {
            setSelectedAsset(null);
        }
    }

    const fetchData = () => {
        dispatch(getAssets(query));
    }

    useEffect(() => {
        fetchData();

        return () => {
            dispatch(cleanUp())
        }
    }, [query]);

    return (
        <>
            <Modal centered show={isShow} onHide={onHide} dialogClassName="modal-90w" aria-labelledby="login-modal"  >
                <Modal.Body>
                    <>
                        <div className="row align-items-center">
                            <h4 className="col primaryColor ">Select Asset</h4>
                            <div className="input-group col">
                                <input
                                    onChange={handleChangeSearch}
                                    value={search}
                                    type="text"
                                    className="form-control"
                                />
                                <span onClick={handleSearch} className="border p-2 pointer">
                                    <Search />
                                </span>
                            </div>
                        </div>
                        <AssetTable
                            assets={assets}
                            handlePage={handlePage}
                            handleSort={handleSort}
                            sortState={{
                                columnValue: query.sortColumn,
                                orderBy: query.sortOrder
                            }}
                            fetchData={fetchData}
                            isSelecting={true}
                            selectedAsset={selectedAsset}
                            setSelectRecord={setSelectedAsset}
                        />
                        <div className="text-center mt-3">
                            <button onClick={onModalSave} className="btn btn-danger mr-3" type="button">Save</button>
                            <button onClick={onModalCancel} className="btn btn-outline-secondary" type="button">Cancel</button>
                        </div>
                    </>
                </Modal.Body>
            </Modal>
        </>
    );

}


export default AssetListModal