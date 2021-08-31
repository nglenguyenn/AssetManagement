import React, { useState } from "react";
import { Columns, PencilFill, VolumeDownFill, XCircle, ArrowCounterclockwise } from "react-bootstrap-icons";
import ButtonIcon from "src/components/ButtonIcon";
import { convertDate } from "src/utils/formatDateTime";
import IAssignment from "src/interfaces/Assignment/IAssignment";
import { AssignmentStates } from "src/constants/selectOptions";
import {
    AcceptedAssignmentString,
    WaitingAssignmentString
  } from "src/constants/Assignment/StateConstants";


type RecordProps = {
    data: IAssignment;
    handleShowInfo: (id: number) => void;
    //handleEdit: (id: number) => void;
    // handleDisable: (id: number) => void;
}

const AssignmentRecord: React.FC<RecordProps> = ({
    data,
    handleShowInfo,
    //handleEdit,
    // handleDisable,
}) => {

    const getState = (stateString: string) => {
        return AssignmentStates.find(item => item.value as string === stateString)?.label;
    }

    const isWaitingAssignment = (stateString: string) => {
        return stateString === WaitingAssignmentString;
    }

    return (
        data ? 
            <tr className="" onClick={() => handleShowInfo(data.id)} >
                <td>{data.id}</td>
                <td>{data.assetCode}</td>
                <td>{data.assetName} </td>
                <td>{data.assignedTo}</td>
                <td>{data.assignedBy}</td>
                <td>{convertDate(data.assignedDate)}</td>
                <td>{getState(data.state)}</td>
                <td className="d-flex">
                    <ButtonIcon disable={!isWaitingAssignment(data.state)} /*onClick={() => handleEdit(data.id)}*/>
                        <PencilFill className="text-black" />
                    </ButtonIcon>
                    <ButtonIcon disable={!isWaitingAssignment(data.state)} /*onClick={() => handleDisable(data.id)}*/>
                        <XCircle className="text-danger mx-2" />
                    </ButtonIcon>
                    <ButtonIcon disable={isWaitingAssignment(data.state)} /*onClick={() => handleReturn()}*/>
                        <ArrowCounterclockwise className="text-primary" />
                    </ButtonIcon>
                </td>
            </tr>
            : <></>
    )
};

export default AssignmentRecord;