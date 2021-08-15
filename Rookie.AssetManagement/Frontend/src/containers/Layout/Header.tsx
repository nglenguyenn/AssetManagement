import React, { useState } from "react";
import { Dropdown } from "react-bootstrap";
import { useHistory, useLocation } from "react-router-dom";
import ConfirmModal from "src/components/ConfirmModal";
import ChangePasswordModal from "../ChangePassword";
import { HOME } from "src/constants/pages";

import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { cleanUp, logout } from "../Authorize/reducer";
import { handleChangePassword } from "../Authorize/sagas/handles";
import { Status } from "src/constants/status";

// eslint-disable-next-line react/display-name
const CustomToggle = React.forwardRef<any, any>(({ children, onClick }, ref): any => (
  <a
    className="btn btn-link dropdownButton"
    ref={ref as any}
    onClick={(e) => {
      e.preventDefault();
      onClick(e);
    }}
  >
    {children} <span>&#x25bc;</span>
  </a>
));

const Header = () => {
  const history = useHistory();
  const { pathname } = useLocation();
  const { account, status } = useAppSelector(state => state.authReducer);
  const dispatch = useAppDispatch();

  const [showModalChangePasswod, setShowModalChangePasswod] = useState(false);
  const [showConfirmLogout, setShowConfirmLogout] = useState(false);
  const [isChangePasswordSuccess, setIsChangePasswordSuccess] = useState(false);

  const headerName = () => {
    const pathnameSplit = pathname.split('/');
    pathnameSplit.shift();
    return pathnameSplit.join(' > ').toString() || 'Home';
  }

  const openModal = () => {
    setShowModalChangePasswod(true);
  };

  const handleHide = () => {
    dispatch(cleanUp());
    setShowModalChangePasswod(false);
    setIsChangePasswordSuccess(false);
  }

  const handleLogout = () => {
    setShowConfirmLogout(true);
  };

  const handleCancleLogout = () => {
    setShowConfirmLogout(false);
  };

  const handleConfirmedLogout = () => {
    history.push(HOME);
    dispatch(logout());
  };

  const handleChangePasswordSuccess = () => {
    setIsChangePasswordSuccess(true);
  };

  return (
    <>
      <div className='header align-items-center font-weight-bold'>
        <div className="container-lg-min container-fluid d-flex pt-2">
          <p className='headText'>{`${headerName()}`}</p>

          <div className='ml-auto text-white'>
            <Dropdown>
              <Dropdown.Toggle as={CustomToggle}>
                {account?.username}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                <Dropdown.Item onClick={openModal}>Change Password</Dropdown.Item>
                <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </div>
        </div>
      </div>

      <ConfirmModal
        title="Are you sure?"
        isShow={showConfirmLogout}
        onHide={handleCancleLogout}
      >
        <div>
          <div className="text-center">Do you want to log out?</div>
          <div className="text-center mt-3">
            <button className="btn btn-danger mr-3" onClick={handleConfirmedLogout} type="button">Log out</button>
            <button className="btn btn-outline-secondary" onClick={handleCancleLogout} type="button">Cancel</button>
          </div>
        </div>
      </ConfirmModal>

      <ChangePasswordModal
        isShow={showModalChangePasswod}
        onHide={handleHide}
        hide={handleHide}
      >
        <button className="btn btn-outline-secondary" onClick={handleHide} type="button">Cancel</button>
      </ChangePasswordModal>

    </>
  );
};

export default Header;
