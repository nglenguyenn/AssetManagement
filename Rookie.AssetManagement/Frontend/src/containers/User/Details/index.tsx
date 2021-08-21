import React from "react";
import { Modal } from "react-bootstrap";
import IUser from "src/interfaces/User/IUser";
import { MaleUserGenderLabel, FemaleUserGenderLabel } from "src/constants/User/GenderConstants";
import { convertDate } from "src/utils/formatDateTime";

type Props = {
   isShow: boolean;
   onHide?: Function;
   hide: Function;
   user?: IUser | null;
};

const UserDetailsModal: React.FC<Props> = ({ isShow, onHide, hide, user }) => {
   const handelCancel = () => {
      hide();
   };

   const convertFulName = (firstname, lastname) => {
      return [firstname, lastname].join(" ");
   }

   return (
      <>
         <Modal centered show={isShow} onHide={onHide} dialogClassName="modal-90w" aria-labelledby="login-modal">
            <Modal.Header closeButton>
               <Modal.Title id="login-modal">Detailed User Information</Modal.Title>
            </Modal.Header>

            <Modal.Body>
               <>
                  <div className="info">
                     <div className="row">
                        <div className="col-4">Staff Code</div>
                        <div className="col-8">{user?.staffCode}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Full Name</div>
                        <div className="col-8">{convertFulName(user?.firstName, user?.lastName)}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Username</div>
                        <div className="col-8">{user?.userName}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Date of Birth</div>
                        <div className="col-8">{user?.dateOfBirth ? convertDate(user?.dateOfBirth) : ""}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Gender</div>
                        <div className="col-8">{user?.gender ? FemaleUserGenderLabel : MaleUserGenderLabel}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Joined Date</div>
                        <div className="col-8">{user?.joinedDate ? convertDate(user?.joinedDate) : ""}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Type</div>
                        <div className="col-8">{user?.type}</div>
                     </div>

                     <div className="row">
                        <div className="col-4">Location</div>
                        <div className="col-8">{user?.location}</div>
                     </div>
                  </div>
               </>
            </Modal.Body>
         </Modal>
      </>
   );
};

export default UserDetailsModal;
