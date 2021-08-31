import React, { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import { Link, useHistory } from "react-router-dom";
import TextAreaField from "src/components/FormInputs/TextAreaField";
import DateField from "src/components/FormInputs/DateField";
import { useAppDispatch,useAppSelector } from "src/hooks/redux";
import { ASSIGNMENT } from "src/constants/pages";
import IAssignmentForm from "src/interfaces/Assignment/IAssignmentForm";
import TextSearchField from "src/components/FormInputs/TextSearchField";
import UserListModal from "./List/PopupModal/UserListModal";
import AssetListModal from "./List/PopupModal/AssetListModal";
import IUser from "src/interfaces/User/IUser";
import { formatName } from "src/utils/helper";
import IAsset from "src/interfaces/Asset/IAsset";
import { createNewAssignment,cleanUp } from "./reducer";
import { Status } from "src/constants/status";


const initialFormValues: IAssignmentForm = {
  userName: "",
  assetName: "",
  assignedDate: new Date(),
  note: "",
};

const validationSchema = Yup.object().shape({
  assignedDate: Yup.date().nullable().required("Required"),
  note: Yup.string().trim().required("Required"),
  user: Yup.string().trim().required("Required"),
  asset: Yup.string().trim().required("Required"),
});

type Props = {
  initialAssignmentForm?: IAssignmentForm;
};
const AssignmentFormContainer: React.FC<Props> = ({
  initialAssignmentForm = {
    ...initialFormValues,
  },
}) => {
  const dispatch = useAppDispatch();
  const history = useHistory();
  const {status, error, loading } = useAppSelector(
    (state) => state.assignmentReducer
  );
  const handleCreateNewAssignment = (values) => {
    dispatch(createNewAssignment(values));
  }

  const [showModalUserList, setShowModalUserList] = useState(false);
  const [modalSelectedUser, setModalSelectedUser] = useState(null as IUser | null);

  const [showModalAssetList, setShowModalAssetList] = useState(false);
  const [modalSelectedAsset, setModalSelectedAsset] = useState(null as IAsset | null);

  //User action
  const showUserListModal = () => {
    setShowModalUserList(true);
  };

  const hideUserListModal = () => {
    setShowModalUserList(false);
  };

  const saveUserListModal = (user: IUser | null) => {
    setModalSelectedUser(user);
    setShowModalUserList(false);
  }

  const cancelUserListModal = () => {
    setShowModalUserList(false);
  }

  //Asset action
  const showAssetListModal = () => {
    setShowModalAssetList(true);
  }

  const hideAssetListModal = () => {
    setShowModalAssetList(false);
  }

  const saveAssetListModal = (asset: IAsset | null) => {
    setModalSelectedAsset(asset);
    setShowModalAssetList(false);
  }

  const cancelAssetListModal = () => {
    setShowModalAssetList(false);
  }

  useEffect(() => {
    if (status === Status.Success) {
      dispatch(cleanUp());
      history.push(ASSIGNMENT);
    }
  }, [modalSelectedUser, modalSelectedAsset,status,error]);


  return (
    <Formik
      initialValues={initialAssignmentForm}
      enableReinitialize
      validationSchema={validationSchema}
      onSubmit={(values) => {
        values.assetId = modalSelectedAsset?.id;
        values.assignToId = modalSelectedUser?.id;
        handleCreateNewAssignment(values);
      }}
    >
      {(actions) =>
        <Form className="intro-y col-lg-6 col-12">
          <TextSearchField 
            name="user" 
            label="User" 
            readOnly 
            isrequired
            handleClickPopup={showUserListModal} 
            selectedValue={formatName(modalSelectedUser?.firstName, modalSelectedUser?.lastName)} />
          <TextSearchField 
            name="asset" 
            label="Asset" 
            readOnly 
            isrequired
            handleClickPopup={showAssetListModal} 
            selectedValue={modalSelectedAsset?.assetName} />
          <DateField label="Assigned Date" name="assignedDate" minDate={new Date()} />
          <TextAreaField name="note" label="Note" />
          <div className="d-flex">
            <div className="ml-auto">
              <button
                className="btn btn-danger"
                type="submit"
                disabled={!(actions.isValid && actions.dirty) || loading}
              >
                Save
                {loading && (
                  <img
                    src="/oval.svg"
                    alt=""
                    className="w-4 h-4 ml-2 inline-block mr-1"
                  />
                )}
              </button>
              <Link to={ASSIGNMENT} className="btn btn-outline-secondary ml-2">
                Cancel
              </Link>
            </div>
          </div>

          <UserListModal
            isShow={showModalUserList}
            onHide={hideUserListModal}
            onSave={saveUserListModal}
            onCancel={cancelUserListModal}
            hide={hideUserListModal}
            currentSelectUser={modalSelectedUser}
          />
          <AssetListModal
            isShow={showModalAssetList}
            onHide={hideAssetListModal}
            onSave={saveAssetListModal}
            onCancel={cancelAssetListModal}
            hide={hideAssetListModal}
            currentSelectAsset={modalSelectedAsset}
          />
        </Form>}
    </Formik>
  );
};
export default AssignmentFormContainer;