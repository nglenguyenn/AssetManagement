import React, { useEffect, useState } from 'react';
import { Modal } from 'react-bootstrap';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';

import IFirstTimeChangePassword from "../../interfaces/IFirstTimeChangePassword"
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import ModalHeader from 'react-bootstrap/esm/ModalHeader';
import PasswordField from '../../components/FormInputs/PasswordField';


const initialValues: IFirstTimeChangePassword = {
    newPassword: '',
}

const validationSchema = Yup.object().shape({
    newPassword: Yup.string().required('Required'),
});

const FirstTimeChangePassword = () => {
    const [isShow, setShow] = useState(true);

    const dispatch = useAppDispatch();

    const { loading, error } = useAppSelector(state => state.authReducer);

    return (
        <>
            <div className='container'>
                <Modal
                    show={isShow}
                >
                    <Modal.Header closeButton>
                        <Modal.Title>
                            Change password
                        </Modal.Title>
                    </Modal.Header>

                    <Modal.Body>
                        <Formik
                            initialValues={initialValues}
                            validationSchema={validationSchema}
                            onSubmit={(values) => { }}
                        >
                            {(action) => (
                                <Form>
                                    This is the first time you logged in you have to change your password to continue

                                    <PasswordField name="newPassword" label="NewPassword" isrequired />

                                    {error?.error && (
                                        <div className="invalid">
                                            {error.message}
                                        </div>
                                    )}

                                    <div className="text-right mt-5 ">
                                        <button className="btn btn-danger mr-3"
                                            type="submit" disabled={loading}>
                                            Save
                                            {(loading) && <img src="/oval.svg" alt="" className='w-4 h-4 ml-2 inline-block mr-1' />}
                                        </button>
                                        <button className="btn btn-light">
                                            Cancel
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

export default FirstTimeChangePassword