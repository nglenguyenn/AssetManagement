import { userInfo } from "os";
import React, { useState } from "react";
import { Columns, PencilFill, VolumeDownFill, XCircle } from "react-bootstrap-icons";
import { useHistory } from "react-router-dom";
import ButtonIcon from "src/components/ButtonIcon";
import Table, { SortType } from "src/components/Table";
import { USER_EDIT } from "src/constants/pages";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import IColumnOption from "src/interfaces/IColumnOption";
import IPagedModel from "src/interfaces/IPagedModel";
import IUser from "src/interfaces/User/IUser";
import { boolean } from "yup";
import UserDetailsModal from "../Details";
import { getUser } from "../reducer";
import UserRecord from "./UserRecords";
import { convertDate } from "src/utils/formatDateTime";
const columns: IColumnOption[] = [
  { columnName: "Staff Code", columnValue: "staffCode" },
  { columnName: "Full Name", columnValue: "fullName" },
  { columnName: "Username", columnValue: "userName" },
  { columnName: "Joined Date", columnValue: "joinedDate" },
  { columnName: "Type", columnValue: "type" },
];

type Props = {
  users: IPagedModel<IUser> | null;
  handlePage: (page: number) => void;
  handleSort: (colValue: string) => void;
  sortState: SortType;
  fetchData: Function;
}

const UserTable: React.FC<Props> = ({
  users,
  handlePage,
  handleSort,
  sortState,
  fetchData,
}) => {
  const dispatch = useAppDispatch();
  const { user, createdUser, userResult } = useAppSelector((state) => state.userReducer);
  const history = useHistory();

  const [userDetail, setUserDetail] = useState(null as IUser | null);

  const foundUser = users?.items.find(item => item.id === createdUser?.id);

  const [disableState, setDisable] = useState({
    isOpen: false,
    id: 0,
    title: '',
    message: '',
    isDisable: true,
  })

  const handleShowInfo = (id: number) => {
    dispatch(getUser(id));
    showUserDetailsModal();
  };

  const handleEdit = (id: number) => {
    dispatch(getUser(id));
    history.push(USER_EDIT);
  };

  const handleDisable = (id: number) => {
    // disable request; 
  };

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

  const [showModalUserDetails, setShowModalUserDetails] = useState(false);

  const showUserDetailsModal = () => {
    setShowModalUserDetails(true);
  };

  const hideUserDetailsModal = () => {
    setShowModalUserDetails(false);
  };

  return (
    <>
      <Table
        columns={columns}
        handleSort={handleSort}
        sortState={sortState}
        page={{
          currentPage: users?.currentPage,
          totalPage: users?.totalPages,
          handleChange: handlePage,
        }}
      >
        {
          (createdUser !== undefined) && (foundUser === undefined) ?
            <UserRecord
              data={createdUser}
              handleDisable={handleDisable}
              handleEdit={handleEdit}
              handleShowInfo={handleShowInfo}
            />
            : <></>
        }
        {
          users?.items.map((data, index) => (
            <UserRecord key={index}
              data={data}
              handleDisable={handleDisable}
              handleEdit={handleEdit}
              handleShowInfo={handleShowInfo}
            />
          ))}
      </Table>
            
      <UserDetailsModal 
        isShow={showModalUserDetails} 
        onHide={hideUserDetailsModal} 
        hide={hideUserDetailsModal}
        user={user} />
    </>);
};

export default UserTable;
