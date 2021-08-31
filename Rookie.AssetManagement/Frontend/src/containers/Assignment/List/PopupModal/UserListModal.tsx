import { Modal } from "react-bootstrap";
import IQueryUserModel from "src/interfaces/User/IQueryUserModel";
import { ACCSENDING, DECSENDING, DEFAULT_PAGE_LIMIT, DEFAULT_USER_SORT_COLUMN_NAME } from "src/constants/paging";
import React, { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { getUsers, setShowCreatedRecord } from "src/containers/User/reducer";
import UserTable from "src/containers/User/List/UserTable";
import { Link, Search } from "react-feather";
import IUser from "src/interfaces/User/IUser";
import { retry } from "redux-saga/effects";
import { cleanUp } from "src/containers/User/reducer";



type Props = {
    isShow: boolean;
    onHide?: Function;
    onSave: Function;
    onCancel: Function;
    hide: Function;
    currentSelectUser: IUser | null;
};

const UserListModal: React.FC<Props> = ({ isShow, onHide, hide, onSave, onCancel, currentSelectUser = null }) => {

    const dispatch = useAppDispatch();
    const { users, loading } = useAppSelector((state) => state.userReducer);

    const [selectedUser, setSelectedUser] = useState(null as IUser | null);

    const [search, setSearch] = useState("");

    const [query, setQuery] = useState({
        page: users?.currentPage ?? 1,
        limit: DEFAULT_PAGE_LIMIT,
        sortOrder: ACCSENDING,
        sortColumn: DEFAULT_USER_SORT_COLUMN_NAME,
    } as IQueryUserModel);

    const handleSort = (sortColumn: string) => {
        const sortOrder = query.sortOrder === ACCSENDING ? DECSENDING : ACCSENDING;

        if (sortColumn.localeCompare("fullName") == 0)
            sortColumn = "firstName";

        setQuery({
            ...query,
            sortColumn,
            sortOrder,
        })
    }

    const handleChangeSearch = (e) => {
        e.preventDefault();

        const search = e.target.value;
        setSearch(search);
    };

    const handleSearch = () => {
        setQuery({
            ...query,
            search,
        });
    };

    const handleSortColumn = (column): string => {
        if (column.localeCompare("firstName") == 0)
            return "fullName"

        return column
    }

    const handlePage = (page: number) => {
        dispatch(setShowCreatedRecord(undefined));

        setQuery({
            ...query,
            page,
        })
    }

    const fetchData = () => {
        dispatch(getUsers(query));
    }

    const onModalSave = () => {
        onSave(selectedUser)
        setSelectedUser(selectedUser);
    }

    const onModalCancel = () => {
        onCancel()
        if (currentSelectUser == null) {
            setSelectedUser(null);
        }
    }


    useEffect(() => {
        fetchData();

        return () => {
            dispatch(cleanUp());
        }
    }, [query]);

    return (
        <>
            <Modal centered show={isShow} onHide={onHide} dialogClassName="modal-90w" aria-labelledby="login-modal" >

                <Modal.Body>
                    <>
                        <div className="row align-items-center">
                            <h4 className="col primaryColor ">Select User</h4>
                            <div className="input-group col">
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
                        <UserTable
                            users={users}
                            handlePage={handlePage}
                            handleSort={handleSort}
                            sortState={{
                                columnValue: handleSortColumn(query.sortColumn),
                                orderBy: query.sortOrder
                            }}
                            fetchData={fetchData}
                            isSelecting={true}
                            selectedUser={selectedUser}
                            setSelectRecord={setSelectedUser}
                        />
                        <div className="text-center mt-3">
                            <button onClick={onModalSave} className="btn btn-danger mr-3" type="button">Save</button>
                            <button onClick={onModalCancel} className="btn btn-outline-secondary" type="button">Cancel</button>
                        </div>
                    </>
                </Modal.Body>
            </Modal>
        </>
    )
}

export default UserListModal;