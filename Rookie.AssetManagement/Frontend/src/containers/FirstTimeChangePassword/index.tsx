import React, { lazy, useEffect, useState } from "react";
import { Modal } from "react-bootstrap";
import { Form, Formik } from "formik";
import * as Yup from "yup";

import IFirstTimeChangePassword from "../../interfaces/IFirstTimeChangePassword";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import ModalHeader from "react-bootstrap/esm/ModalHeader";
import PasswordField from "../../components/FormInputs/PasswordField";
import { firstTimeChangePassword } from "../Authorize/reducer";
import { useHistory } from "react-router-dom";
import { HOME } from "../../constants/pages";

const Home = lazy(() => import("src/containers/Home"));

const initialValues: IFirstTimeChangePassword = {
  newPassword: "",
};

const validationSchema = Yup.object().shape({
  newPassword: Yup.string()
    .trim()
    .required("Required")
    .min(5, "New password must be at least 5 characters"),
});

const FirstTimeChangePassword = () => {
  const history = useHistory();
  const dispatch = useAppDispatch();

  const [isShow, setShow] = useState(true);

  const handleFirstTimeChangePassword = (values) => {
    dispatch(firstTimeChangePassword(values));
  };

  const { loading, error, account } = useAppSelector(
    (state) => state.authReducer
  );

  useEffect(() => {
    if (account?.status.localeCompare("Success") === 0) {
      setShow(false);
    }
  }, [account]);

  return (
    <>
      <div className="container">
        <Modal
          centered
          show={isShow}
          dialogClassName="modal-90w"     
        >
          <Modal.Header>
            <Modal.Title>Change password</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            <Formik
              initialValues={initialValues}
              validationSchema={validationSchema}
              onSubmit={(values) => handleFirstTimeChangePassword(values)}
            >
              {(action) => (
                <Form>
                  This is the first time you logged in you have to change your
                  password to continue
                  <PasswordField
                    name="newPassword"
                    label="New Password"
                    isrequired
                  />
                  {error?.error && (
                    <div className="invalid">{error.message}</div>
                  )}
                  <div className="text-right mt-5 ">
                    <button
                      className="btn btn-danger mr-3"
                      type="submit"
                      disabled={loading}
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
                  </div>
                </Form>
              )}
            </Formik>
          </Modal.Body>
        </Modal>
      </div>
    </>
  );
};

export default FirstTimeChangePassword;
