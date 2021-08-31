import React, { useState } from "react";
import IColumnOption from "src/interfaces/IColumnOption";
import IPagedModel from "src/interfaces/IPagedModel";
import Table, { SortType } from "src/components/Table";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { useHistory } from "react-router-dom";
import IAssignment from "src/interfaces/Assignment/IAssignment";
import AssignmentRecord from "./AssignmentRecord";
import {getAssignment} from 'src/containers/Assignment/reducer'
import AssignmentDetailsModal from 'src/containers/Assignment/Details/index'

type Props = {
    assignments: IPagedModel<IAssignment> | null;
    handlePage: (page: number) => void;
    handleSort: (colValue: string) => void;
    sortState: SortType;
    fetchData: Function;
    isSelecting?: boolean,
    setSelectRecord?: (asset: IAssignment | null) => void;
}

const columns: IColumnOption[] = [
    { columnName: "No.", columnValue: "id", isSelected: true },
    { columnName: "Asset Code", columnValue: "asset.AssetCode", isSelected: true },
    { columnName: "Asset Name", columnValue: "asset.AssetName", isSelected: true },
    { columnName: "Assigned To", columnValue: "assignTo.UserName", isSelected: false },
    { columnName: "Assigned By", columnValue: "assignBy.UserName", isSelected: false },
    { columnName: "Assigned Date", columnValue: "assignedDate", isSelected: false },
    { columnName: "State", columnValue: "state", isSelected: false },
];

const AssignmentTable: React.FC<Props> = ({
    assignments,
    handlePage,
    handleSort,
    sortState,
    fetchData,
    isSelecting = false,
    setSelectRecord,
}) => {
    const [showAssignmentModalDetails, setShowAssignmentModalDetails] = useState(false);
    const dispatch = useAppDispatch();
    const { assignment, createdAssignment } = useAppSelector((state) => state.assignmentReducer);
    const foundAssignment = assignments?.items.find(item => item.id === createdAssignment?.id);
    
    const handleShowInfo = (id: number) => {
        dispatch(getAssignment(id));       
        setShowAssignmentModalDetails(true);
    }
    const showAssignmentDetailsModal = () => {
        setShowAssignmentModalDetails(true);
      };
    
      const hideAssignmentDetailsModal = () => {
        setShowAssignmentModalDetails(false);
      };
    return (
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: assignments?.currentPage,
                    totalPage: assignments?.totalPages,
                    handleChange: handlePage,
                }}
                isSelecting={isSelecting}
            >
                {
                    isSelecting === false ?
                        (createdAssignment !== undefined) ?
                            <AssignmentRecord
                                data={createdAssignment}
                                // handleDisable={handleDisable}
                                // handleEdit={handleEdit}
                                handleShowInfo={handleShowInfo}
                            />
                            : <></>
                        : <></>
                }
                {
                    assignments?.items.map((data, index) => (
                        (data.id !== foundAssignment?.id || isSelecting == true) ?
                            <AssignmentRecord key={index}
                                data={data}                              
                                // handleDisable={handleDisable}
                                // handleEdit={handleEdit}
                                handleShowInfo={handleShowInfo}
                            /> : <></>
                    ))
                }
            </Table>
            <AssignmentDetailsModal
        isShow={showAssignmentModalDetails}
        onHide={hideAssignmentDetailsModal}
        hide={hideAssignmentDetailsModal}
        assignment={assignment} />
        </>
    );
};
export default AssignmentTable;