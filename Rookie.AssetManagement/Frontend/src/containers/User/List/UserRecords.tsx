import React, { useState } from "react";
import { Columns, PencilFill, VolumeDownFill, XCircle } from "react-bootstrap-icons";
import ButtonIcon from "src/components/ButtonIcon";
import IUser from "src/interfaces/User/IUser";
import { convertDate } from "src/utils/formatDateTime";
import { Search } from "react-feather";

type RecordProps = {
    data: IUser;
    handleShowInfo: (id: number) => void;
    handleEdit: (id: number) => void;
    handleDisable: (id: number) => void;
    isSelecting?: boolean;
    selectedUser?: IUser | null;
}

const UserRecord: React.FC<RecordProps> = ({
    data,
    handleShowInfo,
    handleEdit,
    handleDisable,
    isSelecting = false,
    selectedUser = null,
}) => {
    const getFullName = (firstName: string, lastName: string) => {
        if (firstName === undefined)
            return lastName;
        if (lastName === undefined)
            return firstName;

        const firstNameWithSpace = firstName.concat(" ");
        return firstNameWithSpace.concat(lastName);
        return firstName;
    };

    const getType = (type: string) => {
        return type;
    }

    return (
        data ?
            <tr className="" onClick={() => handleShowInfo(data.id)}>
                <td style={isSelecting ? {} : { display: "none" }}>
                    <input type="radio" checked={selectedUser?.id === data.id}></input>
                </td>
                <td>{data.staffCode}</td>
                <td>{getFullName(data.firstName, data.lastName)}</td>
                <td style={isSelecting ? { display: "none" } : {}}> {data.userName} </td>
                <td style={isSelecting ? { display: "none" } : {}}> {convertDate(data.joinedDate)} </td>
                <td> {getType(data.type)} </td>

                <td style={isSelecting ? { display: "none" } : {}} className="d-flex">
                    <ButtonIcon isSelecting={isSelecting} onClick={() => handleEdit(data.id)}>
                        <PencilFill className="text-black" />
                    </ButtonIcon>
                    <ButtonIcon isSelecting={isSelecting} onClick={() => handleDisable(data.id)}>
                        <XCircle className="text-danger mx-2" />
                    </ButtonIcon>
                </td>
            </tr>
            : <></>
    )
};

export default UserRecord;