import React from "react";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { getUser } from "../reducer";
import UserFormContainer from "../UserForm";

const EditUserContainer = () => {
    const { user } = useAppSelector(
        (state) => state.userReducer
    );

    return (
        <div className="ml-5">
            <div className="primaryColor text-title intro-x">Edit User</div>
            <div className="row">
                <UserFormContainer initialUserForm={ user ?? undefined } />
            </div>
        </div>
    );
};
export default EditUserContainer;
