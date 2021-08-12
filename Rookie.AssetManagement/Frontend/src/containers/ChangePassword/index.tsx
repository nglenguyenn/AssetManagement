import React, { useEffect, useState } from 'react';
import { Modal } from 'react-bootstrap';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';

import IChangepassWord from "../../interfaces/IChangePassword"
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import ModalHeader from 'react-bootstrap/esm/ModalHeader';
import PasswordField from 'src/components/FormInputs/PasswordField';


const initialValues: IChangepassWord = {
    currentPassword: '',
    newPassword: '',
}

const validationSchema = Yup.object().shape({
    currentPassword: Yup.string().trim().required('Required'),
    newPassword: Yup.string().trim().required('Required')
});

const ChangePassword = () => {
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
                            {(actions) => (
                                <Form>
                                    <PasswordField name="currentPassword" label="CurrentPassword" isrequired />
                                    <PasswordField name="newPassword" label="NewPassword" isrequired />

                                    {error?.error && (
                                        <div className="invalid">
                                            {error.message}
                                        </div>
                                    )}

                                    <div className="text-right mt-5 ">
                                        <button className="btn btn-danger mr-3"
                                            type="submit" disabled={!(actions.isValid && actions.dirty)}>
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

export default ChangePassword