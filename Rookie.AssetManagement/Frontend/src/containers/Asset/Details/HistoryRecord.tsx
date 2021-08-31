import IAssignment from "../../../interfaces/Assignment/IAssignment";
import { convertDate } from "../../../utils/formatDateTime";
import React from "react";

type RecordProps = {
    data: Array<IAssignment> | null | undefined;
}

const HistoryRecord: React.FC<RecordProps> = ({
    data
}) => {

    return (
        <table className="table-container table">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Assigned To</th>
                    <th>Assigned By</th>
                    <th>Returned Date</th>
                </tr>
            </thead>
            <tbody>
            {
                data ? 
                    data.map((record, index) => (
                        <tr key={index} >
                            <td>
                                {convertDate(record.assignedDate)}
                            </td>
                            <td>{record.assignedTo}</td>
                            <td>{record.assignedBy}</td>
                            <td>
                                {record.returnedDate != undefined ? convertDate(record.returnedDate) : ""}
                            </td>
                        </tr>
                    )) 
                :
                    <></>
            }
            </tbody>
        </table>
    )
}

export default HistoryRecord;