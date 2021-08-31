import React from "react";
import { Modal } from "react-bootstrap";
import IAssignment from "../../../interfaces/Assignment/IAssignment";
import { convertDate } from "../../../utils/formatDateTime";
import {AcceptedAssignmentLabel, AcceptedAssignmentString, WaitingAssignmentLabel, WaitingAssignmentString} from "../../../constants/Assignment/StateConstants"
type Props = {
  isShow: boolean;
  onHide?: Function;
  hide: Function;
  assignment?: IAssignment | null;
};
const AssignmentDetailsModal: React.FC<Props> = ({
  isShow,
  onHide,
  hide,
  assignment,
}) => {
  const handelCancel = () => {
    hide();
  };
  const showAssignmentState = (state: string) => {
          const stateLabel = [
          AcceptedAssignmentLabel,
          WaitingAssignmentLabel
          ];
    
          const stateString = [
            AcceptedAssignmentString,
            WaitingAssignmentString, 
          ]
          const index = stateString.indexOf(state);
          return stateLabel[index];
       }
  
  return (
    <>
      <Modal
        centered
        show={isShow}
        onHide={onHide}
        dialogClassName="modal-90w"
        aria-labelledby="login-modal"
      >
        <Modal.Header closeButton>
          <Modal.Title id="login-modal">
            Detailed Assignment Information
          </Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <>
            <div className="info">
              <div className="row">
                <div className="col-4">Asset Code</div>
                <div className="col-8">{assignment?.assetCode}</div>
              </div>
              <div className="row">
                <div className="col-4">Asset Name</div>
                <div className="col-8">{assignment?.assetName}</div>
              </div>
              <div className="row">
                <div className="col-4">Specification</div>
                <div className="col-8">{assignment?.specification}</div>
              </div>
              <div className="row">
                <div className="col-4">Assigned to</div>
                <div className="col-8">{assignment?.assignedTo}</div>
              </div>
              <div className="row">
                <div className="col-4">Assigned by</div>
                <div className="col-8">{assignment?.assignedBy}</div>
              </div>
              <div className="row">
                <div className="col-4">Assigned Date</div>
                <div className="col-8">
                  {assignment?.assignedDate
                    ? convertDate(assignment.assignedDate)
                    : ""}
                </div>
              </div>
              <div className="row">
                <div className="col-4">State</div>
                <div className="col-8">{assignment?.state ? showAssignmentState(assignment.state) : ""}</div>
              </div>
              <div className="row">
                <div className="col-4">Note</div>
                <div className="col-8">{assignment?.note}</div>
              </div>
            </div>
          </>
        </Modal.Body>
      </Modal>
    </>
  );
};
export default AssignmentDetailsModal;
