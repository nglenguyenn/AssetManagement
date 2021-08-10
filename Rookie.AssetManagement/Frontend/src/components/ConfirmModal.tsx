import React from "react";
import { Modal } from 'react-bootstrap';

type Props = {
    title: string,
    isShow: boolean,
    onHide?: Function,
    children: React.ReactNode,
}

const ConfirmModal: React.FC<Props> = ({ title, isShow, onHide, children }) => {

    return (
        <Modal
            show={isShow}
            onHide={onHide}
            dialogClassName="modal-90w"
            aria-labelledby="login-modal"
        >
            <Modal.Header closeButton>
                <Modal.Title id="login-modal">
                    {title}
                </Modal.Title>
            </Modal.Header>

            <Modal.Body>
                {children}
            </Modal.Body>
        </Modal>
    );
};

export default ConfirmModal;
