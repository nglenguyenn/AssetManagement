import React, { useEffect, useState } from "react";
import { FunnelFill } from "react-bootstrap-icons";
import { Search, Users } from "react-feather";
import ReactMultiSelectCheckboxes from "react-multiselect-checkboxes";
import { Link } from "react-router-dom";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";
import { useAppSelector, useAppDispatch } from "src/hooks/redux";
import ISelectOption from "src/interfaces/ISelectOption";
import { FilterUserTypeOptions } from "src/constants/selectOptions";
import {
  ACCSENDING,
  DECSENDING,
  DEFAULT_USER_SORT_COLUMN_NAME,
  DEFAULT_PAGE_LIMIT,
} from "src/constants/paging";

const ListUser = () => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [selectedType, setSelectedType] = useState([
    { id: 1, label: "All", value: 0 },
  ] as ISelectOption[]);
  const handleChangeSearch = (e) => {
    e.preventDefault();

    const search = e.target.value;
    setSearch(search);
  };
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
              <span className="border p-2 pointer">
                <Search />
              </span>
            </div>
          </div>

          <div className="d-flex align-items-center ml-3">
            <Link to="/brand/create" type="button" className="btn btn-danger">
              Create new Brand
            </Link>
          </div>
        </div>     
      </div>
    </>
  );
};
export default ListUser;
