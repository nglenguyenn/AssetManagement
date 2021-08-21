import React, { useState } from "react";
import { Columns, PencilFill, VolumeDownFill, XCircle } from "react-bootstrap-icons";
import ButtonIcon from "src/components/ButtonIcon";
import IUser from "src/interfaces/User/IUser";
import { convertDate } from "src/utils/formatDateTime";


type RecordProps = {
    data: IUser;
    handleShowInfo: (id: number) => void;
    handleEdit: (id: number) => void;
    handleDisable: (id: number) => void;

}

const UserRecord: React.FC<RecordProps> = ({
    data,
    handleShowInfo,
    handleEdit,
    handleDisable,
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
                <td>{data.staffCode}</td>
                <td>{getFullName(data.firstName, data.lastName)}</td>
                <td> {data.userName} </td>
                <td> {convertDate(data.joinedDate)} </td>
                <td> {getType(data.type)} </td>

                <td className="d-flex">
                    <ButtonIcon onClick={() => handleEdit(data.id)}>
                        <PencilFill className="text-black" />
                    </ButtonIcon>
                    <ButtonIcon onClick={() => handleDisable(data.id)}>
                        <XCircle className="text-danger mx-2" />
                    </ButtonIcon>
                </td>
            </tr>
            : <></>
    )
};

export default UserRecord;