import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Modal } from "react-bootstrap";
import { Form, Formik } from "formik";
import * as Yup from "yup";

import { HOME, FIRST_TIME_CHANGE_PASSWORD } from "../../constants/pages";
import HeaderLogIn from "../Layout/HeaderLogIn";
import TextField from "src/components/FormInputs/TextField";
import ILoginModel from "src/interfaces/ILoginModel";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { cleanUp, login } from "./reducer";

const initialValues: ILoginModel = {
  userName: "",
  password: "",
};

const validationSchema = Yup.object().shape({
  userName: Yup.string().trim().required("Required"),
  password: Yup.string().trim().required("Required"),
});

const Login = () => {
  const history = useHistory();

  const [isShow, setShow] = useState(true);

  const dispatch = useAppDispatch();
  const { loading, error, isAuth } = useAppSelector(
    (state) => state.authReducer
  );

  const handleHide = () => {
    setShow(false);
  };

  const handleLogin = (values) => {
    dispatch(login(values));
  };

  useEffect(() => {
    if (isAuth) {
      history.push(HOME);
    }

    return () => {
      dispatch(cleanUp());
    };
  }, [isAuth]);

  return (
    <>
      <div>
        <HeaderLogIn />
      </div>

      <div className="container">
        <Modal
          centered
          show={isShow}
          dialogClassName="modal-90w"
          aria-labelledby="login-modal"
        >
          <Modal.Header>
            <Modal.Title id="login-modal">
              Welcome to Online Asset Management
            </Modal.Title>
          </Modal.Header>

          <Modal.Body>
            <Formik
              initialValues={initialValues}
              validationSchema={validationSchema}
              onSubmit={(values) => {
                handleLogin(values);
              }}
            >
              {(actions) => (
                <Form className="intro-y">
                  <TextField name="userName" label="Username" isrequired />
                  <TextField
                    name="password"
                    label="Password"
                    type="password"
                    isrequired
                  />

                  {(
                    <div className="invalid">{error}</div>
                  )}

                  <div className="text-center mt-5">
                    <button
                      className="btn btn-danger"
                      type="submit"
                      disabled={!(actions.isValid && actions.dirty)}
                    >
                      Log in
                      {loading && (
                        <img
                          src="/oval.svg"
                          className="w-4 h-4 ml-2 inline-block"
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

export default Login;
