import React, { useEffect, useState } from "react";
import { Row, Col } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ASSIGNMENT_CREATE } from "src/constants/pages";
import { Search } from "react-feather";
import { FilterAssignmentStateOptions } from "src/constants/selectOptions";
import { FunnelFill } from "react-bootstrap-icons";
import IQueryAssignmentModel from "src/interfaces/Assignment/IQueryAssignmentModel";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import ISelectOption from "src/interfaces/ISelectOption";
import DatePicker from 'react-datepicker';
import { CalendarDateFill } from "react-bootstrap-icons";
import { ACCSENDING, DEFAULT_ASSIGNMENT_SORT_COLUMN_NAME, DEFAULT_PAGE_LIMIT, DECSENDING } from "src/constants/paging";
import AssignmentTable from "src/containers/Assignment/List/AssignmentTable";
import { getAssignments, setShowCreatedRecord } from "../reducer";
import ReactMultiSelectCheckboxes from "react-multiselect-checkboxes";


const ListAssignment = () => {

  const { assignments, createdAssignment } = useAppSelector((state) => state.assignmentReducer);

  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");

  const [selectedState, setSelectedState] = React.useState([
    ...FilterAssignmentStateOptions
  ]);

  const [selectedDate, setSelectedDate] = useState();

  const [query, setQuery] = useState({
    page: assignments?.currentPage ?? 1,
    limit: DEFAULT_PAGE_LIMIT,
    sortOrder: ACCSENDING,
    sortColumn: DEFAULT_ASSIGNMENT_SORT_COLUMN_NAME,
  } as IQueryAssignmentModel);

  const handleChangeSearch = (e) => {
    e.preventDefault();

    const search = e.target.value;
    setSearch(search);
  };

  const handleFilterDate = (date) => {
    dispatch(setShowCreatedRecord(undefined));
    setSelectedDate(date);
    if (date !== null) {
      date = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
      setQuery({
        ...query,
        page: 1,
        assignedDate: date,
      })
    } else {
      date = new Date("0001-01-01");
      setQuery({
        ...query,
        page: 1,
        assignedDate: date,
      })
    }
  };

  const handleState = (selected: ISelectOption[]) => {

    const exceptAll = FilterAssignmentStateOptions.filter((item) => item.id !== 0);

    const selectedAll = selected.find((item) => item.id === 0);


    if (selected.length === 0) {
      setQuery({
        ...query,
        page: 1,
        states: [],
      });

      setSelectedState([...FilterAssignmentStateOptions]);
      return;
    }

    setSelectedState((prevSelected) => {

      const newSelected = selected.filter((item) => item.id !== 0);

      if (newSelected.length === exceptAll.length) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          states: [],
        });

        setSelectedState([...FilterAssignmentStateOptions]);
        if (selectedAll !== undefined)
          return [selectedAll]
      }

      if (!prevSelected.some((item) => item.id === 0) && selectedAll) {
        dispatch(setShowCreatedRecord(undefined));
        setQuery({
          ...query,
          page: 1,
          states: [],
        });

        setSelectedState([...FilterAssignmentStateOptions]);
        return [selectedAll];
      }


      const states = newSelected.map((item) => item.value as string);

      dispatch(setShowCreatedRecord(undefined));
      setQuery({
        ...query,
        page: 1,
        states,
      });

      return newSelected;
    });
  };

  const handleSort = (sortColumn: string) => {
    const sortOrder = query.sortOrder === ACCSENDING ? DECSENDING : ACCSENDING;

    dispatch(setShowCreatedRecord(undefined));
    setQuery({
      ...query,
      sortColumn,
      sortOrder,
    })
  };

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
  };

  const fetchData = () => {
    dispatch(getAssignments(query));
  }

  useEffect(() => {
    fetchData();
  }, [query]);

  return (
    <>
      <div className="primaryColor text-title intro-x">Assignment List</div>

      <div>
        <div className="d-flex mb-5 intro-x">
          <div className="d-flex align-items-center w-md mr-5">
            <ReactMultiSelectCheckboxes
              options={FilterAssignmentStateOptions}
              value={selectedState}
              hideSearch={true}
              onChange={handleState}
              placeholderButtonLabel="States"
            />
            <div className="border p-2">
              <FunnelFill />
            </div>
          </div>

          <div className="d-flex align-items-center w-md mr-5">
            <DatePicker
              placeholderText="Assigned Date"
              className={`w-100 text-center form-control`}
              dateFormat='MM/dd/yyyy'
              selected={selectedDate}
              onChange={date => handleFilterDate(date as Date)}
              isClearable
              showDisabledMonthNavigation
              showMonthDropdown
              showYearDropdown
            />
            <div className="border p-2">
              <CalendarDateFill />
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
            <Link to={ASSIGNMENT_CREATE} type="button" className="btn btn-danger">
              Create new Assignment
            </Link>
          </div>
        </div>
      </div>
      <AssignmentTable
        assignments={assignments}
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
export default ListAssignment;
