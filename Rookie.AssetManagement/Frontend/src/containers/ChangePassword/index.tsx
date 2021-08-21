import React, { useEffect, useState } from "react";
import { Modal } from "react-bootstrap";
import { Form, Formik } from "formik";
import * as Yup from "yup";
import IChangepassWord from "../../interfaces/IChangePassword";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import PasswordField from "src/components/FormInputs/PasswordField";
import { changePassword, cleanUp } from "../Authorize/reducer";
import { useHistory } from "react-router-dom";
import { Status } from "src/constants/status";

const initialValues: IChangepassWord = {
  oldPassword: "",
  newPassword: "",
};

const validationSchema = Yup.object().shape({
  oldPassword: Yup.string().trim().required("Required"),
  newPassword: Yup.string()
    .trim()
    .required("Required")
    .min(5, "New password must be at least 5 characters"),
});

type Props = {
  isShow: boolean;
  onHide?: Function;
  hide: Function;
  children: React.ReactNode;
};

const ChangePasswordModal: React.FC<Props> = ({
  isShow,
  onHide,
  hide,
  children,
}) => {
  const history = useHistory();

  const dispatch = useAppDispatch();

  const { loading, error, status } = useAppSelector(
    (state) => state.authReducer
  );

  const handleChangePassword = (values) => {
    setIstouch(true);
    dispatch(changePassword(values));
  };

  const [success, setSuccess] = useState(false);

  const [isTouched, setIstouch] = useState(false);

  const handelCancel = () => {
    hide();
  };

  useEffect(() => {
    if (status === Status.Success && isTouched) {
      setSuccess(true);
    } else {
      setSuccess(false);
    }
  }, [status]);

  return (
    <Modal
      centered
      show={isShow}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="login-modal"
    >
      <Modal.Header closeButton>
        <Modal.Title id="login-modal">Change Password</Modal.Title>
      </Modal.Header>

      <Modal.Body>
        {success ? (
          <>
            <div className="text-center">
              Your password has been changed successfully!
            </div>
            <div className="text-right mt-5 ">
              <button
                className="btn btn-outline-secondary"
                onClick={handelCancel}
                type="button"
              >
                Close
              </button>
            </div>
          </>
        ) : (
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={(values) => handleChangePassword(values)}
          >
            {(actions) => (
              <Form>
                <PasswordField name="oldPassword" label="Old Password" />
                <PasswordField name="newPassword" label="New Password" />

                {error && <div className="invalid">{error}</div>}

                <div className="text-right mt-5 ">
                  <button
                    className="btn btn-danger mr-3"
                    type="submit"
                    disabled={!(actions.isValid && actions.dirty)}
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
                  {children}
                </div>
              </Form>
            )}
          </Formik>
        )}
      </Modal.Body>
    </Modal>
  );
};
export default ChangePasswordModal;
