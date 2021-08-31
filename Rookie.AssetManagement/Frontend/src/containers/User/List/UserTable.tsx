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
import { formatName } from "src/utils/helper";


const columns: IColumnOption[] = [
  { columnName: "Staff Code", columnValue: "staffCode", isSelected: true },
  { columnName: "Full Name", columnValue: "fullName", isSelected: true },
  { columnName: "Username", columnValue: "userName", isSelected: false },
  { columnName: "Joined Date", columnValue: "joinedDate", isSelected: false },
  { columnName: "Type", columnValue: "type", isSelected: true },
];

type Props = {
  users: IPagedModel<IUser> | null;
  handlePage: (page: number) => void;
  handleSort: (colValue: string) => void;
  sortState: SortType;
  fetchData: Function;
  isSelecting?: boolean;
  setSelectRecord?: (user: IUser | null) => void;
  selectedUser?: IUser | null;
}

const UserTable: React.FC<Props> = ({
  users,
  handlePage,
  handleSort,
  sortState,
  fetchData,
  isSelecting = false,
  setSelectRecord,
  selectedUser = null,
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

  const handleSelect = (id: number) => {
    const foundUser = users?.items.find(user => user.id == id) || null
    if (setSelectRecord) setSelectRecord(foundUser);
  }

  const handleEdit = (id: number) => {
    dispatch(getUser(id));
    history.push(USER_EDIT);
  };

  const handleDisable = (id: number) => {
    // disable request; 
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


  const handleShowColumn = (): IColumnOption[] => {
    if (isSelecting) {
      return columns.filter(column => column.isSelected === true)
    }

    return columns
  }

  return (
    <>
      <Table
        columns={handleShowColumn()}
        handleSort={handleSort}
        sortState={sortState}
        page={{
          currentPage: users?.currentPage,
          totalPage: users?.totalPages,
          handleChange: handlePage,
        }}
        isSelecting={isSelecting}
      >
        {
          isSelecting === false ?
            (createdUser !== undefined) ?
              <UserRecord
                data={createdUser}
                handleDisable={handleDisable}
                handleEdit={handleEdit}
                handleShowInfo={isSelecting ? handleSelect : handleShowInfo}
                isSelecting={isSelecting}
                selectedUser={selectedUser}
              />
              : <></>
            : <></>
        }
        {
          users?.items.map((data, index) => (
            (data.id !== foundUser?.id || isSelecting == true) ?
              <UserRecord key={index}
                data={data}
                handleDisable={handleDisable}
                handleEdit={handleEdit}
                handleShowInfo={isSelecting ? handleSelect : handleShowInfo}
                isSelecting={isSelecting}
                selectedUser={selectedUser}
              /> : <></>
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
