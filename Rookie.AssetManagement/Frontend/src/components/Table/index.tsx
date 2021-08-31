import React from "react";
import { CaretDownFill, CaretUpFill } from "react-bootstrap-icons";
import IColumnOption from "src/interfaces/IColumnOption";

import Paging, { PageType } from "./Paging";
import { Search } from "react-feather";


export type SortType = {
  columnValue: string;
  orderBy: string;
};

type ColumnIconType = {
  colValue: string;
  sortState: SortType;
}

const ColumnIcon: React.FC<ColumnIconType> = ({ colValue, sortState }) => {
  if (colValue === sortState.columnValue && sortState.orderBy === 'Decsending') return <CaretUpFill />

  return (<CaretDownFill />);
};

type Props = {
  columns: IColumnOption[];
  children: React.ReactNode;
  sortState: SortType;
  handleSort: (colValue: string) => void;
  page?: PageType;
  isSelecting?: boolean;
};

const Table: React.FC<Props> = ({ columns, children, page, sortState, handleSort, isSelecting = false }) => {

  return (
    <>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th style={isSelecting ? {} : { display: "none" }}>
              </th>
              {
                columns.map((col, i) => (
                  <th key={i}>
                    <a className="btn" onClick={() => handleSort!(col.columnValue)}>
                      {col.columnName}
                      <ColumnIcon colValue={col.columnValue} sortState={sortState} />
                    </a>
                  </th>
                ))
              }
            </tr>
          </thead>

          <tbody>
            {children}
          </tbody>
        </table>
      </div>

      {
        (page && page.totalPage && page.totalPage > 1) && <Paging {...page} />
      }
    </>
  );
};

export default Table;
