import React, { useEffect, useState } from "react";
import { Search } from "react-feather";
import { FunnelFill } from "react-bootstrap-icons";
import ReactMultiSelectCheckboxes from "react-multiselect-checkboxes";
import { Link } from "react-router-dom";

import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";
import UserTable from "./UserTable";
import { ACCSENDING, DECSENDING, DEFAULT_PAGE_LIMIT, DEFAULT_USER_SORT_COLUMN_NAME } from "src/constants/paging";
import { cleanUp, getUsers, setShowCreatedRecord } from "../reducer";
import { FilterUserTypeOptions } from "src/constants/selectOptions";
import { USER_CREATE } from "src/constants/pages";
import ISelectOption from "src/interfaces/ISelectOption";
import { type } from "os";
import {
  AllUserTypeLabel, StaffUserTypeLabel, AdminUserTypeLabel,
  AllUserType, StaffUserType, AdminUserType
} from 'src/constants/User/RoleConstants';
import { NavItem } from "react-bootstrap";
import { select } from "redux-saga/effects";

const ListUser = () => {
  const dispatch = useAppDispatch();
  const { users, loading } = useAppSelector((state) => state.userReducer);

  const [search, setSearch] = useState("");

  const [selectedType, setSelectedType] = useState([
    ...FilterUserTypeOptions
  ] as ISelectOption[]);

  const [query, setQuery] = useState({
    page: users?.currentPage ?? 1,
    limit: DEFAULT_PAGE_LIMIT,
    sortOrder: ACCSENDING,
    sortColumn: DEFAULT_USER_SORT_COLUMN_NAME,
  } as IQueryUserModel);

  const handleType = (selected: ISelectOption[]) => {

    const exceptAll = FilterUserTypeOptions.filter((item) => item.id !== 0);

    const selectedAll = selected.find((item) => item.id === 0);


    if (selected.length === 0) {
      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        types: [],
      });

      setSelectedType([...FilterUserTypeOptions]);
      return;
    }

    setSelectedType((prevSelected) => {

      const newSelected = selected.filter((item) => item.id !== 0);

      if (newSelected.length === exceptAll.length) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          types: [],
        });

        setSelectedType([...FilterUserTypeOptions]);
        if (selectedAll !== undefined)
          return [selectedAll]
      }

      if (!prevSelected.some((item) => item.id === 0) && selectedAll) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          types: [],
        });

        setSelectedType([...FilterUserTypeOptions]);
        return [selectedAll];
      }


      const types = newSelected.map((item) => item.value as string);

      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        types,
      });

      return newSelected;
    });
  };

  const handlePage = (page: number) => {
    dispatch(setShowCreatedRecord(undefined));

    setQuery({
      ...query,
      page,
    })
  }

  const handleSort = (sortColumn: string) => {
    const sortOrder = query.sortOrder === ACCSENDING ? DECSENDING : ACCSENDING;

    if (sortColumn.localeCompare("fullName") == 0)
      sortColumn = "firstName";

    dispatch(setShowCreatedRecord(undefined));
    setQuery({
      ...query,
      sortColumn,
      sortOrder,
    })
  }

  const handleSortColumn = (column): string => {
    if (column.localeCompare("firstName") == 0)
      return "fullName"

    return column
  }


  const handleChangeSearch = (e) => {
    e.preventDefault();

    const search = e.target.value;
    setSearch(search);
  };

  const handleSearch = () => {
    dispatch(setShowCreatedRecord(undefined));
    setQuery({
      ...query,
      page: 1,
      search,
    });
  };

  const fetchData = () => {
    dispatch(getUsers(query));
  }


  useEffect(() => {
    fetchData();

    return () => {
      dispatch(cleanUp());
    }
  }, [query]);

  return (
    <>
      <div className="primaryColor text-title intro-x">User List</div>

      <div>
        <div className="d-flex mb-5 intro-x">
          <div className="d-flex align-items-center w-md mr-5">
            <ReactMultiSelectCheckboxes
              options={FilterUserTypeOptions}
              hideSearch={true}
              placeholderButtonLabel="Type"
              value={selectedType}
              onChange={handleType}
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
            <Link to={USER_CREATE} type="button" className="btn btn-danger">
              Create new user
            </Link>
          </div>
        </div>
        <UserTable
          users={users}
          handlePage={handlePage}
          handleSort={handleSort}
          sortState={{
            columnValue: handleSortColumn(query.sortColumn),
            orderBy: query.sortOrder
          }}
          fetchData={fetchData}
        />
      </div>
    </>
  );
};
export default ListUser;
