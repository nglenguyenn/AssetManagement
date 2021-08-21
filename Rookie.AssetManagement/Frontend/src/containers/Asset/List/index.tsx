import React, { useEffect, useState } from "react";
import ReactMultiSelectCheckboxes from "react-multiselect-checkboxes";
import { FunnelFill } from "react-bootstrap-icons";
import { Search } from "react-feather";
import { CREATE_ASSET } from "src/constants/pages";
import { Link } from "react-router-dom";
import {
  FilterAssetStateOptions,
  FilterAssetCategoryOptions,
} from "src/constants/selectOptions";
import ISelectOption from "src/interfaces/ISelectOption";
import { useAppDispatch } from "src/hooks/redux";
import { Row, Col } from "react-bootstrap";

const ListAsset = () => {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [selectedState, setSelectedState] = useState([
    { id: 0, label: "State", value: 0 },
  ] as ISelectOption[]);
  const [selectedCategory, setSelectedCategory] = useState([
    { id: 0, label: "Category", value: 0 },
  ] as ISelectOption[]);
  const handleChangeSearch = (e) => {
    e.preventDefault();

    const search = e.target.value;
    setSearch(search);
  };
  return (
    <>
      <div className="primaryColor text-title intro-x">Asset List</div>
      <div>
        <Row className="filter-bar mb-3">
          <Col>
            <ReactMultiSelectCheckboxes
              options={FilterAssetStateOptions}
              hideSearch={true}
              placeholderButtonLabel="Type"
              value={selectedState}
            />
          </Col>
          <Col>
            <ReactMultiSelectCheckboxes
              options={FilterAssetCategoryOptions}
              hideSearch={true}
              placeholderButtonLabel="Type"
              value={selectedCategory}
            />
          </Col>
          <Col>
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
          </Col>
          <div className="d-flex align-items-center ml-3">
            <Link to={CREATE_ASSET} type="button" className="btn btn-danger">
              Create new Asset
            </Link>
          </div>
        </Row>
      </div>
    </>
  );
};
export default ListAsset;
