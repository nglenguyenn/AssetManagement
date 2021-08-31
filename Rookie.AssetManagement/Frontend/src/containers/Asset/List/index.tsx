import React, { useEffect, useState } from "react";
import { Search } from "react-feather";
import { CREATE_ASSET } from "src/constants/pages";
import { Link } from "react-router-dom";
import {
  FilterAssetStateOptions,
  FilterAssetCategoryOptions,
} from "src/constants/assetOptions";
import AssetDetailsModal from "../Details";
import { getAsset, getHistory } from "../reducer";

import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { Row, Col } from "react-bootstrap";
import { ACCSENDING, DEFAULT_ASSET_SORT_COLUMN_NAME, DEFAULT_PAGE_LIMIT, DECSENDING } from "src/constants/paging";
import IQueryAssetModel from "src/interfaces/Asset/IQueryAssetModel";
import AssetTable from "./AssetTable";
import ISelectOption from "src/interfaces/ISelectOption";
import { getAssets, setShowCreatedRecord } from "../reducer";
import { FunnelFill } from "react-bootstrap-icons";
import ReactMultiSelectCheckboxes from "react-multiselect-checkboxes";
import {
  AvailableAssetLabel,
  AssignedAssetLabel,
  UnavailableAssetLabel,
  AvailableAsset,
  AssignedAsset,
  UnavailableAsset
} from "src/constants/Asset/StateConstants";


const ListAsset = () => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [itemsSeleted, setItemsSeleted] = React.useState([]);
  const [showAssetModalDetails, setShowAssetModalDetails] = useState(false);
  const { asset, history } = useAppSelector((state) => state.assetReducer);

  const handleShowInfo = (id: number) => {
    dispatch(getAsset(id));
    dispatch(getHistory(id));
    setShowAssetModalDetails(true);
  }

  const hideAssetModal = () => {
    setShowAssetModalDetails(false);
  }

  const { assets, loading } = useAppSelector((state) => state.assetReducer);

  const handleChangeSearch = (e) => {
    e.preventDefault();

    const search = e.target.value;
    setSearch(search);
  };

  const [selectedCategory, setSelectedCategory] = useState([
    ...FilterAssetCategoryOptions
  ] as ISelectOption[]);

  const [selectedState, setSelectedState] = useState([
    { id: 1, label: AssignedAssetLabel, value: AssignedAsset },
    { id: 2, label: AvailableAssetLabel, value: AvailableAsset },
    { id: 3, label: UnavailableAssetLabel, value: UnavailableAsset },
  ] as ISelectOption[]);

  const [query, setQuery] = useState({
    page: assets?.currentPage ?? 1,
    limit: DEFAULT_PAGE_LIMIT,
    sortOrder: ACCSENDING,
    sortColumn: DEFAULT_ASSET_SORT_COLUMN_NAME,
  } as IQueryAssetModel);

  const handleCategory = (selected: ISelectOption[]) => {

    const exceptAll = FilterAssetCategoryOptions.filter((item) => item.id !== 0);

    const selectedAll = selected.find((item) => item.id === 0);


    if (selected.length === 0) {
      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        categoryName: [],
      });

      setSelectedCategory([...FilterAssetCategoryOptions]);
      return;
    }

    setSelectedCategory((prevSelected) => {

      const newSelected = selected.filter((item) => item.id !== 0);

      if (newSelected.length === exceptAll.length) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          categoryName: [],
        });

        setSelectedCategory([...FilterAssetCategoryOptions]);
        if (selectedAll !== undefined)
          return [selectedAll]
      }

      if (!prevSelected.some((item) => item.id === 0) && selectedAll) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          categoryName: [],
        });

        setSelectedCategory([...FilterAssetCategoryOptions]);
        return [selectedAll];
      }


      const categoryName = newSelected.map((item) => item.value as string);

      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        categoryName,
      });

      return newSelected;
    });
  };

  const handleState = (selected: ISelectOption[]) => {

    const exceptAll = FilterAssetStateOptions.filter((item) => item.id !== 0);

    const selectedAll = selected.find((item) => item.id === 0);


    if (selected.length === 0) {
      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        state: [],
      });

      setSelectedState([...FilterAssetStateOptions]);
      return;
    }

    setSelectedState((prevSelected) => {

      const newSelected = selected.filter((item) => item.id !== 0);

      if (newSelected.length === exceptAll.length) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          state: [],
        });

        setSelectedState([...FilterAssetStateOptions]);
        if (selectedAll !== undefined)
          return [selectedAll]
      }

      if (!prevSelected.some((item) => item.id === 0) && selectedAll) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          state: [],
        });

        setSelectedState([...FilterAssetStateOptions]);
        return [selectedAll];
      }


      const state = newSelected.map((item) => item.value as string);

      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        state,
      });

      return newSelected;
    });
  }

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
      page: 1,
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

  const fetchData = () => {
    dispatch(getAssets(query));
  }

  useEffect(() => {
    fetchData();
  }, [query]);

  return (
    <>
      <div className="primaryColor text-title intro-x">Asset List</div>
      <div>
        <div className="d-flex mb-5 intro-x">
          <div className="d-flex align-items-center w-md mr-5">
            <ReactMultiSelectCheckboxes
              options={FilterAssetStateOptions}
              value={selectedState}
              hideSearch={true}
              placeholderButtonLabel="States"
              onChange={handleState}
            />

            <div className="border p-2">
              <FunnelFill />
            </div>
          </div>
          <div className="d-flex align-items-center w-md mr-5">
            <ReactMultiSelectCheckboxes
              options={FilterAssetCategoryOptions}
              value={selectedCategory}
              hideSearch={true}
              placeholderButtonLabel="Categories"
              onChange={handleCategory}
            />

            <div className="border p-2">
              <FunnelFill />
            </div>
          </div>

          <div className="d-flex align-items-center w-ld ml-auto">
            <div className="input-group">
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

          <div className="d-flex align-items-center ml-3">
            <Link to={CREATE_ASSET} type="button" className="btn btn-danger">
              Create new Asset
            </Link>
          </div>
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
      />
    </>
  );
};
export default ListAsset;
